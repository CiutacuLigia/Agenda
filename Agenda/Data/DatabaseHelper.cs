using Agenda.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


     
namespace Agenda.Components.Data
    {
        public static class DatabaseHelper
        {
            private static string connectionString;

            static DatabaseHelper()
            {
                // Obține conexiunea din configurația aplicației
                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                             .AddJsonFile("appsettings.json")
                                                             .Build();
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            // Obține toți utilizatorii asincron
            public static async Task<List<utilizator>> GetAllUtilizatoriAsync()
            {
                var lista = new List<utilizator>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Utilizator";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new utilizator
                            {
                                Id = (int)reader["Id"],
                                Nume = reader["Nume"]?.ToString(),
                                Prenume = reader["Prenume"]?.ToString(),
                                Telefon = reader["Telefon"]?.ToString(),
                                Birthday = reader["Birthday"] as DateTime?,
                                Observatie = reader["Observatie"]?.ToString(),
                                Relatie = reader["Relatie"]?.ToString()
                            });
                        }
                    }
                }
                return lista;
            }


        // Verifică dacă numărul de telefon există deja
        public static bool NumarExistent(string telefon)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Utilizator WHERE Telefon = @Telefon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Telefon", telefon);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Adaugă un utilizator
        public static async Task AddUtilizator(utilizator utilizator)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Utilizator (Nume, Prenume, Telefon, Birthday, Observatie, Relatie) VALUES (@Nume, @Prenume, @Telefon, @Birthday, @Observatie, @Relatie)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nume", utilizator.Nume);
                cmd.Parameters.AddWithValue("@Prenume", utilizator.Prenume);
                cmd.Parameters.AddWithValue("@Telefon", utilizator.Telefon);
                cmd.Parameters.AddWithValue("@Birthday", utilizator.Birthday);
                cmd.Parameters.AddWithValue("@Observatie", utilizator.Observatie);
                cmd.Parameters.AddWithValue("@Relatie", utilizator.Relatie);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task DeleteUtilizator(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Utilizator WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async Task UpdateUtilizator(utilizator utilizator)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = @"UPDATE Utilizator 
                         SET Nume = @Nume, Prenume = @Prenume, Telefon = @Telefon, 
                             Birthday = @Birthday, Observatie = @Observatie, Relatie = @Relatie 
                         WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", utilizator.Id);
                    cmd.Parameters.AddWithValue("@Nume", (object?)utilizator.Nume ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Prenume", (object?)utilizator.Prenume ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Telefon", (object?)utilizator.Telefon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Birthday", utilizator.Birthday.HasValue ? (object)utilizator.Birthday.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Observatie", (object?)utilizator.Observatie ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Relatie", (object?)utilizator.Relatie ?? DBNull.Value);

                    Console.WriteLine($"[DEBUG] UPDATE utilizator ID={utilizator.Id}");
                    Console.WriteLine($"        Nume={utilizator.Nume}, Prenume={utilizator.Prenume}, Telefon={utilizator.Telefon}");
                    Console.WriteLine($"        Birthday={utilizator.Birthday}, Observatie={utilizator.Observatie}, Relatie={utilizator.Relatie}");

                    try
                    {
                        int rows = await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine($"Updated rows: {rows}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] {ex.Message}");
                    }
                }
            }
        }



        public static async Task<utilizator?> GetUtilizatorByIdAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Utilizator WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                await conn.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new utilizator
                        {
                            Id = (int)reader["Id"],
                            Nume = reader["Nume"]?.ToString(),
                            Prenume = reader["Prenume"]?.ToString(),
                            Telefon = reader["Telefon"]?.ToString(),
                            Birthday = reader["Birthday"] as DateTime?,
                            Observatie = reader["Observatie"]?.ToString(),
                            Relatie = reader["Relatie"]?.ToString()
                        };
                    }
                }
            }
            return null;
        }

    }
}






