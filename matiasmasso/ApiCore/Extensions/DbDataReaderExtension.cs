using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Api.Extensions
{
    public static class DbDataReaderExtension
    {
        public static string? GetNullableString(this DbDataReader reader, string colname)
        {
            if (!reader.IsDBNull(colname))
                return reader.GetString(colname);
            return null;
        }
        public static int? GetNullableInt32(this DbDataReader reader, string colname)
        {
            if (!reader.IsDBNull(colname))
                return reader.GetInt32(colname);
            return null;
        }
        public static decimal? GetNullableDecimal(this DbDataReader reader, string colname)
        {
            if (!reader.IsDBNull(colname))
                return reader.GetDecimal(colname);
            return null;
        }

        public static Guid? GetNullableGuid(this DbDataReader reader, string colname)
        {
            if (!reader.IsDBNull(colname))
                return reader.GetGuid(colname);
            return null;

        }
    }
}
