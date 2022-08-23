﻿using System.ComponentModel.DataAnnotations;

namespace atmAPI.Models
{
    public class client
    {
        [Key]
        public int client_id { get; set; }
        public string username { get; set; }
        public string client_name { get; set; }
        public string client_surname { get; set; }
        public string client_phone { get; set; }
        public int pin { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int age { get; set; }
        public ICollection<account> accounts { get; set; }
    }
}