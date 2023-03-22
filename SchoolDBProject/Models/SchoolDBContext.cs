using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace SchoolDBProject.Models
{
    public class SchoolDBContext
    {
        //read-only properties for SchoolDBContext
        //for linking to local school database
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                return
                    "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }

        //method for getting the database
        public MySqlConnection AccessDatabase()
        {
            //Instantiating MySqlConnection Class to create new object that connects to School database on port 3306 of localhost
            return new MySqlConnection(ConnectionString);
        }
    }
}