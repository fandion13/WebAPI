using System.Data.SqlClient;
using System;
using System.Configuration;
using System.Data;
using WebAPI.Camadas.Entidades;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data.Common;

namespace WebAPI.Camadas.Dados
{
    public class DTask
    {
        private readonly string _connectionString;

        public DTask(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BancoSQL");
        }

        public List<TaskModel> GetAllTasks()
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                using (connection)
                {
                    string query = "SELECT * FROM TASK_TABLE";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<TaskModel> tasks = new List<TaskModel>();

                    while (reader.Read())
                    {
                        tasks.Add(new TaskModel
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            Description = (string)reader["Description"],
                            Completed_at = reader["Completed_at"] != DBNull.Value ? (DateTime)reader["Completed_at"] : null,
                            Created_at = reader["Created_at"] != DBNull.Value ? (DateTime)reader["Created_at"] : null,
                            Updated_at = reader["Updated_at"] != DBNull.Value ? (DateTime)reader["Updated_at"] : null
                        });
                    }

                    connection.Close();

                    return tasks;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        public TaskModel GetTaskById(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                using (connection)
                {
                    string query = "SELECT * FROM TASK_TABLE WHERE id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new TaskModel
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            Description = (string)reader["Description"],
                            Completed_at = reader["Completed_at"] != DBNull.Value ? (DateTime)reader["Completed_at"] : null,
                            Created_at = reader["Created_at"] != DBNull.Value ? (DateTime)reader["Created_at"] : null,
                            Updated_at = reader["Updated_at"] != DBNull.Value ? (DateTime)reader["Updated_at"] : null
                        };
                    }

                    connection.Close();

                    return null;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        public TaskModel CreateTask(TaskModel task)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                using (connection)
                {
                    string query = "INSERT INTO TASK_TABLE (title, description, created_at) OUTPUT INSERTED.id VALUES (@Title, @Description, @Created_at)";
                 
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@Created_at", DateTime.Now);

                    connection.Open();

                    task.Id = (int)command.ExecuteScalar();

                    connection.Close();

                    return task;
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTask(int id, TaskModel task)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                using (connection)
                {
                    string query = "UPDATE TASK_TABLE SET title = @Title, description = @Description WHERE id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteTask(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);

            try
            {
                using (connection)
                {
                    string query = "DELETE FROM TASK_TABLE WHERE Id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                throw new Exception(ex.Message);
            }

        }

    }
}