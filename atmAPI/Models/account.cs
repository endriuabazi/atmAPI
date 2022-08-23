﻿using System.ComponentModel.DataAnnotations;

namespace atmAPI.Models
{
    public class account
    {
        [Key]
        public int account_id { get; set; }
        public string account_name { get; set; }
        public int balance { get; set; }
        public char currency { get; set; }
        public client client { get; set; }

        public ICollection<transaction> transactions { get; set; }

    }
}