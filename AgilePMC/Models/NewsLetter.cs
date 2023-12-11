using System;
using System.Collections.Generic;

namespace AgilePMC.Models
{
    public partial class NewsLetter
    {
        public long NewsLetterId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string? TemplateImageUrl { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
