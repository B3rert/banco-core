using Microsoft.Data.SqlClient;

namespace banco_core.Utilities
{
    public static class SqlDataReaderExtensions
    {
        public static T? GetValueOrDefault<T>(this SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            if (value != DBNull.Value)
            {
                return (T)value;
            }
            return default;
        }

        public static T? GetValueOrDefaulInt<T>(this SqlDataReader reader, int indexColumn)
        {
            object value = reader[indexColumn];
            if (value != DBNull.Value)
            {
                return (T)value;
            }
            return default;
        }
    }
}
