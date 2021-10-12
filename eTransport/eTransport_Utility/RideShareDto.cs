using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTransport_Utility
{
    public class RideShareDto
    {
        #region properties
        [Key]
        [Required]
        public int RideShareId { get; set; }
        public string Requests { get; set; }
        [Required]
        public string Destination { get; set; }
        public int SelectedTruck { get; set; }
        [ForeignKey("TruckId")]
        public int MaxCapacity { get; set; }
        #endregion

        public RideShareDto(int rideShareId, string requests, string destination, int selectedTruck, int maxCapacity)
        {
            RideShareId = rideShareId;
            Requests = requests;
            Destination = destination;
            SelectedTruck = selectedTruck;
            MaxCapacity = maxCapacity;
        }
    }
}
