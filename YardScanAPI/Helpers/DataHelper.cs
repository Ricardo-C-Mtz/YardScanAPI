using Microsoft.Data.SqlClient;
using System.Data;

namespace YardScanAPI.Helpers
{
  public class DataHelper
  {
    // Get all on yard units
    public static List<UnitViewModel> Units(SqlConnection sqlConnection)
    {
      var units = new List<UnitViewModel>();

      try
      {
        using var command = new SqlCommand("GetUnitsOnYard", sqlConnection);
        command.CommandType = CommandType.StoredProcedure;
        sqlConnection.Open();

        var dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
          var unit = new UnitViewModel()
          {
            UnitId = (int)dataReader[0],
            Serial = dataReader[1].ToString(),
            Location = dataReader[2].ToString(),
            Row = Convert.ToChar(dataReader[3]),
            RowNumber = (int)dataReader[4],
            TrackInDate = (DateTime)dataReader[5],
            TrackOutDate = dataReader[6] == DBNull.Value ? DateTime.MinValue : (DateTime)dataReader[6],
            //TrackOutLocationId = dataReader["TrackOutLocationId"] == DBNull.Value ? 0 : (int)dataReader["TrackOutLocationId"]
          };
          units.Add(unit);
        }
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        if (sqlConnection.State == ConnectionState.Open)
        {
          sqlConnection.Close();
        }
      }

      return units;
    }

    // Get unit id from Inspect db
    public static int GetUnitId(SqlConnection sqlConnection, string serial)
    {
      var unitId = 0;

      var sql = "select top 1 unit_id from units where serial = @serial order by unit_id desc";

      try
      {
        using var command = new SqlCommand(sql, sqlConnection);
        sqlConnection.Open();
        command.Parameters.Add("serial", SqlDbType.NVarChar).Value = serial;
        unitId = command.ExecuteScalar() == null ? 0 : (int)command.ExecuteScalar();
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        if (sqlConnection.State == ConnectionState.Open)
        {
          sqlConnection.Close();
        }
      }

      return unitId;
    }

    // Get unit serial from Inspect db
    public static string GetUnitSerial(SqlConnection sqlConnection, int unitId)
    {
      var serial = string.Empty;

      var sql = "select serial from units where unit_id = @unitId";

      try
      {
        using var command = new SqlCommand(sql, sqlConnection);
        sqlConnection.Open();
        command.Parameters.Add("unitId", SqlDbType.Int).Value = unitId;
        serial = command.ExecuteScalar() == DBNull.Value ? null : command.ExecuteScalar().ToString();
      }
      catch (Exception)
      {

        throw;
      }
      finally
      {
        if (sqlConnection.State == ConnectionState.Open)
        {
          sqlConnection.Close();
        }
      }

      return serial;
    }
  }
}
