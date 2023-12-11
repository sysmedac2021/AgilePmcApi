using System;
using System.Collections.Generic;

namespace AgilePMC.Models
{
    public partial class AdminLogin
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
    }
}
