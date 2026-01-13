using Microsoft.EntityFrameworkCore;
using System.Data;
using DTO;
using System.Text;

namespace Api.Services
{
    public class SQLService
    {
        /// <summary>
        /// Returns a list of tables that have been updated since last time it was checked
        /// </summary>
        /// <param name="db"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<CacheDTO.Table> DirtyTables(Entities.MaxiContext db, List<CacheDTO.Table> serverTables, List<CacheDTO.Table> clientTables)
        {
            List<CacheDTO.Table> retval = new();
            var connectionDb = db.Database.GetDbConnection();
            if (connectionDb.State.Equals(ConnectionState.Closed))
                connectionDb.Open();
            using (var command = connectionDb.CreateCommand())
            {
                var sb = new StringBuilder();
                sb.AppendLine("SELECT name AS TableName, MAX(create_date) AS FchCreated, max(last_user_update) AS FchUpdated ");
                sb.AppendLine("FROM sys.tables ");
                sb.AppendLine("INNER JOIN sys.dm_db_index_usage_stats  ON sys.tables.Object_Id = sys.dm_db_index_usage_stats.object_id ");
                sb.AppendLine("WHERE ( ");
                sb.AppendLine(string.Join(" OR ", clientTables.Select(x => string.Format("sys.tables.name = '{0}'", ((CacheDTO.Table.TableIds)x.Id).ToString()))));
                sb.AppendLine(") ");
                sb.AppendLine("GROUP BY sys.tables.name ");
                var SqlCommand = sb.ToString();
                command.CommandText = SqlCommand;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader["TableName"].ToString() ?? "";
                                    DateTime fch = reader["FchUpdated"] is System.DBNull ? (DateTime)reader["FchCreated"] : (DateTime)reader["FchUpdated"];
                        var serverTable = serverTables.FirstOrDefault(x=>((CacheDTO.Table.TableIds)x.Id).ToString() == name);
                        
                        if (serverTable != null && (serverTable.Fch == null || serverTable.Fch  < fch) )
                            retval.Add(serverTable!);
                   }
                }
            }
            return retval;
        }
    }
}
