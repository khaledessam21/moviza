using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moviza.Models
{
    public class Customer
    {

        public int Id { get; set; }
        [Required(ErrorMessage ="please ener customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Display(Name="Date of Birth")]
      [Min18YearsIfAMember]
        public DateTime Birthday { get; set; }
        public MembershipType MembershipType { get; set; }
        [Display(Name="Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}
//1900-01-01T00:00:00.000