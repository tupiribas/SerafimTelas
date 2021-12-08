using System;
using MySql.Data.MySqlClient;


namespace SerafTelis.db
{
    public static class DB
    {
        private static MySqlConnection conn;

        private const string SERVER = "localhost";
        private const string PORT = "3306";
        private const string USER_ID = "root";
        private const string DATABASE = "serafimtelas";
        private const string PASSWORD = "";

        public static MySqlConnection GetConnection()
        {
            if (conn == null)
            {
                try
                {
                    conn = new MySqlConnection("server=" + SERVER + 
                                               ";port=" + PORT + 
                                               ";User Id=" + USER_ID + 
                                               ";database=" + DATABASE + 
                                               ";password=" + PASSWORD);
                }
                catch (MySqlException error)
                {
                    throw new BDException("Desculpe, ouve uma falha na conexão com seus dados. " +
                        "Estamos resolvendo. >>> Cod.:00c1 \n" + error);
                }
            }
            return conn; 
        }
    }
}
