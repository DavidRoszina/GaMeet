﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GaMeet.Models
{
    public class Gamer
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State is Required")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is Required")]
        public string ZipCode { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
