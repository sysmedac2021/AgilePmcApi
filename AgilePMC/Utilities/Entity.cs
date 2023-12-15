namespace AgilePMC.Utils
{
    public class Entity
    {
        public class ResponseObj<T> where T : class
        {
            public int responseCode { get; set; }
            public bool isSuccess { get; set; }
            public object? data { get; set; }
            public string? message { get; set; }
        }

        public partial class SliderReq
        {
            public long SliderId { get; set; }
            public string? SliderName { get; set; }
            public string? Description { get; set; }
            public string? SliderImageUrl { get; set; }
            public string? Status { get; set; }
            public long? CreatedBy { get; set; }
            public long? UpdatedBy { get; set; }
            public DateTime? EffectiveFrom { get; set; }
            public DateTime? EffectiveTo { get; set; }
        }

        public partial class NewsLetterReq
        {
            public long NewsLetterId { get; set; }
            public string? Name { get; set; }
            public string? Status { get; set; }
            public string? Message { get; set; }
            public string? TemplateImageUrl { get; set; }
            public long? CreatedBy { get; set; }
            public long? UpdatedBy { get; set; }
            public DateTime? EffectiveFrom { get; set; }
            public DateTime? EffectiveTo { get; set; }
        }

        public partial class ContactUsReq
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
            public long? CreatedBy { get; set; }
            public string? Status { get; set; }
        }

        public class DownloadReq
        {
            public string? Email { get; set; }
            public string? MobileNumber { get; set; }
        }

    }
}
