using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudExamples.WebApi.Models.Database
{
    public class VesselEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BoardedPassengers { get; set; }

        public int MaxPassengersCapacity { get; set; }

        // This type of business object -> to -> entity manual mapping can also be solved with AutoMapper or another mapper class, 
        // or in the VesselsService so that the entity doesn't know about the business object.
        public static VesselEntity CreateFromVessel(Vessel vessel)
        {
            return new VesselEntity
            {
                Id = vessel.Id,
                Name = vessel.Name,
                BoardedPassengers = vessel.BoardedPassengers,
                MaxPassengersCapacity = vessel.MaxPassengersCapacity
            };
        }

        public Vessel CreateVessel()
        {
            return new Vessel(this.Id, this.Name, this.MaxPassengersCapacity, this.BoardedPassengers);
        }

    }
}