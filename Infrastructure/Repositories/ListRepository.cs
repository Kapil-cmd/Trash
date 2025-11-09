using Core;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
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

        public SpResponse<List<SelectListItem>> GetCountryList()
        {
            var response = new SpResponse<List<SelectListItem>>();
            try
            {
                var list = new List<SelectListItem>();

                using (var connection = _connectionFactory.CreateConnection())
                using (var command = new SqlCommand("PROC_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "CountryList");

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            list.Add(new SelectListItem
                            {
                                Text = reader["CountryName"].ToString(),
                                Value = reader["CountryId"].ToString()
                            });
                        }
                    }
                }

                if (list.Count > 0)
                {
                    response.ErrorCode = "000";
                    response.Message = "LIST FETCHED SUCCESSFULLY!!!";
                    response.Data = list;
                }
                else
                {
                    response.ErrorCode = "1";
                    response.Message = "NO RECORDS FOUND!";
                }

                return response;
            }
            catch (Exception)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<List<SelectListItem>> GetCountyList(long? countryId)
        {
            var response = new SpResponse<List<SelectListItem>>();
            try
            {
                var list = new List<SelectListItem>();

                using (var connection = _connectionFactory.CreateConnection())
                using (var command = new SqlCommand("PROC_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "CountyList");
                    command.Parameters.AddWithValue("@CountryId", countryId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            list.Add(new SelectListItem
                            {
                                Text = reader["CountyName"].ToString(),
                                Value = reader["CountyId"].ToString()
                            });
                        }
                    }
                }

                if (list.Count > 0)
                {
                    response.ErrorCode = "000";
                    response.Message = "LIST FETCHED SUCCESSFULLY!!!";
                    response.Data = list;
                }
                else
                {
                    response.ErrorCode = "1";
                    response.Message = "NO RECORDS FOUND!";
                }

                return response;
            }
            catch (Exception)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }

        public SpResponse<List<SelectListItem>> GetItemTypeList()
        {
            var response = new SpResponse<List<SelectListItem>>();
            try
            {
                var list = new List<SelectListItem>();

                using (var connection = _connectionFactory.CreateConnection())
                using (var command = new SqlCommand("PROC_List", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Flag", "ItemTypeList");

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            list.Add(new SelectListItem
                            {
                                Value = reader["ItemTypeId"].ToString(),
                                Text = reader["ItemTypeName"].ToString()
                            });
                        }
                    }
                }

                if (list.Count > 0)
                {
                    response.ErrorCode = "000";
                    response.Message = "LIST FETCHED SUCCESSFULLY!!!";
                    response.Data = list;
                }
                else
                {
                    response.ErrorCode = "1";
                    response.Message = "NO RECORDS FOUND!";
                }

                return response;
            }
            catch (Exception)
            {
                response.ErrorCode = "1";
                response.Message = "TECHNICAL ERROR OCCURRED WHILE PROCESSING YOUR REQUEST!!!";
                return response;
            }
        }
    }
}
