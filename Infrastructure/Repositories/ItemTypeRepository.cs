
using Core;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public ItemTypeRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public SpResponse<string> AddItemType(AddItemTypeViewModel model)
        {
            var response = new SpResponse<string>();
            using (var connection = _connectionFactory.CreateConnection())
            using (var command = new SqlCommand("PROC_ITEMTYPE", connection))
            {

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Flag", "Insert");
                command.Parameters.AddWithValue("@ItemTypeName", model.ItemTypeName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ItemDescription", model.Description ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy??(object)DBNull.Value);
                //command.Parameters.AddWithValue("@CreatedDateTime", model.CreatedDateTime ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@status", model.Status??(object) DBNull.Value);

                connection.Open();

                using(var reader = command.ExecuteReader())
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
                return response;
            }
        }

        public SpResponse<List<ItemTypesViewModel>> GetItemTypeList()
        {
            var response = new SpResponse<List<ItemTypesViewModel>>();
            try
            {
                var list = new List<ItemTypesViewModel>();
                using(var connection = _connectionFactory.CreateConnection())
                    using(var command = new SqlCommand("PROC_ITEMTYPE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "GetItemList");

                    connection.Open();

                    using(var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new ItemTypesViewModel
                            {
                                CreatedBy = reader["CreatedBy"].ToString(),
                                CreatedDateTime = Convert.ToDateTime(reader["CreatedDateTime"]).ToString("dd/MM/yyyy"),
                                Description = reader["Description"].ToString(),
                                ItemTypeName = reader["ItemTypeName"].ToString(),
                                Status = reader["Status"].ToString()
                            };
                            list.Add(model);
                        }
                    }
                    return response;
                }
            }catch(Exception ex)
            {
                response.ErrorCode = "99";
                response.Message = "No response from database.";
                return response;
            }
        }
    }
}
