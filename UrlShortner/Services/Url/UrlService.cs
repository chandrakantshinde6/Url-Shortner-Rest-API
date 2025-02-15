﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Formatters;
using shortid;
using shortid.Configuration;
using UrlShortner.Models;

namespace UrlShortner.Services.Url
{
    public class UrlService : IUrlServices
    {

        private static readonly string ConnectionString = "Server=CHANDRAKANT;Database=UrlShortner;Trusted_Connection=True;";
        
        
        public ResponseUrl GetByShortURL(string shortId)
        {
            return GetURlFromDBByShortUrl(shortId);
        }

        public string CreateShortUrl(RequestUrl url)
        {
            var options = new GenerationOptions(useSpecialCharacters: false, length: 8);
            string shortUrl = ShortId.Generate(options);

            string query = $"insert into urls(userId, originalUrl, shortUrl) values( '{url.UserId}', '{url.OriginalUrl}', '{shortUrl}')";

            using (SqlConnection con = 
                   new SqlConnection(ConnectionString))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = query;
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        
                        Console.WriteLine(e.Message);
                        shortUrl = null;
                    }
                }
            }

            return shortUrl;
        }

        public List<ResponseUrl> GetUrlsByUserId(string userId)
        {
            List<ResponseUrl> list = new List<ResponseUrl>();

            string query = $"select * from urls where userId = '{userId}'";

            using (SqlConnection con =
                   new SqlConnection(ConnectionString))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = query;
                    try
                    {
                        using (var reader = tableCmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    list.Add(ReadUrl(reader));
                                }
                               
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        
                        Console.WriteLine(e);
                    }
                }


            }

            return list;

        }

        public string DeleteUrlByShortId(string shortId)
        {
            string query = $"delete from urls where shortUrl='{shortId}'";

            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var tableCmd = sqlConnection.CreateCommand())
                {
                    sqlConnection.Open();
                    try
                    {
                        tableCmd.CommandText = query;
                        int affectedRow = tableCmd.ExecuteNonQuery();
                        if (affectedRow == 0)
                        {
                            return $"Id {shortId} doesn't exist";
                        }

                        return $"Id {shortId} deleted Successfully";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Something went wrong while deleting :{shortId}, \n Error is : {e.Message}");
                        return null;
                    }
                    
                }
            }
            
        }


        public string UpdateUrlByShortId(string shortId, string userId)
        {
            string Output;
            string query = $"delete from urls where shorturl = '{shortId}' and userId = '{userId}'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        tableCmd.CommandText = query;
                        int result = tableCmd.ExecuteNonQuery();
                        if (result == 0)
                        {
                            Output = $"unable to delete, ShortID {shortId}";
                        }
                        else Output = $"ShortID {shortId} deleted successfully";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Something went wrong while deleting shortID : {shortId}, Error = {e.Message}");
                        Output = "Server error";
                    }
                }   
                
            }

            return Output;
        }


        private ResponseUrl GetURlFromDBByShortUrl(string shortId)
        {
              
            ResponseUrl responseUrl = new ResponseUrl();
            string query = $"select * from urls where shorturl = '{shortId}';";
            
            using (SqlConnection con = 
                   new SqlConnection(ConnectionString))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = query;
                    try
                    {
                        using (var reader = tableCmd.ExecuteReader())
                        {
                            
                          
                            if (reader.HasRows)
                            {
                                reader.Read();
                                responseUrl = ReadUrl(reader);
                                // if( reader.GetDateTime(6) != null)
                                //     responseUrl.UpdatedAtDateTime =  reader.GetDateTime(6);
                            }
                            else responseUrl = null;

                        }
                    }
                    catch (Exception e)
                    {
                        
                        Console.WriteLine(e);
                        responseUrl = null;
                    }
                }
            }

            return responseUrl;
        }

        private ResponseUrl ReadUrl(SqlDataReader reader)
        {
            ResponseUrl responseUrl = new ResponseUrl();
            
            responseUrl.UrlId = (int) reader["id"];
            responseUrl.UserId = (string) reader["userId"];
            responseUrl.OriginalUrl = (string) reader["originalUrl"];
            responseUrl.ShortUrl = (string) reader["shortUrl"];
            responseUrl.CreatedAtDateTime = (DateTime) reader["createdAt"];
            
            responseUrl.ExpiryDateTime = (DateTime) reader["expiryDate"];
            
            if( reader["updatedAt"] != DBNull.Value)
                responseUrl.UpdatedAtDateTime = (DateTime) reader["updatedAt"];

            return responseUrl;
        }
    }
}