using Moviza.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moviza.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        //[Min18YearsIfAMember]
        public DateTime Birthday { get; set; }

       [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

       

           }
}