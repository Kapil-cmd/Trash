
using Core;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace Infrastructure
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public ItemRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public SpResponse<string> AddItem(AddItemViewModel model)
        {
            SpResponse<string> response = new SpResponse<string>();
            try
            {
                var imageModel = model.Image;
                string images = JsonConvert.
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand("PROC_ITEM", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "InsertItem");
                    command.Parameters.AddWithValue("@ItemName", model.ItemName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemStatus", model.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemDescription", model.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedDateTime", model.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Images", imageModel ?? (object)DBNull.Value);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErroMessage"].ToString();
                        }
                    }
                }
                    return response;
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING REQUEST!!!";
                return response;
            }
        }
    }
}
