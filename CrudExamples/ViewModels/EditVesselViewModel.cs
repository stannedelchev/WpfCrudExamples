using CrudExamples.Helpers;
using CrudExamples.InteractionActions;
using CrudExamples.Services;
using CrudExamples.Shared;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrudExamples.ViewModels
{
    public class EditVesselViewModel : BindableBase, IInteractionRequestAware
    {
        private VesselViewModel vessel;
        private INotification notification;
        private VesselNotification.EditModes editMode;
        private bool isLoading;

        public EditVesselViewModel()
        {
            this.SaveCommand = new DelegateCommand(this.OnSaveCommandExecuted, () => this.Vessel != null && !this.Vessel.HasErrors);
            this.CancelCommand = new DelegateCommand(this.OnCancelCommandExecuted);
        }

        public VesselViewModel Vessel
        {
            get
            {
                return vessel;
            }

            set
            {
                // Clean up any existing event handlers for ErrorsChanged
                if (this.vessel != null)
                {
                    this.vessel.ErrorsChanged -= this.RaiseSaveCommandCanExecuteChanged;
                }

                this.SetProperty(ref this.vessel, value);

                // Reattach again
                if (vessel != null)
                {
                    this.vessel.ErrorsChanged += this.RaiseSaveCommandCanExecuteChanged;
                }

                this.SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsLoading
        {
            get { return this.isLoading; }
            set { this.SetProperty(ref this.isLoading, value); }
        }

        public INotification Notification
        {
            get
            {
                return this.notification;
            }

            set
            {
                this.notification = value;
                this.editMode = (value as VesselNotification)?.EditMode ?? VesselNotification.EditModes.NotSet;

                this.Vessel = value.Content as VesselViewModel;
                this.Vessel?.BeginEdit();
            }
        }

        public Action FinishInteraction { get; set; }

        public DelegateCommand SaveCommand { get; }

        public DelegateCommand CancelCommand { get; }

        private async void OnSaveCommandExecuted()
        {
            this.Vessel?.EndEdit();

            try
            {
                this.IsLoading = true;
                var service = RestService.For<IVesselsApi>(ConfigurationManager.AppSettings[Constants.ApiBaseUrlConfigName]);

                switch (this.editMode)
                {
                    case VesselNotification.EditModes.Add:
                        await service.AddNewVesselAsync(this.ToDto());
                        break;
                    case VesselNotification.EditModes.Edit:
                        await service.EditVesselAsync(this.Vessel.Id, this.ToDto());
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.IsLoading = false;
            }
            this.FinishInteraction();
        }

        private void OnCancelCommandExecuted()
        {
            this.Vessel?.CancelEdit();
            this.FinishInteraction();
        }

        private void RaiseSaveCommandCanExecuteChanged(object sender, DataErrorsChangedEventArgs args)
        {
            this.SaveCommand.RaiseCanExecuteChanged();
        }

        private VesselDto ToDto()
        {
            return new VesselDto()
            {
                Id = vessel.Id,
                BoardedPassengers = vessel.BoardedPassengers,
                MaxPassengersCapacity = vessel.MaxPassengersCapacity,
                Name = vessel.Name
            };
        }
    }
}
