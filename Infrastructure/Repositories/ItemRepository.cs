
using Core;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace Infrastructure
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        private static string spName = "PROC_ITEM";
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
                string images = JsonConvert.SerializeObject(imageModel);
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "InsertItem");
                    command.Parameters.AddWithValue("@ItemName", model.ItemName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemStatus", model.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemDescription", model.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ItemTypeId", model.ItemTypeId);
                    command.Parameters.AddWithValue("@UserId", model.UserId);
                    command.Parameters.AddWithValue("@CreatedDateTime", model.CreatedDateTime ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Images", string.IsNullOrWhiteSpace(images) ? (object)DBNull.Value : images);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
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

        public SpResponse<List<ImageDetailViewModel>> GetItemList()
        {
            SpResponse<List<ImageDetailViewModel>> response = new SpResponse<List<ImageDetailViewModel>>();
            try
            {
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "AllItemList");

                    connection.Open();
                    var list = new List<ImageDetailViewModel>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var imageItem = new ImageDetailViewModel
                            {
                                Description = reader["Itemdescription"].ToString(),
                                ItemId = Convert.ToInt64(reader["Itemid"]),
                                ImageName = reader["ItemName"].ToString(),
                                ImageUrl = reader["Images"].ToString(),
                                ItemName = reader["ItemName"].ToString(),
                                CityName = reader["CityName"].ToString(),
                                StreetName= reader["StreetName"].ToString(),
                                PostalCode= reader["PostalCode"].ToString(),
                                HouseName= reader["HouseName"].ToString(),
                                CountyName= reader["CountyName"].ToString(),
                                Status = reader["ItemStatus"].ToString(),
                            };
                            var images = imageItem.ImageUrl.Split(",");
                            imageItem.Images = images.ToList();
                            list.Add(imageItem);
                        }
                    }
                    response.Data = list;
                }
                response.ErrorCode = "000";
                response.Message = "ITEM FETCH SUCCESSFULLY!!!";
                return response;
            }
            catch (Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "UNABLE TO GET LIST!!!!";
                return response;
            }
        }

        public SpResponse<string> UpdateItemStatus(UpdateItemStatus model)
        {
            var response = new SpResponse<string>(); try
            {
                using (var connection = _dbConnectionFactory.CreateConnection())
                using (var command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Flag", "ChangeStatus");
                    command.Parameters.AddWithValue("@ItemId", model.ItemId);
                    command.Parameters.AddWithValue("@UserId", model.UserId);
                    command.Parameters.AddWithValue("@ItemStatus", model.Status ?? (object)DBNull.Value);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.ErrorCode = reader["ErrorCode"].ToString();
                            response.Message = reader["ErrorMessage"].ToString();
                        };
                    }
                }
                return response;
            }catch(Exception ex)
            {
                response.ErrorCode = "1";
                response.Message = "UNABLE TO UPDATE ITEM STATUS!!!!";
                return response;
            }
        }
    }
}
