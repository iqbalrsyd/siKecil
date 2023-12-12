using System;
using System.Data.SqlClient;

namespace siKecil
{
    class Connection
    {
        public SqlConnection GetConn()
        {
            SqlConnection sqlCon = new SqlConnection();
            sqlCon.ConnectionString = "Server=tcp:sqldatabasesikecil.database.windows.net,1433; " +
                "Database=DB_siKecil; User ID=adminsiKecil; Password=siKecil123; Encrypt=True; " +
                "TrustServerCertificate=False; Connection Timeout=15";
            return sqlCon;
        }
    }
}