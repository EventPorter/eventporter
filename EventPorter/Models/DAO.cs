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
            return 0;
        }



    }
}