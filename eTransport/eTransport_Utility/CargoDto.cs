using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTransport_Utility
{
    public class CargoDto
    {
        #region properties
        public string UserId { get; set; }
        [Key]
        public int CargoId { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        [Required]
        public string Destination { get; set; }
        public string SelectedTrucks { get; set; }
        public int MaxCapacity { get; set; }
        #endregion

        public CargoDto(string userId, int cargoId, int requestId, string destination, string selectedTrucks, int maxCapacity)
        {
            UserId = userId;
            CargoId = cargoId;
            RequestId = requestId;
            Destination = destination;
            SelectedTrucks = selectedTrucks;
            MaxCapacity = maxCapacity;
        }
    }
}
