using System.Data;
using System.Security.AccessControl;
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

        public SpResponse<List<User>> GetAllUsers()
        {
            var list = new SpResponse<List<User>>();
            var userList = new List<User>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_USER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Flag", "UserList");

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var user = new User
                        {
                            UserId = Convert.ToInt64(reader["UserId"]),
                            FullName = reader["FullName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Email = reader["EmailAddress"].ToString(),
                        };
                        userList.Add(user);
                        if (reader.NextResult() && reader.Read())
                        {
                            list.ErrorCode = reader["ErrorCode"].ToString();
                            list.Message = reader["Message"].ToString();
                        }
                        list.Data = userList;
                        return list;
                    }
                }
            }
            return list;
        }
        public SpResponse<LoginResponseViewModel> Login(LoginViewModel model)
        {
            var response = new SpResponse<LoginResponseViewModel>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_USER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Flag", "Login");
                command.Parameters.AddWithValue("@UserName", model.UserName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Password", model.Password ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EmailAddress", model.UserName ?? (object)DBNull.Value);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["Message"].ToString();
                            if (reader.NextResult() && reader.Read())
                            {
                                response.Data = new LoginResponseViewModel
                                {
                                    UserName = reader["UserName"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    EmailAddress = reader["EmailAddress"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    Status = reader["Status"].ToString()
                                };
                            }

                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(response.ErrorCode))
            {
                response.ErrorCode = "99";
                response.Message = "Unexpected error occurred.";
            }

            return response;
        }


        public SpResponse<string> RegisterUser(RegisterUserViewModel user)
        {
            var response = new SpResponse<string>();

            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_USER", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


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
