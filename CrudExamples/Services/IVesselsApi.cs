using CrudExamples.Shared;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudExamples.Services
{
    interface IVesselsApi
    {
        [Get("/api/vessels")]
        Task<IEnumerable<VesselDto>> GetAllAsync();

        [Post("/api/vessels")]
        Task<VesselDto> AddNewVesselAsync([Body] VesselDto vessel);

        [Put("/api/vessels/{id}")]
        Task<VesselDto> EditVesselAsync(int id, [Body] VesselDto vessel);
    }
}
