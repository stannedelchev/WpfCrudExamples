using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudExamples.Shared;
using System.ComponentModel;
using System.Collections;

namespace CrudExamples.ViewModels
{
    public class VesselViewModel : BindableBase, IEditableObject, INotifyDataErrorInfo
    {
        private int id;
        private string name;
        private int boardedPassengers;
        private int maxPassengersCapacity;
        private VesselViewModel memento;
        private Dictionary<string, string> errors = new Dictionary<string, string>();

        public VesselViewModel()
        {
            this.Validate();
        }

        public VesselViewModel(VesselDto dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.BoardedPassengers = dto.BoardedPassengers;
            this.MaxPassengersCapacity = dto.MaxPassengersCapacity;
        }

        public VesselViewModel(VesselViewModel other)
        {
            this.CopyFromOther(other);
        }

        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        public string Name
        {
            get { return name; }
            set { this.SetProperty(ref this.name, value); this.Validate(); }
        }

        public int BoardedPassengers
        {
            get { return this.boardedPassengers; }
            set { this.SetProperty(ref this.boardedPassengers, value); this.Validate(); }
        }

        public int MaxPassengersCapacity
        {
            get { return maxPassengersCapacity; }
            set { this.SetProperty(ref this.maxPassengersCapacity, value); this.Validate(); }
        }

        private void Validate()
        {
            this.errors.Clear();

            if(string.IsNullOrEmpty(this.Name))
            {
                this.errors.Add(nameof(this.Name), "Name is required.");
                this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(this.Name)));
            }

            if(this.MaxPassengersCapacity <= 0)
            {
                this.errors.Add(nameof(this.MaxPassengersCapacity), "Capacity should be > 0.");
                this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(this.MaxPassengersCapacity)));
            }

            if(this.BoardedPassengers > this.MaxPassengersCapacity)
            {
                this.errors.Add(nameof(this.BoardedPassengers), "Ship is over capacity.");
            }

            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
        }

        public bool HasErrors => this.errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void BeginEdit()
        {
            this.memento = new VesselViewModel(this);
        }

        public void CancelEdit()
        {
            this.CopyFromOther(memento);
        }

        public void EndEdit()
        {
            this.memento = null;
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if(this.errors.ContainsKey(propertyName))
            {
                return new[] { this.errors[propertyName] };
            }

            return null;
        }

        private void CopyFromOther(VesselViewModel other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.MaxPassengersCapacity = other.MaxPassengersCapacity;
            this.BoardedPassengers = other.BoardedPassengers;
        }
    }
}
