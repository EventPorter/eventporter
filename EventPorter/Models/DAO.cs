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
        #region User
        //Insert adam into database
        public int Insert(User adam)
        {
            //no of rows affected by insertion
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@firstname", adam.Firstname);
            cmd.Parameters.AddWithValue("@lastname", adam.Lastname);
            cmd.Parameters.AddWithValue("@username", adam.Username.ToLower());
            cmd.Parameters.AddWithValue("@email", adam.Email.ToLower());
            cmd.Parameters.AddWithValue("@dob", adam.DateOfBirth);
            //cmd.Parameters.AddWithValue("@userid", adam.UserId);
            cmd.Parameters.AddWithValue("@regDate", adam.RegDate);
            //cmd.Parameters.AddWithValue("@userType", adam.UserType);
            cmd.Parameters.AddWithValue("@userType", (int) Role.User);
            password = Crypto.HashPassword(adam.Password);
            //message = password;
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

        public User CheckLogin(User user)
        {
            if (user == null || user.Username == null || user.Password == null)
                return null;

            User result = null;
            
            SqlCommand cmd;
            SqlDataReader reader;
            string password;
            Connection();
            cmd = new SqlCommand("uspCheckLogin", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", user.Username.ToLower());
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Password"].ToString();
                    if (Crypto.VerifyHashedPassword(password, user.Password))
                    {
                        result = new User();
                        result.Username = user.Username;
                        result.UserId = int.Parse(reader["UserId"].ToString());
                        result.UserType = (Role)int.Parse(reader["UserType"].ToString());
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

            return result;
        }

        public User GetUserInfo(int userID)
        {
            User user = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetUserInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", user.UserId);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new User();
                    user.UserId = userID;
                    user.Firstname = reader["FirstName"].ToString();
                    user.Lastname = reader["LastName"].ToString();
                    user.Username = reader["UserName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.DateOfBirth = reader.GetDateTime(4);
                    user.RegDate = reader.GetDateTime(5);
                    user.Location = reader["Location"].ToString();
                    int type = int.Parse(reader["UserType"].ToString());
                    user.UserType = (Role)type;
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
            return user;
        }

        public Adam getInfo(int userID)
        {
            Adam user = null;
            return user;
        }
        #endregion


    }
}