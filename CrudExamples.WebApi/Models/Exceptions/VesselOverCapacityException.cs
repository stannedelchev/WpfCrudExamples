using System;

namespace CrudExamples.WebApi.Models.Exceptions
{
    public class VesselOverCapacityException: Exception
    {
        public VesselOverCapacityException()
        {

        }

        public VesselOverCapacityException(string message)
            :base(message)
        {

        }
    }
}