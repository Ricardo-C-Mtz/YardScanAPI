using System.ComponentModel.DataAnnotations;

namespace YardScanAPI.Models
{
    public class TrackInUnitHistory
    {
        public int Id { get; set; }

        [Required]
        public int UnitId { get; set; }

        [Required]
        public Location? Location { get; set; }
        public int LocationId { get; set; }

        [StringLength(200)]
        public string? Coordinates { get; set; } = string.Empty;
        public char Row { get; set; }
        public int RowNumber { get; set; }
        public DateTime TrackInDate { get; set; }

    }
}
