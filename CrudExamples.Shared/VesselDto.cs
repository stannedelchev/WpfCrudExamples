using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudExamples.Shared
{
    /// <summary>
    /// Represents a common data transfer object between the client application and the server.
    /// Can be used both for API input, as well as API results (although ideally both should be separate classes).
    /// </summary>
    public class VesselDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxPassengersCapacity { get; set; }

        public int BoardedPassengers { get; set; }
    }
}
