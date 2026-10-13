using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace CatShowDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // DB info
            ip = "192.168.1.105";
            port = "5432";
            db_name = "CatShowDB";
            username = "postgres";
            password = "Cozy_Cap2602:D";
            
            // Connection string
            connetion = $"Host={ip};Port={port};Database={db_name};Username={username};Password={password};";
        }
    }
}