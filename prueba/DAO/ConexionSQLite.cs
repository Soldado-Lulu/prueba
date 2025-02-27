using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba.DAO
{
    public static class ConexionSQLite
    {
        private static readonly string cadena = ConfigurationManager.ConnectionStrings["cadena"].ConnectionString;

        public static SQLiteConnection ObtenerConexion()
        {
            return new SQLiteConnection(cadena);
        }
    }
}
