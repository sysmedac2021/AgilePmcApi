using System;
using System.Collections.Generic;

namespace AgilePMC.Models
{
    public partial class Slider
    {
        public long SliderId { get; set; }
        public string? SliderName { get; set; }
        public string? Description { get; set; }
        public string? SliderImageUrl { get; set; }
        public string? Status { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
