using System;
using System.Collections.Generic;

namespace AgilePMC.Models
{
    public partial class SmtpDetail
    {
        public long SmtpDetailId { get; set; }
        public string? SmtpDomainName { get; set; }
        public long? SmtpPort { get; set; }
        public string? SmtpUserEmail { get; set; }
        public string? SmtpPassword { get; set; }
    }
}
