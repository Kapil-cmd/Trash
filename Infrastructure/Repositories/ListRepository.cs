
using Core;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace Infrastructure
{
    public class ListRepository : IListRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public ListRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public SpResponse<List<SelectListItem>> GetItemTypeList()
        {
            var response = new SpResponse<List<SelectListItem>>();
            try
            {
                using (var connection = _connectionFactory.CreateConnection())
                using (var command = new SqlCommand("PROC_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "ItemTypeList");

                    connection.Open();
                    var list = new List<SelectListItem>();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var listItem = new SelectListItem()
                            {
                                Value = reader["ItemTypeId"].ToString(),
                                Text = reader["ItemTypeName"].ToString()
                            };
                            //response.Data.Add(listItem);
                            list.Add(listItem);
                            if(list.Count > 0)
                            {
                                response.ErrorCode = reader["ErrorCode"].ToString();
                                response.Message = reader["ErrorMessage"].ToString();
                            }
                            else
                            {
                                response.ErrorCode = "1";
                                response.Message = "UNABLE TO GET LIST!!!";
                            }

                        }
                        response.Data = list;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
    }
}
