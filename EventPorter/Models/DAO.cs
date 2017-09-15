using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration;
using EventPorter.Models;
using System.Data;
using System.Web.Helpers;

namespace EventPorter.Models
{
    public class DAO
    {
        SqlConnection conn;
        public string message;
        
        
        //MAKE SURE THERE IS A VALID CONNECTION STRING IN THE WEBCONFIG FILE POINTING TO THE LOCAL DB
        //Create a connection object
        public void Connection()
        {
            conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["connStringLocal"].ConnectionString);
        }
        #region Adam
        //Insert adam into database
        public int Insert(Adam adam)
        {
            //no of rows affected by insertion
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertAdam", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", adam.Username);
            cmd.Parameters.AddWithValue("@email", adam.Email);
            cmd.Parameters.AddWithValue("@userid", adam.UserId);
            cmd.Parameters.AddWithValue("@regDate", adam.RegDate);
            cmd.Parameters.AddWithValue("@userType", adam.UserType);
            password = Crypto.HashPassword(adam.Password);
            message = password;
            cmd.Parameters.AddWithValue("@pass", password);
            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {

                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public string CheckLogin(Adam user)
        {
            string username = null;
            SqlCommand cmd;
            SqlDataReader reader;
            string password;
            Connection();
            cmd = new SqlCommand("uspCheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", user.UserId);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Password"].ToString();
                    if (Crypto.VerifyHashedPassword(password, user.Password))
                    {
                        username = reader["Username"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            catch (FormatException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return username;

        }
        #endregion


    }
}