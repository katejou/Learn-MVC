﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Chart_ex.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name必須輸入")]
        public string Name { get; set; }

        [Display(Name="暱稱")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Password必須輸入")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int Gender { get; set; }

        public int City { get; set; }

        [DataType(DataType.Url)]
        public string Blog { get; set; }

        public string Commutermode { get; set; }

        public string Comment { get; set; }

        public bool Terms { get; set; }

    }
}