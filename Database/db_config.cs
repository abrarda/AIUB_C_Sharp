using MySql.Data.MySqlClient;
using System;
using System.IO;
using UniProject.Utilities;

namespace UniProject.Database
{
    public static class db_config
    {
        private static readonly string server = "localhost";
        private static readonly string database = "expensetrackerdb";
        private static readonly string username = "root";
        private static readonly string password = "root";

        public static MySqlConnection GetConnection()
        {
            string connectionString = $"Server={server};Database={database};User ID={username};Password={password};";
            return new MySqlConnection(connectionString);
        }
    }

    public static class DatabaseSetup
    {
        public static void CreateDatabaseAndTables()
        {
            using (var connection = db_config.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Create Database
                    string createDatabaseSql = "CREATE DATABASE IF NOT EXISTS expensetrackerdb;";
                    using (var cmd = new MySqlCommand(createDatabaseSql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Use the created Database
                    connection.ChangeDatabase("expensetrackerdb");

                    // Create Users Table
                    string createTableSql = @"
                        DROP TABLE IF EXISTS users;
                        CREATE TABLE users (
                            id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
                            username VARCHAR(50) NOT NULL UNIQUE,
                            email VARCHAR(100) NOT NULL UNIQUE,
                            password_hash VARCHAR(255) NOT NULL,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );";

                    using (var cmd = new MySqlCommand(createTableSql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Handle MySQL errors
                    MessageBoxUtility.ShowErrorMessage("MySQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other errors
                    MessageBoxUtility.ShowErrorMessage("Error: " + ex.Message);
                }
            }
        }
    }
}