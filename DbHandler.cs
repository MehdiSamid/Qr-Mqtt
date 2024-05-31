using System.Data.SqlClient;
using System.Threading.Tasks;
using System;

namespace demo_Qr_Mqtt
{
    public class DatabaseHandler
    {
        private readonly string _connectionString;

        public DatabaseHandler()
        {
            _connectionString = ConfigHelper.GetConnectionString("def");
        }

        public async Task InsertIntoDatabaseAsync(string cin, DateTime scanTime)
        {
            var query = "INSERT INTO Presence (studentId, datePresence) VALUES (@Cin, @ScanTime)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Cin", cin);
                command.Parameters.AddWithValue("@ScanTime", scanTime);

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    Console.WriteLine("Record inserted successfully into the database.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
