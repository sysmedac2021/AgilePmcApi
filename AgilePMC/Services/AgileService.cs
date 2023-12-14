using AgilePMC.Models;
using Microsoft.EntityFrameworkCore;
using static AgilePMC.Utils.Entity;

namespace AgilePMC.Services
{
    public class AgileService
    {
        agileadminContext _db;
        public AgileService(agileadminContext db)
        {
            _db = db;
        }

        public ResponseObj<object> DashBoard(DateTime? selectRange, DateTime? fromDate, DateTime? toDate)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var newsLetter = _db.NewsLetters.ToList();
                var slider = _db.Sliders.ToList();
                var contactUs = _db.ContactUs.ToList();

                if (selectRange != null)
                {
                     newsLetter = _db.NewsLetters.Where(x => x.CreateAt >= selectRange).ToList();
                     slider = _db.Sliders.Where(x => x.CreateAt >= selectRange).ToList();
                     contactUs = _db.ContactUs.Where(x => x.CreatedAt >= selectRange).ToList();
                }
                else if (fromDate != null && toDate != null)
                {
                     newsLetter = _db.NewsLetters.Where(x => x.CreateAt >= fromDate && x.CreateAt <= toDate).ToList();
                     slider = _db.Sliders.Where(x => x.CreateAt >= fromDate && x.CreateAt <= toDate).ToList();
                     contactUs = _db.ContactUs.Where(x => x.CreatedAt >= fromDate && x.CreatedAt <= toDate).ToList();
                }
                else if (fromDate != null)
                {
                     newsLetter = _db.NewsLetters.Where(x => x.CreateAt >= fromDate).ToList();
                     slider = _db.Sliders.Where(x => x.CreateAt >= fromDate).ToList();
                     contactUs = _db.ContactUs.Where(x => x.CreatedAt >= fromDate).ToList();
                }

                var result = new
                {
                    NewsletterCount = newsLetter.Count > 0 ? newsLetter.Count : 0,
                    SlidersCount = slider.Count > 0 ? slider.Count : 0,
                    ContactUsCount = contactUs.Count > 0 ? contactUs.Count : 0,
                };

                    responseObj.responseCode = 200;
                    responseObj.isSuccess = true;
                    responseObj.data = result;
                    return responseObj;

                
            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500; responseObj.isSuccess = false; responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> InsertOrUpdateSlider(SliderReq request)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                if (request.SliderId == 0)
                {
                    var existSliderName = _db.Sliders.Where(x => x.SliderName == request.SliderName).FirstOrDefault();

                    if (existSliderName != null)
                    {
                        responseObj.isSuccess = false;
                        responseObj.message = "";
                        responseObj.responseCode = 400;
                        responseObj.data = "SliderName Exist.";
                        return responseObj;
                    }

                    Slider slider = new Slider();
                    slider.SliderName = request.SliderName;
                    slider.Status = request.Status;
                    slider.CreatedBy = request.CreatedBy;
                    slider.CreateAt = DateTime.Now; ;
                    slider.SliderImageUrl = request.SliderImageUrl;
                    slider.Description = request.Description;
                    slider.EffectiveFrom = request.EffectiveFrom;
                    slider.EffectiveTo = request.EffectiveTo;

                    _db.Sliders.Add(slider);
                    _db.SaveChanges();

                    responseObj.isSuccess = true;
                    responseObj.message = "Success";
                    responseObj.responseCode = 200;
                    responseObj.data = "Slider Created Successfully..!";
                    return responseObj;

                }
                else
                {
                    var check = _db.Sliders.Where(x => x.SliderId == request.SliderId).FirstOrDefault();

                    if (check != null)
                    {
                        check.SliderName = request.SliderName;
                        check.Status = request.Status;
                        check.UpdatedBy = request.UpdatedBy;
                        check.UpdateAt = DateTime.Now;
                        check.EffectiveFrom = request.EffectiveFrom;
                        check.EffectiveTo = request.EffectiveTo;
                        check.SliderImageUrl = request.SliderImageUrl;
                        check.Description = request.Description;

                        _db.Sliders.Update(check);
                        _db.SaveChanges();
                                               
                        responseObj.isSuccess = true;
                        responseObj.message = "Success";
                        responseObj.responseCode = 200;
                        responseObj.data = "Slider Updated Successfully..!";
                        return responseObj;
                    }
                    responseObj.responseCode = 500;
                    responseObj.isSuccess = false;
                    responseObj.data = "Slider Not Found.";
                    return responseObj;

                }
            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500; responseObj.isSuccess = false; responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> InsertOrUpdateNewsLetter(NewsLetterReq request)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                if (request.NewsLetterId == 0)
                {
                    var existNewsLetterName = _db.NewsLetters.Where(x => x.Name == request.Name).FirstOrDefault();

                    if (existNewsLetterName != null)
                    {
                        responseObj.isSuccess = false;
                        responseObj.message = "";
                        responseObj.responseCode = 400;
                        responseObj.data = "NewsLetterName Exist.";
                        return responseObj;
                    }

                    NewsLetter letter = new NewsLetter();
                    letter.Name = request.Name;
                    letter.Status = request.Status;
                    letter.CreatedBy = request.CreatedBy;
                    letter.CreateAt = DateTime.Now; ;
                    letter.TemplateImageUrl = request.TemplateImageUrl;
                    letter.Message = request.Message;
                    letter.EffectiveFrom = request.EffectiveFrom;
                    letter.EffectiveTo = request.EffectiveTo;

                    _db.NewsLetters.Add(letter);
                    _db.SaveChanges();

                    responseObj.isSuccess = true;
                    responseObj.message = "Success";
                    responseObj.responseCode = 200;
                    responseObj.data = "NewsLetter Created Successfully..!";
                    return responseObj;

                }
                else
                {
                    var check = _db.NewsLetters.Where(x => x.NewsLetterId == request.NewsLetterId).FirstOrDefault();

                    if (check != null)
                    {
                        check.Name = request.Name;
                        check.Status = request.Status;
                        check.UpdatedBy = request.UpdatedBy;
                        check.UpdateAt = DateTime.Now;
                        check.EffectiveFrom = request.EffectiveFrom;
                        check.EffectiveTo = request.EffectiveTo;
                        check.TemplateImageUrl = request.TemplateImageUrl;
                        check.Message = request.Message;

                        _db.NewsLetters.Update(check);
                        _db.SaveChanges();

                        responseObj.isSuccess = true;
                        responseObj.message = "Success";
                        responseObj.responseCode = 200;
                        responseObj.data = "NewsLetter Updated Successfully..!";
                        return responseObj;
                    }
                    responseObj.responseCode = 500;
                    responseObj.isSuccess = false;
                    responseObj.data = "NewsLetter Not Found.";
                    return responseObj;

                }
            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> AdminLogin(string UserName, string Password)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var loginres = _db.AdminLogins.Where(x => x.UserName == UserName && x.Password == Password && x.Status == "Active").FirstOrDefault();

                if (loginres != null)
                {
                    responseObj.isSuccess = true;
                    responseObj.message = "Success";
                    responseObj.responseCode = 200;
                    responseObj.data = "Login Successful..!";
                    return responseObj;
                }
                else
                {
                    responseObj.isSuccess = false;
                    responseObj.message = "Error";
                    responseObj.responseCode = 400;
                    responseObj.data = "Login Failed..!";
                    return responseObj;
                }

            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> GetAllCurrentSliders()
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var slider = (from x in _db.Sliders
                              where x.EffectiveFrom >= DateTime.Now && x.EffectiveTo <= DateTime.Now
                                    select new
                                    {
                                       x.SliderImageUrl,
                                       x.Description,
                                       x.SliderName,
                                       x.SliderId,
                                        x.Status,
                                        x.EffectiveFrom,
                                       x.EffectiveTo,
                                       x.CreatedBy,
                                       x.UpdatedBy,
                                       x.UpdateAt,
                                       x.CreateAt,
                                    }).ToList();

                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = slider;
                return responseObj;


            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> GetAllSliders()
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var slider = (from x in _db.Sliders
                              select new
                              {
                                  x.SliderImageUrl,
                                  x.Description,
                                  x.SliderName,
                                  x.Status,
                                  x.SliderId,
                                  x.EffectiveFrom,
                                  x.EffectiveTo,
                                  x.CreatedBy,
                                  x.UpdatedBy,
                                  x.UpdateAt,
                                  x.CreateAt,
                              }).ToList();

                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = slider;
                return responseObj;


            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> GetAllCurrentNewsletter()
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var letter = (from x in _db.NewsLetters
                              where x.EffectiveFrom >= DateTime.Now && x.EffectiveTo <= DateTime.Now
                              select new
                              {
                                  x.TemplateImageUrl,
                                  x.Message,
                                  x.Status,
                                  x.Name,
                                  x.NewsLetterId,
                                  x.EffectiveFrom,
                                  x.EffectiveTo,
                                  x.CreatedBy,
                                  x.UpdatedBy,
                                  x.UpdateAt,
                                  x.CreateAt,
                              }).ToList();

                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = letter;
                return responseObj;


            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> GetAllNewsLetter()
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var slider = (from x in _db.NewsLetters
                              select new
                              {
                                  x.TemplateImageUrl,
                                  x.Status,
                                  x.Message,
                                  x.Name,
                                  x.NewsLetterId,
                                  x.EffectiveFrom,
                                  x.EffectiveTo,
                                  x.CreatedBy,
                                  x.UpdatedBy,
                                  x.UpdateAt,
                                  x.CreateAt,
                              }).ToList();

                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = slider;
                return responseObj;


            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> InsertOrUpdateContactUs(ContactUsReq request)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                if (request.ContactUsId == 0)
                {
                    ContactU contact = new ContactU();
                    contact.ContactName = request.ContactName;
                    contact.Status = request.Status;
                    contact.CreatedBy = request.CreatedBy;
                    contact.CreatedAt = DateTime.Now; 
                    contact.FirstName = request.FirstName;
                    contact.SurName = request.SurName;
                    contact.ContactUsType = request.ContactUsType;
                    contact.OrganizationName = request.OrganizationName;
                    contact.OrganizationType = request.OrganizationType;
                    contact.AssociateFor = request.AssociateFor;
                    contact.ApplyingFor = request.ApplyingFor;
                    contact.Email = request.Email;
                    contact.PhoneNumber = request.PhoneNumber;
                    contact.BusinessProfille = request.BusinessProfille;
                    contact.Website = request.Website;
                    contact.Address = request.Address;
                    contact.Message = request.Message;
                    contact.Qualification = request.Qualification;
                    contact.Experiance = request.Experiance;
                    contact.BusinessProfille = request.BusinessProfille;
                    contact.Website = request.Website;
                    contact.Address = request.Address;

                    _db.ContactUs.Add(contact);
                    _db.SaveChanges();

                    responseObj.isSuccess = true;
                    responseObj.message = "Success";
                    responseObj.responseCode = 200;
                    responseObj.data = "Query Send Successfully..!";
                    return responseObj;

                }
                else
                { 
                    responseObj.responseCode = 500;
                    responseObj.isSuccess = false;
                    responseObj.data = "Query Send Failed";
                    return responseObj;

                }
            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500; responseObj.isSuccess = false; responseObj.data = ex.Message;
                return responseObj;
            }
        }

        public ResponseObj<object> GetAllContactUsList()
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var slider = (from x in _db.ContactUs
                              select new
                              {
                                  x.ContactUsId,
                                  x.ContactName,
                                  x.ContactUsType,
                                  x.ApplyingFor,
                                  x.AssociateFor,
                                  x.BusinessProfille,
                                  x.Website,
                                  x.Address,
                                  x.Email,
                                  x.PhoneNumber,
                                  x.Experiance,
                                  x.Message,
                                  x.OrganizationName,
                                  x.OrganizationType,
                                  x.Qualification,
                                  x.Status,
                                  x.CreatedAt,
                                  x.CreatedBy,
                                  x.FirstName,
                                  x.SurName,
                              }).ToList();

                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = slider;
                return responseObj;


            }
            catch (Exception ex)
            {
                responseObj.responseCode = 500;
                responseObj.isSuccess = false;
                responseObj.data = ex.Message;
                return responseObj;
            }
        }
    }
}
