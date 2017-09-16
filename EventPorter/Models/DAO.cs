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
            cmd.Parameters.AddWithValue("@userType", (int) adam.UserType);
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

        public User GetUserInfo(string username)
        {
            User user = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetUserInfo", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user = new User();
                    user.Username = username;
                    user.UserId = int.Parse(reader["ID"].ToString());
                    user.Firstname = reader["FirstName"].ToString();
                    user.Lastname = reader["LastName"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.DateOfBirth = reader.GetDateTime(4);
                    user.RegDate = reader.GetDateTime(5);
                    user.Location = reader["Location"].ToString();
                    user.UserType = (Role)int.Parse(reader["UserType"].ToString());
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

        #region Event
        public int Insert(Event newEvent)
        {
            //no of rows affected by insertion
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //(@creatorName, @title, @description, @startdateandtime, @enddateandtime, @price, @thumbnail, @longitude, @latitude)
            cmd.Parameters.AddWithValue("@creatorName", newEvent.CreatorUserName);
            cmd.Parameters.AddWithValue("@title", newEvent.Title);
            cmd.Parameters.AddWithValue("@description", newEvent.Description);
            cmd.Parameters.AddWithValue("@startdateandtime", newEvent.StartDateAndTime);
            cmd.Parameters.AddWithValue("@enddateandtime", newEvent.EndDateAndTime);
            cmd.Parameters.AddWithValue("@price", newEvent.Price);
            cmd.Parameters.AddWithValue("@thumbnail", newEvent.Thumbnail);
            cmd.Parameters.AddWithValue("@longitude", newEvent.Longitude);
            cmd.Parameters.AddWithValue("@latitude", newEvent.Latitude);

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

        public Event GetEvent(int id)
        {
            Event _event = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eventid", id);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _event = new Event();
                    _event.ID = int.Parse(reader["ID"].ToString());
                    _event.CreatorUserName = reader["CreatorUserName"].ToString();
                    _event.Title = reader["Title"].ToString();
                    _event.Description = reader["Description"].ToString();
                    _event.Thumbnail = reader["Thumbnail"].ToString();
                    _event.StartDateAndTime = reader.GetDateTime(4);
                    _event.EndDateAndTime = reader.GetDateTime(5);
                    _event.Price = decimal.Parse(reader["Price"].ToString());
                    _event.Longitude = float.Parse(reader["Longitude"].ToString());
                    _event.Latitude = float.Parse(reader["Latitude"].ToString());
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
            return _event;
        }

        public List<Event> SearchEvents(string searchString)
        {
            List<Event> events = new List<Event>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspUserEventSearch", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@searchString", searchString);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //[Event].[Title], [Event].[Thumbnail], [Event].[Description], [Event].[ID] FROM[Event] WHERE[Event].[Title]
                    Event _event = new Event();
                    _event.ID = int.Parse(reader["ID"].ToString());
                    _event.Title = reader["Title"].ToString();
                    _event.Description = reader["Description"].ToString();
                    _event.Thumbnail = reader["Thumbnail"].ToString();
                    events.Add(_event);
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
            return events;
        }
        #endregion

    }
}