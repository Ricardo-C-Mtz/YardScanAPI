namespace YardScanAPI.Models
{
  public class UnitViewModel
  {
    public int UnitId { get; set; }
    public string Serial { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public char Row { get; set; }
    public int RowNumber { get; set; }
    public DateTime TrackInDate { get; set; }
    public DateTime? TrackOutDate { get; set; }
    public int? TrackOutLocationId { get; set; }
  }
}
