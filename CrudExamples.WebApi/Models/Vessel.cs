using CrudExamples.WebApi.Models.Exceptions;
using System;

namespace CrudExamples.WebApi.Models
{
    public class Vessel
    {
        private int boardedPassengers = 0;

        public Vessel(int? id, string name, int maxPassengersCapacity, int boardedPassengers)
        {
            this.Id = id.GetValueOrDefault();
            this.Name = name;
            this.MaxPassengersCapacity = maxPassengersCapacity;
            this.boardedPassengers = boardedPassengers;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxPassengersCapacity { get; set; }

        public int BoardedPassengers
        {
            get
            {
                return this.boardedPassengers;
            }
            set
            {
                this.Board(value);
            }
        }

        public void Board(int passengers)
        {
            if(passengers <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(passengers), "Passengers should be a positive number");
            }

            if (this.boardedPassengers + passengers > this.MaxPassengersCapacity)
            {
                throw new VesselOverCapacityException($"Maximum capacity of vessel is {this.MaxPassengersCapacity}");
            }

            this.boardedPassengers += passengers;
        }
    }
}