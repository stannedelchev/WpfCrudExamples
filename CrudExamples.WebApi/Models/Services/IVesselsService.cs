using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrudExamples.WebApi.Models.Services
{
    public interface IVesselsService
    {
        Task<int> AddVesselAsync(Vessel vessel);

        Task UpdateVesselAsync(int id, Vessel vessel);

        Task<IEnumerable<Vessel>> GetAllAsync();

        Task RemoveVesselAsync(int vesselId);
    }
}