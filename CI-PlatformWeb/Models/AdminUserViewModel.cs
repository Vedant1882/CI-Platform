﻿using CI_Entity.Models;

namespace CI_PlatformWeb.Models
{
    public class AdminUserViewModel
    {
        public List<User> users { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string employeeid { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string department { get; set; }
        public IFormFile files { get; set; }
        public string avatar { get; set; }
        public string profiletext { get; set; }
        public long? cityid { get; set; }
        public long? userId { get; set; }
        public long? countryid { get; set; }
        public List<City> allcity { get; set; }
        public List<Country> allcountry { get; set; }

    }
}