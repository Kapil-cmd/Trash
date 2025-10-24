using System.Data;

using Core;
using Infrastructure;
using Microsoft.Data.SqlClient;

namespace Application
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<User> GetAllUsers()
        {
            var list = new List<User>();

            using(var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_USER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;  
                command.Parameters.AddWithValue("@Flag", "GetAllUsers");

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = Convert.ToInt64(reader["UserId"]),
                            UserName = reader["UserName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString()
                        };
                        list.Add(user);
                    }
                }
            }
            return list;    
        }
         
        public SpResponse RegisterUser(RegisterUserViewModel user)
        {
            var response = new SpResponse();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_USER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Pass parameters safely
                command.Parameters.AddWithValue("@Flag", "RegisterUser");
                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", user.EmailAddress ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@UserName", user.UserName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Password", user.Password ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        response.ErrorCode = reader["ErrorCode"].ToString();
                        response.Message = reader["Message"].ToString();
                    }
                    else
                    {
                        response.ErrorCode = "99";
                        response.Message = "No response from database.";
                    }
                }
            }

            return response;
        }
    }
}
