using Moviza.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Moviza.ViewModels
{
    public class NewCustomerViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        //public Customer Customer { get; set; }

        public int? Id { get; set; }
        [Required(ErrorMessage = "please ener customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
        public MembershipType MembershipType { get; set; }
        [Display(Name = "Membership Type")]
        public byte? MembershipTypeId { get; set; }


        public string Title {
            get
            {
                if (this.Id != 0 ) return "Edit Customer";

                return "New Customer";
            }
        }

        public NewCustomerViewModel()
        {
            if (this.Id == 0)
                this.Birthday = null;
        }

        public NewCustomerViewModel(Customer customer)
        {
            this.Id = customer.Id;
            this.MembershipTypeId = customer.MembershipTypeId;
            this.Name = customer.Name;
            this.Birthday = customer.Birthday;
            this.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
        }

    }

    
}