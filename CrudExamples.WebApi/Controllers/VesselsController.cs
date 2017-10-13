using CrudExamples.Shared;
using CrudExamples.WebApi.Models;
using CrudExamples.WebApi.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CrudExamples.WebApi.Controllers
{
    [ThrottleActionFilter(DelayTimeInMilliseconds = 1000)]
    public class VesselsController : ApiController
    {
        private readonly IVesselsService vesselsService;

        public VesselsController(IVesselsService vesselsService)
        {
            this.vesselsService = vesselsService;
        }

        [HttpGet]
        public async Task<IEnumerable<VesselDto>> GetAllVesselsAsync()
        {
            var result = await this.vesselsService.GetAllAsync();

            // manual mapping can be replaced by AutoMapper
            return result.Select(VesselToDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddNewVesselAsync([FromBody] VesselDto input)
        {
            if(!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var newVessel = this.DtoToVessel(input);
            await this.vesselsService.AddVesselAsync(newVessel);

            var result = this.VesselToDto(newVessel);
            return this.Ok(result);
        }


        [HttpPut]
        public async Task<IHttpActionResult> EditVesselAsync(int id, [FromBody] VesselDto input)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var newVessel = this.DtoToVessel(input);
            await this.vesselsService.UpdateVesselAsync(id, newVessel);

            var result = this.VesselToDto(newVessel);
            return this.Ok(result);
        }


        private VesselDto VesselToDto(Vessel vessel)
        {
            return new VesselDto()
            {
                Id = vessel.Id,
                BoardedPassengers = vessel.BoardedPassengers,
                MaxPassengersCapacity = vessel.MaxPassengersCapacity,
                Name = vessel.Name
            };
        }

        private Vessel DtoToVessel(VesselDto dto)
        {
            return new Vessel(dto.Id, dto.Name, dto.MaxPassengersCapacity, dto.BoardedPassengers);
        }
    }
}