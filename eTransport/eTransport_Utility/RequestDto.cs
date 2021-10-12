using System.ComponentModel.DataAnnotations;

namespace eTransport_Utility
{
    public class RequestDto
    {
        #region properties
        [Key]
        [Required]
        public int RequestId { get; set; }
        [Required]
        public string Materials { get; set; }
        [Required]
        public MaterialState MaterialType { get; set; }
        [Required]
        public float CargoHeight { get; set; }
        [Required]
        public float CargoWidth { get; set; }
        [Required]
        public float CargoLength { get; set; }
        [Required]
        public float CargoVolume { get; set; }
        [Required]
        public string PickupLocation { get; set; }
        public string Comments { get; set; }
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public bool IsComplete { get; set; }
        [Required]
        public bool IsLinked { get; set; }
        #endregion

        public RequestDto(int requestId, string materials, MaterialState materialType, float cargoHeight, float cargoWidth, float cargoLength, float cargoVolume, string pickupLocation, string comments, bool isApproved, bool isComplete, bool isLinked)
        {
            RequestId = requestId;
            Materials = materials;
            MaterialType = materialType;
            CargoHeight = cargoHeight;
            CargoWidth = cargoWidth;
            CargoLength = cargoLength;
            CargoVolume = cargoVolume;
            PickupLocation = pickupLocation;
            Comments = comments;
            IsApproved = isApproved;
            IsComplete = isComplete;
            IsLinked = isLinked;
        }
    }
}
