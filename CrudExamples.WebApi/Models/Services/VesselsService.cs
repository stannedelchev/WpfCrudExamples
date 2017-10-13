using CrudExamples.WebApi.Models.Database;
using CrudExamples.WebApi.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudExamples.WebApi.Models.Services
{
    internal class VesselsService: IVesselsService
    {
        private readonly VesselsContext context;

        public VesselsService(VesselsContext context)
        {
            this.context = context;
        }

        public async Task<int> AddVesselAsync(Vessel vessel)
        {
            var entity = VesselEntity.CreateFromVessel(vessel);

            this.context.Vessels.Add(entity);
            await this.context.SaveChangesAsync();

            // business object needs to be updated with proper database id, as we're using different BO and DB classes and instances.
            vessel.Id = entity.Id;
            return vessel.Id;
        }

        public async Task<IEnumerable<Vessel>> GetAllAsync()
        {
            var dbResult = await this.context.Vessels.AsNoTracking().ToListAsync();
            var result = dbResult.Select(v => v.CreateVessel());
            return result;
        }

        public async Task UpdateVesselAsync(int id, Vessel vessel)
        {
            var dbVessel = await this.context.Vessels.FindAsync(id);
            if(dbVessel == null)
            {
                throw new VesselNotFoundException($"Could not find vessel with id #{id}");
            }

            dbVessel.Name = vessel.Name;
            dbVessel.BoardedPassengers = vessel.BoardedPassengers;
            dbVessel.MaxPassengersCapacity = vessel.MaxPassengersCapacity;

            await this.context.SaveChangesAsync();
        }
    }
}