using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudExamples.InteractionActions
{
    /// <summary>
    /// Represents a custom notification for the EditVesselView that specifies whether the screen is in Add or Edit mode.
    /// </summary>
    public class VesselNotification : Notification
    {
        public VesselNotification.EditModes EditMode { get; set; }

        /// <summary>
        /// Represents the different edit modes for the EditVessel view. 
        /// Since we're reusing the same view for both Edit and Add, we need to know what endpoint to call on Save.
        /// </summary>
        public enum EditModes
        {
            NotSet = 0,
            Add = 1,
            Edit = 2,
            Remove = 3
        }
    }
}
