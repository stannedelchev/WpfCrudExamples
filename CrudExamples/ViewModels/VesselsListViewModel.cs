using CrudExamples.Helpers;
using CrudExamples.InteractionActions;
using CrudExamples.Services;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudExamples.ViewModels
{
    public class VesselsListViewModel : BindableBase, INavigationAware
    {
        private bool isLoading;
        private IEnumerable<VesselViewModel> vessels;

        public VesselsListViewModel()
        {
            this.AddNewVesselCommand = new DelegateCommand(this.OnAddNewVesselCommandExecuted);
            this.EditVesselCommand = new DelegateCommand<VesselViewModel>(this.OnEditVesselCommandExecuted);

            this.AddNewVesselInteractionRequest = new InteractionRequest<VesselNotification>();
            this.EditVesselInteractionRequest = new InteractionRequest<VesselNotification>();
        }

        public DelegateCommand AddNewVesselCommand { get; }
        public DelegateCommand<VesselViewModel> EditVesselCommand { get; }

        // No requirement to have two separate interaction requests when they do the same thing.
        // This approach is more useful when the requests might have different arguments or triggers/actions in the view.
        public InteractionRequest<VesselNotification> AddNewVesselInteractionRequest { get; set; }
        public InteractionRequest<VesselNotification> EditVesselInteractionRequest { get; set; }

        public bool IsLoading
        {
            get { return isLoading; }
            set { this.SetProperty(ref this.isLoading, value); }
        }

        public IEnumerable<VesselViewModel> Vessels
        {
            get { return vessels; }
            set { this.SetProperty(ref this.vessels, value); }
        }

        public async Task InitializeAsync()
        {
            this.IsLoading = true;
            try
            {
                var apiService = RestService.For<IVesselsApi>(ConfigurationManager.AppSettings[Constants.ApiBaseUrlConfigName]);
                var result = await apiService.GetAllAsync();

                this.Vessels = result.Select(dto => new VesselViewModel(dto)).ToList();
            }
            catch (Exception ex)
            {
                // This can be abstracted away with an InteractionRequest.
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        // Implementing INavigationAware is not required if the view is not navigated to, but activated manually.
        // INavigationAware is useful for triggering InitializeAsync, so if it's not used, the appropriate approach would be 
        // to call InitializeAsync() manually when the view is loaded, either via EventTrigger+InvokeMethod or in the view's codebehind.
        #region INavigationAware implementation
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var _ = this.InitializeAsync();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion

        private void OnAddNewVesselCommandExecuted()
        {
            this.AddNewVesselInteractionRequest.Raise(new VesselNotification()
            {
                Title = "Add new vessel",
                Content = new VesselViewModel(),
                EditMode = VesselNotification.EditModes.Add
            }, _ =>
            {
                var __ = this.InitializeAsync();
            });
        }

        private void OnEditVesselCommandExecuted(VesselViewModel vessel)
        {
            // We can specify the notification content in two ways here:
            // 1* If we use "Content = vessel" and pass the selected vessel directly, any intermediate changes in the Edit window will be applied 
            //    automatically to the List window (they're both binding to the same instance). Whenever we cancel (i.e. use IEditableObject.CancelEdit())
            //    the changes will be reverted and the List window will be back to its original state.
            // 2* If we use "Content = new VesselViewModel(vessel)", there's no need to implement IEditableObject on VesselViewModel, because 
            //    we're passing a new instance to the Edit window every time and it will be discarded both on Save and Cancel.
            this.EditVesselInteractionRequest.Raise(new VesselNotification()
            {
                Title = $"Edit {vessel.Name}",
                Content = vessel, // APPROACH 1
                //Content = new VesselViewModel(vessel), // APPROACH 2
                EditMode = VesselNotification.EditModes.Edit
            }, _ =>
            {
                var __ = this.InitializeAsync();
            });
        }
    }
}
