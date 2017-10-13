using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrudExamples.WebApi.Models.Exceptions
{
    public class VesselNotFoundException: Exception
    {
        public VesselNotFoundException()
        {

        }

        public VesselNotFoundException(string message)
            : base(message)
        {

        }
    }
}