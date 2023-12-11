using System;
using System.Collections.Generic;

namespace AgilePMC.Models
{
    public partial class ContactU
    {
        public long ContactUsId { get; set; }
        public string? ContactName { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? OrganizationName { get; set; }
        public string? ContactUsType { get; set; }
        public string? OrganizationType { get; set; }
        public string? AssociateFor { get; set; }
        public string? ApplyingFor { get; set; }
        public string? Email { get; set; }
        public string? BusinessProfille { get; set; }
        public string? Website { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Message { get; set; }
        public string? Qualification { get; set; }
        public long? Experiance { get; set; }
        public DateTime? CreatedAt { get; set; }
        public long? CreatedBy { get; set; }
        public string? Status { get; set; }
    }
}
