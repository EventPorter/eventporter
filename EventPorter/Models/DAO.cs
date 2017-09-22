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
        private static DAO dao;
        SqlConnection conn;
        public string message { get; set; }
        
        private DAO()
        {
        }

        public static DAO GetInstance()
        {
            if (dao == null)
                dao = new DAO();
            return dao;
        }

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
            cmd.Parameters.AddWithValue("@regDate", adam.RegDate);
            cmd.Parameters.AddWithValue("@thumbnailID", 1);
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
                    //user.Location = reader["Location"].ToString();
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

        public User getInfo(int userID)
        {
            User user = null;
            return user;
        }
        #endregion

        #region Event
        public int Insert(Event newEvent)
        {
            //no of rows affected by insertion
            int id = -1;
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
            cmd.Parameters.AddWithValue("@thumbnailID", newEvent.ThumbnailID);
            cmd.Parameters.AddWithValue("@price", newEvent.Price);
            cmd.Parameters.AddWithValue("@locationDesc", newEvent.LocationDesc);
            cmd.Parameters.AddWithValue("@longitude", newEvent.Longitude);
            cmd.Parameters.AddWithValue("@latitude", newEvent.Latitude);

            try
            {
                conn.Open();
                id = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return id;
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
                    _event.ThumbnailID = int.Parse(reader["ThumbnailID"].ToString());
                    _event.StartDateAndTime = reader.GetDateTime(4);
                    _event.EndDateAndTime = reader.GetDateTime(5);
                    _event.Price = decimal.Parse(reader["Price"].ToString());
                    _event.Longitude = float.Parse(reader["Longitude"].ToString());
                    _event.Latitude = float.Parse(reader["Latitude"].ToString());
                    _event.LocationDesc = reader["LocationDesc"].ToString();
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

        public int RemoveEvent(int eventID)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspRemoveEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@eventID", eventID);
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

        public List<Event> SearchEvents(string searchString)
        {
            List<Event> events = new List<Event>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspEventSearch", conn);
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
                    _event.ThumbnailID = int.Parse(reader["ThumbnailID"].ToString());
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

        public List<Event> SearchUserEvents(string userID)
        {
            List<Event> events = new List<Event>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetUserCreatedEvents", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", userID);
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
                    _event.ThumbnailID = int.Parse(reader["ThumbnailID"].ToString());
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

        public int GetNoOfUserEvents(string username)
        {
            SqlCommand cmd;
            SqlDataReader reader;
            int count = -1;
            Connection();
            cmd = new SqlCommand("uspGetNoOfUserEvents", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            try
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
                

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
            return count;
        }

        public List<Event> GetUpcomingEvents()
        {
            List<Event> events = new List<Event>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetEventByDate", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Event _event = new Event();
                    _event.ID = int.Parse(reader["ID"].ToString());
                    _event.Title = reader["Title"].ToString();
                    _event.Description = reader["Description"].ToString();
                    _event.ThumbnailID = int.Parse(reader["ThumbnailID"].ToString());
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

            var orderedUpcoming = (from e in events
                                   orderby e.StartDateAndTime
                                   select e).ToList();

            return orderedUpcoming;
        }

        #endregion

        #region Image
        public int Insert(Image img)
        {
            int id = -1;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertImage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.AddWithValue("@filepath", img.FilePath);
            try
            {
                conn.Open();
                id = (int) cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return id;
        }

        public int Insert(EventImage eventImg)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertEventImage", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eventID", eventImg.EventID);
            cmd.Parameters.AddWithValue("@imageID", eventImg.ImageID);
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

        public List<Image> GetEventGalleryImages(int eventID)
        {
            List<Image> images = new List<Image>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetEventGalleryImages", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eventID", eventID);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Image img = new Image();
                    img.ID = int.Parse(reader["ID"].ToString());
                    img.FilePath = reader["FilePath"].ToString();
                    images.Add(img);
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
            return images;
        }

        public Image GetImage(int id)
        {
            Image img = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetImage", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@imageID", id);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    img = new Image();
                    img.FilePath = reader["FilePath"].ToString();
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
            return img;
        }
        #endregion

        #region Cart
        public int Insert(CartItem item)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspAddToCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eventID", item.EventID);
            cmd.Parameters.AddWithValue("@userID", item.UserID);
            cmd.Parameters.AddWithValue("@confirmationStatus", (int)item.Confirmed);

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

        public int Remove(CartItem item)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspRemoveItemFromCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", item.UserID);
            cmd.Parameters.AddWithValue("@eventID", item.EventID);
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

        public int GetNumberOfItemsInCart(int userID)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspGetNumItemsInCart", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", userID);
            try
            {
                conn.Open();
                count = (int) cmd.ExecuteScalar();
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

        public List<CartEvent> GetCartItems(int userID)
        {
            List<CartEvent> itemsInCart = new List<CartEvent>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetCartItems", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", userID);
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //[Event].[Title], [Event].[Thumbnail], [Event].[Description], [Event].[ID] FROM[Event] WHERE[Event].[Title]
                    CartEvent _event = new CartEvent();
                    _event.ID = int.Parse(reader["ID"].ToString());
                    _event.Title = reader["Title"].ToString();
                    _event.Price = decimal.Parse(reader["Price"].ToString());
                    itemsInCart.Add(_event);
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
            return itemsInCart;
        }

        public int ConfirmCartItem(CartItem item)
        {
            // count
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspSetItemConfirmedStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", item.UserID);
            cmd.Parameters.AddWithValue("@eventID", item.EventID);
            cmd.Parameters.AddWithValue("@confirmedStatus", (int) item.Confirmed);
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

        public bool CheckIfUserIsAttendingEvent(int userID, int eventID)
        {
            bool result = false;
            //SqlDataReader reader;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspIsAttendingEvent", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@eventID", eventID);
            try
            {
                conn.Open();
                object objResult = cmd.ExecuteScalar();
                if (objResult == null)
                    return false;

                result = (bool)objResult;
                //    while (reader.Read())
                //    {
                //        int result = int.Parse(reader["Confirmed"].ToString());
                //        attending = result == (int)Confirmed.Yes;
                //    }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
        #endregion

        #region Thumbnail
        public int UpdateUserThumbnail(string username, int thumbID)
        {
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspUpdateUserThumbnail", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@thumbID", thumbID);
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

        public string GetUserThumbnail(string username)
        {
            string thumbnailPath = null;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspGetUserThumbnail", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            try
            {
                conn.Open();
                thumbnailPath = cmd.ExecuteScalar() as string;
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return thumbnailPath;
        }
        #endregion
    }
}