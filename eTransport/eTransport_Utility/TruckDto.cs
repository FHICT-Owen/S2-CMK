using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace eTransport_Utility
{
    [Index(nameof(LicensePlate), IsUnique = true)]
    public class TruckDto
    {
        #region properties

        [Key]
        [Required]
        public int TruckId { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Amount of kilometers is outside the range.")]
        [RegularExpression("^[0-9]+km{1}$", ErrorMessage = "Expression does not match.")]
        public string TruckRange { get; set; }
        [Required]
        [StringLength(8, ErrorMessage = "Amount of kilo's is outside the range.")]
        [RegularExpression("^[0-9]+kg{1}$", ErrorMessage = "Expression does not match.")]
        public string Weight { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "License plate length is invalid.")]
        [RegularExpression("^[A-Z0-9]{1,3}-[A-Z0-9]{1,3}-[A-Z0-9]{1,3}$", ErrorMessage = "Expression does not match.")]
        public string LicensePlate { get; set; }
        [Required]
        public float FuelUsage { get; set; }
        [Required]
        public float Height { get; set; }
        [Required]
        public float Capacity { get; set; }
        [Required]
        public TruckBrand Brand { get; set; }
        [Required]
        public MaterialState CargoType { get; set; }
        [Required]
        public bool InUse { get; set; }
        #endregion

        public TruckDto(int truckId, string truckRange, string weight, string licensePlate, float fuelUsage, float height, float capacity, TruckBrand brand, MaterialState cargoType, bool inUse)
        {
            TruckId = truckId;
            TruckRange = truckRange;
            Weight = weight;
            LicensePlate = licensePlate;
            FuelUsage = fuelUsage;
            Height = height;
            Capacity = capacity;
            Brand = brand;
            CargoType = cargoType;
            InUse = inUse;
        }
    }
}
