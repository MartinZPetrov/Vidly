﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage="Please enter customer`s name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter { get; set; }
        
        [Display(Name="Date of Birth")]
        //[Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }
        public MembershipType MemberShipType { get; set; }
        
        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }
        

    }
}