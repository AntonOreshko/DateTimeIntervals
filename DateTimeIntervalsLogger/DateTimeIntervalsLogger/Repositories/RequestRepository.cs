using System;
using System.Data;
using System.Data.SqlClient;
using DateTimeIntervals.Dtos.Dtos;
using Microsoft.Extensions.Configuration;

namespace DateTimeIntervalsLogger.Repositories
{
    public class RequestRepository: IRequestRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public RequestRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("LoggerConnection");
        }

        public void AddRequestData(RequestDto data)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddRequest", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@UserId", data.UserId);
                cmd.Parameters.AddWithValue("@ResponseCode", data.ResponseCode);
                cmd.Parameters.AddWithValue("@RequestMethod", data.RequestMethod);
                cmd.Parameters.AddWithValue("@RequestPath", data.RequestPath);
                cmd.Parameters.AddWithValue("@RequestTime", data.RequestTime);
                cmd.Parameters.AddWithValue("@ResponseTime", data.ResponseTime);
                cmd.Parameters.AddWithValue("@RequestProtocol", data.RequestProtocol);

                SqlParameter requestBody = new SqlParameter("@RequestBody", SqlDbType.NVarChar)
                {
                    Value = (object) data.RequestBody ?? DBNull.Value
                };
                cmd.Parameters.Add(requestBody);

                SqlParameter responseBody = new SqlParameter("@ResponseBody", SqlDbType.NVarChar)
                {
                    Value = (object)data.ResponseBody ?? DBNull.Value
                };
                cmd.Parameters.Add(responseBody);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
