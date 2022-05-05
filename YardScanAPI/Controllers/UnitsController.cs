using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace YardScanAPI.Controllers
{
  [EnableCors("ionic")]
  [Route("api/[controller]")]
  [ApiController]
  public class UnitsController : ControllerBase
  {
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    // Constructor
    public UnitsController(DataContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    // Units
    [HttpGet]
    public async Task<ActionResult<List<Unit>>> GetUnits()
    {
      SqlConnection sqlConnection = new(_config.GetConnectionString("DefaultConnection"));

      var units = DataHelper.Units(sqlConnection);

      if (units == null || units.Count == 0)
        return BadRequest("There are no vehicles in the yards!");

      return Ok(units);
    }

    // Get a single unit id
    [HttpGet("{unitId}")]
    public async Task<ActionResult<Unit>> GetSingleUnit(int unitId)
    {
      // Inspect connection
      SqlConnection sqlConnection = new(_config.GetConnectionString("InspectConnection"));

      var unit = await _context.Units!.Where(u => u.UnitId == unitId).FirstOrDefaultAsync();

      if (unit == null)
        return BadRequest("Unit could not be found!");

      var location = await _context.Locations!.Where(l => l.Id == unit.LocationId).FirstOrDefaultAsync();

      if (location == null)
        return BadRequest("Location of the unit do not exists in database!");

      var unitInYard = new UnitViewModel
      {
        UnitId = unitId,
        Serial = DataHelper.GetUnitSerial(sqlConnection, unitId),
        Location = location.Name,
        Row = unit.Row,
        RowNumber = unit.RowNumber,
        TrackInDate = unit.TrackInDate,
        TrackOutDate = unit.TrackOutDate,
        TrackOutLocationId = unit.TrackOutLocationId
      };

      return Ok(unitInYard);
    }

    // Track in a new vehicle
    [HttpPost("trackInUnit")]
    public async Task<ActionResult<Unit>> TrackIn(string serial, int locationId, char row, int rowNumber)
    {
      // Inspect connection
      SqlConnection sqlConnection = new(_config.GetConnectionString("InspectConnection"));

      var unitId = DataHelper.GetUnitId(sqlConnection, serial);

      if (unitId == 0)
        return BadRequest("Could not add the unit to the yards!");

      var trackUnit = await _context.Units!.Where(u => u.UnitId == unitId).FirstOrDefaultAsync();

      // Look if unit already exists
      if (trackUnit == null)
      {
        var unit = new Unit
        {
          UnitId = unitId,
          Coordinates = null,
          LocationId = locationId,
          Row = row,
          RowNumber = rowNumber,
          TrackInDate = DateTime.Now
        };

        var unitHistory = new TrackInUnitHistory
        {
          UnitId = unitId,
          Coordinates = null,
          LocationId = locationId,
          Row = row,
          RowNumber = rowNumber,
          TrackInDate = DateTime.Now
        };

        _context.Units!.Add(unit);
        _context.TrackInUnitHistories!.Add(unitHistory);

        await _context.SaveChangesAsync();
      }
      else
      {
        trackUnit.Coordinates = null;
        trackUnit.LocationId = locationId;
        trackUnit.Row = row;
        trackUnit.RowNumber = rowNumber;
        trackUnit.TrackInDate = DateTime.Now;

        var unitHistory = new TrackInUnitHistory
        {
          UnitId = unitId,
          Coordinates = null,
          LocationId = locationId,
          Row = row,
          RowNumber = rowNumber,
          TrackInDate = DateTime.Now
        };

        _context.TrackInUnitHistories!.Add(unitHistory);

        await _context.SaveChangesAsync();
        return Ok(trackUnit); 
      }

      return Ok("2");
    }

    // Track out a vehicle
    [HttpPut("trackOutUnit")]
    public async Task<ActionResult<Unit>> TrackOut(string serial, int locationId)
    {
      // Inspect connection
      SqlConnection sqlConnection = new(_config.GetConnectionString("InspectConnection"));

      var unitId = DataHelper.GetUnitId(sqlConnection, serial);

      if (unitId == 0)
        return BadRequest("Unit could not be found!");

      var unit = await _context.Units!.Where(u => u.UnitId == unitId).FirstOrDefaultAsync();

      if (unit == null)
        return BadRequest("Unit do not have a track in date!");

      if (unit.TrackOutDate != null)
        return BadRequest("Unit has already a track out date!");

      unit.TrackOutDate = DateTime.Now;
      unit.TrackOutLocationId = locationId;
      await _context.SaveChangesAsync();

      return Ok(unit);
    }

    // Locations
    [HttpGet("locations")]
    public async Task<ActionResult<List<Location>>> GetLocations()
    {
      var locations = await _context.Locations!.ToListAsync();

      if (locations == null || locations.Count == 0)
        return BadRequest("There are no locations registered in the database!");
      return Ok(locations);
    }
  }
}
