using AgilePMC.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using static AgilePMC.Utils.Entity;
using static System.Net.WebRequestMethods;

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
                DateTime now = DateTime.Now;
                DateTime dateOnly = now.Date;
                var date = dateOnly.ToString("yyyy-MM-dd");
                TimeSpan timeOnly = now.TimeOfDay;
                var hours = timeOnly.Hours;
                var minutes = timeOnly.Minutes;
                var seconds = timeOnly.Seconds;
                var time = hours + ":" + minutes + ":" + seconds;
                string timenew = date + " " + time;
                DateTime times = Convert.ToDateTime(timenew);

                var slider = (from x in _db.Sliders
                              where x.EffectiveFrom <= times && x.EffectiveTo >= times
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
            DateTime now = DateTime.Now;
            DateTime dateOnly = now.Date;
            var date = dateOnly.ToString("yyyy-MM-dd");
            TimeSpan timeOnly = now.TimeOfDay;
            var hours = timeOnly.Hours;
            var minutes = timeOnly.Minutes;
            var seconds = timeOnly.Seconds;
            var time = hours + ":" + minutes + ":" + seconds;
            string timenew = date + " " + time;
            DateTime times = Convert.ToDateTime(timenew);

            var responseObj = new ResponseObj<object>();
            try
            {
                var letter = (from x in _db.NewsLetters
                              where x.EffectiveFrom <= times && x.EffectiveTo >= times
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

                    var smtpCRED = _db.SmtpDetails.FirstOrDefault();
                    if(smtpCRED == null)
                    {
                        responseObj.isSuccess = false;
                        responseObj.message = "Error";
                        responseObj.responseCode = 400;
                        responseObj.data = "Mail Not Sent";
                        return responseObj;
                    }

                    if (request.ContactUsType == "ForAssociation")
                    {
                      //  for Agile PMC


                        MailMessage mailMsg = new MailMessage();
                        // From
                        MailAddress mailAddress = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg.From = mailAddress;
                        // Subject and Body
                        mailMsg.Subject = "New Associatiion Enquiry.";


                        string mailcon = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <title>Reset Password</title>
                        <style> 
                        .header{text-align:center;}
                        .position{text-align: justify;}
                        .code{background-color:#ececec;padding:9px;}
                        .img-socialmedia{width:24px;}
                        .footer{background-color:black;padding-top:3px;padding-bottom:3px;}
                        .text{color:white;}
                        .text1{border-bottom:1px solid white;color:white;}
                        .socialmedia{margin-bottom:35px;}
                        </style>
                        </head>
                        <body class=""header"">
                        <img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
                        <p class=""position"">Dear Agile PMC,</p>
                        <p class=""position"">        You have received a new association enquiry with the following details,</p>
                        <p class=""position"">Organization Name : " + request.OrganizationName + @".</p>
                        <p class=""position"">Organization Type : " + request.OrganizationType + @".</p>
                        <p class=""position"">Association For : " + request.AssociateFor + @".</p>
                        <p class=""position"">Business Profile Brief : " + request.BusinessProfille + @".</p>
                        <p class=""position"">Website : " + request.Website + @".</p>
                        <p class=""position"">Contact Name & Designation : " + request.ContactName + @".</p>
                        <p class=""position"">Address : " + request.Address + @".</p>
                        <p class=""position"">Phone Number : " + request.PhoneNumber + @".</p>
                        <p class=""position"">Email Address : " + request.Email + @".</p>
                        <p class=""position"">Message  : " + request.Message + @".</p>
                        <br>
                        <p class=""position"">Regards,</p>
                        <p  class=""position"">
                        <a href=https://agilepmc.com/>https://agilepmc.com/>
                        </p>
                        </body>
                        </html>";

                        mailMsg.Body = mailcon;
                       // mailMsg.To.Add(request.Email);
                          mailMsg.To.Add("michel.charles@sysmedac.com");

                        mailMsg.IsBodyHtml = true;
                        SmtpClient emailClient = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient.Credentials = credentials;
                        emailClient.EnableSsl = true; // Enable SSL for secure communication
                                                      // Add the following line to enable STARTTLS
                        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg = "";
                        try
                        {
                            emailClient.Send(mailMsg);
                            Msg = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg = "Faliure";
                        }

                        // for visitors


                        MailMessage mailMsg1 = new MailMessage();
                        // From
                        MailAddress mailAddress1 = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg1.From = mailAddress1;
                        // Subject and Body
                        mailMsg1.Subject = "Thanks  for contacting Agile PMC.";

                        string mailcon1 = @"<!DOCTYPE html>
<html>
<head>
<title>Reset Password</title>
<style> 
.header{text-align:center;}
.position{text-align: justify;}
.code{background-color:#ececec;padding:7px;}
.img-socialmedia{width:24px;}
.footer{background-color:black;padding-top:3px;padding-bottom:3px;}
.text{color:white;}
.text1{border-bottom:1px solid white;color:white;}
.socialmedia{margin-bottom:35px;}
</style>
</head>
<body class=""header"">
<img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
<p class=""position"">Dear " + request.ContactName + @",</p>
<p class=""position"">Thanks for contacting Agile PMC, We will contact you soon.</p>
<p class=""position"">Thanks and Regards,</p>
<h5 class=""position"">Team Agile PMC,</h5>
<p  class=""position"">
<a href=https://agilepmc.com/>https://agilepmc.com/>
</p>
</body>
</html>";

                        mailMsg1.Body = mailcon1;
                        //  mailMsg1.To.Add(request.Email);
                        mailMsg1.To.Add("michel.charles@sysmedac.com");

                        mailMsg1.IsBodyHtml = true;
                        SmtpClient emailClient1 = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient1.Credentials = credentials1;
                        emailClient1.EnableSsl = true; // Enable SSL for secure communication
                                                       // Add the following line to enable STARTTLS
                        emailClient1.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg1 = "";
                        try
                        {
                            emailClient1.Send(mailMsg1);
                            Msg1 = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg1 = "Faliure";
                        }

                    }
                    #region
                    else if (request.ContactUsType == "ForEnquires")
                    {

                        //  for Agile PMC


                        MailMessage mailMsg = new MailMessage();
                        // From
                        MailAddress mailAddress = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg.From = mailAddress;
                        // Subject and Body
                        mailMsg.Subject = "New Enquiry.";


                        string mailcon = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <title>Reset Password</title>
                        <style> 
                        .header{text-align:center;}
                        .position{text-align: justify;}
                        .code{background-color:#ececec;padding:9px;}
                        .img-socialmedia{width:24px;}
                        .footer{background-color:black;padding-top:3px;padding-bottom:3px;}
                        .text{color:white;}
                        .text1{border-bottom:1px solid white;color:white;}
                        .socialmedia{margin-bottom:35px;}
                        </style>
                        </head>
                        <body class=""header"">
                        <img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
                        <p class=""position"">Dear Agile PMC,</p>
                        <p class=""position"">        You have received a new enquiry with the following details,</p>
                        <p class=""position"">Contact Name & Designation : " + request.ContactName + @".</p>
                        <p class=""position"">Phone Number : " + request.PhoneNumber + @".</p>
                        <p class=""position"">Email Address : " + request.Email + @".</p>
                        <p class=""position"">Message  : " + request.Message + @".</p>
                        <br>
                        <p class=""position"">Regards,</p>
                        <p  class=""position"">
                        <a href=https://agilepmc.com/>https://agilepmc.com/>
                        </p>
                        </body>
                        </html>";

                        mailMsg.Body = mailcon;
                        // mailMsg.To.Add(request.Email);
                        mailMsg.To.Add("michel.charles@sysmedac.com");

                        mailMsg.IsBodyHtml = true;
                        SmtpClient emailClient = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient.Credentials = credentials;
                        emailClient.EnableSsl = true; // Enable SSL for secure communication
                                                      // Add the following line to enable STARTTLS
                        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg = "";
                        try
                        {
                            emailClient.Send(mailMsg);
                            Msg = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg = "Faliure";
                        }

                        //                        // for visitors


                        MailMessage mailMsg1 = new MailMessage();
                        // From
                        MailAddress mailAddress1 = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg1.From = mailAddress1;
                        // Subject and Body
                        mailMsg1.Subject = "Thanks  for contacting Agile PMC.";

                        string mailcon1 = @"<!DOCTYPE html>
<html>
<head>
<title>Reset Password</title>
<style> 
.header{text-align:center;}
.position{text-align: justify;}
.code{background-color:#ececec;padding:7px;}
.img-socialmedia{width:24px;}
.footer{background-color:black;padding-top:3px;padding-bottom:3px;}
.text{color:white;}
.text1{border-bottom:1px solid white;color:white;}
.socialmedia{margin-bottom:35px;}
</style>
</head>
<body class=""header"">
<img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
<p class=""position"">Dear " + request.ContactName + @",</p>
<p class=""position"">Thanks for contacting Agile PMC, We will contact you soon.</p>
<p class=""position"">Thanks and Regards,</p>
<h5 class=""position"">Team Agile PMC,</h5>
<p  class=""position"">
<a href=https://agilepmc.com/>https://agilepmc.com/>
</p>
</body>
</html>";

                        mailMsg1.Body = mailcon1;
                        //  mailMsg1.To.Add(request.Email);
                        mailMsg1.To.Add("michel.charles@sysmedac.com");

                        mailMsg1.IsBodyHtml = true;
                        SmtpClient emailClient1 = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient1.Credentials = credentials1;
                        emailClient1.EnableSsl = true; // Enable SSL for secure communication
                                                       // Add the following line to enable STARTTLS
                        emailClient1.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg1 = "";
                        try
                        {
                            emailClient1.Send(mailMsg1);
                            Msg1 = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg1 = "Faliure";
                        }

                    }
                    else if (request.ContactUsType == "ForCareers")
                    {
                        //  for Agile PMC


                        MailMessage mailMsg = new MailMessage();
                        // From
                        MailAddress mailAddress = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg.From = mailAddress;
                        // Subject and Body
                        mailMsg.Subject = "New Career Application.";


                        string mailcon = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <title>Reset Password</title>
                        <style> 
                        .header{text-align:center;}
                        .position{text-align: justify;}
                        .code{background-color:#ececec;padding:9px;}
                        .img-socialmedia{width:24px;}
                        .footer{background-color:black;padding-top:3px;padding-bottom:3px;}
                        .text{color:white;}
                        .text1{border-bottom:1px solid white;color:white;}
                        .socialmedia{margin-bottom:35px;}
                        </style>
                        </head>
                        <body class=""header"">
                        <img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
                        <p class=""position"">Dear Agile PMC,</p>
                        <p class=""position"">        You have received a new  Career apllication with the following details,</p>
                        <p class=""position"">First Name : " + request.FirstName + @".</p>
                        <p class=""position"">SurName  : " + request.SurName + @".</p>
                        <p class=""position"">Applying For : " + request.ApplyingFor + @".</p>
                        <p class=""position"">Address : " + request.Address + @".</p>
                        <p class=""position"">Qualification  : " + request.Qualification + @".</p>
                        <p class=""position"">Experiance in year : " + request.Experiance + @".</p>
                        <p class=""position"">Phone Number : " + request.PhoneNumber + @".</p>
                        <p class=""position"">Email Address : " + request.Email + @".</p>
                        <p class=""position"">Message  : " + request.Message + @".</p>
                        <br>
                        <p class=""position"">Regards,</p>
                        <p  class=""position"">
                        <a href=https://agilepmc.com/>https://agilepmc.com/>
                        </p>
                        </body>
                        </html>";

                        mailMsg.Body = mailcon;
                        // mailMsg.To.Add(request.Email);
                        mailMsg.To.Add("michel.charles@sysmedac.com");

                        mailMsg.IsBodyHtml = true;
                        SmtpClient emailClient = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient.Credentials = credentials;
                        emailClient.EnableSsl = true; // Enable SSL for secure communication
                                                      // Add the following line to enable STARTTLS
                        emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg = "";
                        try
                        {
                            emailClient.Send(mailMsg);
                            Msg = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg = "Faliure";
                        }

                        //                        // for visitors


                        MailMessage mailMsg1 = new MailMessage();
                        // From
                        MailAddress mailAddress1 = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                        mailMsg1.From = mailAddress1;
                        // Subject and Body
                        mailMsg1.Subject = "Thanks  for contacting Agile PMC.";

                        string mailcon1 = @"<!DOCTYPE html>
<html>
<head>
<title>Reset Password</title>
<style> 
.header{text-align:center;}
.position{text-align: justify;}
.code{background-color:#ececec;padding:7px;}
.img-socialmedia{width:24px;}
.footer{background-color:black;padding-top:3px;padding-bottom:3px;}
.text{color:white;}
.text1{border-bottom:1px solid white;color:white;}
.socialmedia{margin-bottom:35px;}
</style>
</head>
<body class=""header"">
<img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
<p class=""position"">Dear " + request.FirstName + @",</p>
<p class=""position"">Thanks for contacting Agile PMC, We will contact you soon.</p>
<p class=""position"">Thanks and Regards,</p>
<h5 class=""position"">Team Agile PMC,</h5>
<p  class=""position"">
<a href=https://agilepmc.com/>https://agilepmc.com/>
</p>
</body>
</html>";

                        mailMsg1.Body = mailcon1;
                        //  mailMsg1.To.Add(request.Email);
                        mailMsg1.To.Add("michel.charles@sysmedac.com");

                        mailMsg1.IsBodyHtml = true;
                        SmtpClient emailClient1 = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                        System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                        emailClient1.Credentials = credentials1;
                        emailClient1.EnableSsl = true; // Enable SSL for secure communication
                                                       // Add the following line to enable STARTTLS
                        emailClient1.DeliveryMethod = SmtpDeliveryMethod.Network;

                        var Msg1 = "";
                        try
                        {
                            emailClient1.Send(mailMsg1);
                            Msg1 = "Successs";
                        }
                        catch (Exception ex)
                        {
                            string register = ex.ToString();
                            Msg1 = "Faliure";
                        }
                    }
                        #endregion

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

        public ResponseObj<object> DownloadNotificationMail(DownloadReq request)
        {
            var responseObj = new ResponseObj<object>();
            try
            {
                var smtpCRED = _db.SmtpDetails.FirstOrDefault();
                if (smtpCRED == null)
                {
                    responseObj.isSuccess = false;
                    responseObj.message = "Error";
                    responseObj.responseCode = 400;
                    responseObj.data = "Mail Not Sent";
                    return responseObj;
                }

                //  for Agile PMC


                MailMessage mailMsg = new MailMessage();
                // From
                MailAddress mailAddress = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                mailMsg.From = mailAddress;
                // Subject and Body
                mailMsg.Subject = "Brochure Download.";


                string mailcon = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <title>Reset Password</title>
                        <style> 
                        .header{text-align:center;}
                        .position{text-align: justify;}
                        .code{background-color:#ececec;padding:9px;}
                        .img-socialmedia{width:24px;}
                        .footer{background-color:black;padding-top:3px;padding-bottom:3px;}
                        .text{color:white;}
                        .text1{border-bottom:1px solid white;color:white;}
                        .socialmedia{margin-bottom:35px;}
                        </style>
                        </head>
                        <body class=""header"">
                        <img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
                        <p class=""position"">Dear Agile PMC,</p>
                        <p class=""position"">        New brocher download with the following details,</p>
                        <p class=""position"">Name : " + request.Name + @".</p>
                        <p class=""position"">Email Address : " + request.Email + @".</p>
                        <br>
                        <p class=""position"">Regards,</p>
                        <p  class=""position"">
                        <a href=https://agilepmc.com/>https://agilepmc.com/>
                        </p>
                        </body>
                        </html>";

                mailMsg.Body = mailcon;
                // mailMsg.To.Add(request.Email);
                mailMsg.To.Add("michel.charles@sysmedac.com");

                mailMsg.IsBodyHtml = true;
                SmtpClient emailClient = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                emailClient.Credentials = credentials;
                emailClient.EnableSsl = true; // Enable SSL for secure communication
                                              // Add the following line to enable STARTTLS
                emailClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                var Msg = "";
                try
                {
                    emailClient.Send(mailMsg);
                    Msg = "Successs";
                }
                catch (Exception ex)
                {
                    string register = ex.ToString();
                    Msg = "Faliure";
                }

                //for visitors

                MailMessage mailMsg1 = new MailMessage();
                // From
                MailAddress mailAddress1 = new MailAddress(smtpCRED.SmtpUserEmail, "Agile PMC");
                mailMsg1.From = mailAddress1;
                // Subject and Body
                mailMsg1.Subject = "Brochure Download.";

                string mailcon1 = @"<!DOCTYPE html>
<html>
<head>
<title>Reset Password</title>
<style> 
.header{text-align:center;}
.position{text-align: justify;}
.code{background-color:#ececec;padding:7px;}
.img-socialmedia{width:24px;}
.footer{background-color:black;padding-top:3px;padding-bottom:3px;}
.text{color:white;}
.text1{border-bottom:1px solid white;color:white;}
.socialmedia{margin-bottom:35px;}
</style>
</head>
<body class=""header"">
<img src=""https://agileapi.sysmedac.com/Uploads/agilelogo.png""/>
<p class=""position"">Dear " + request.Name + @",</p>
<p class=""position"">Thanks for downloading the Brochure, Please contact us for more details.</p>
<p class=""position"">Thanks and Regards,</p>
<h5 class=""position"">Team Agile PMC,</h5>
<p  class=""position"">
<a href=https://agilepmc.com/>https://agilepmc.com/>
</p>
</body>
</html>";

                mailMsg1.Body = mailcon1;
                //  mailMsg1.To.Add(request.Email);
                mailMsg1.To.Add("michel.charles@sysmedac.com");

                mailMsg1.IsBodyHtml = true;
                SmtpClient emailClient1 = new SmtpClient(smtpCRED.SmtpDomainName, (int)smtpCRED.SmtpPort);
                System.Net.NetworkCredential credentials1 = new System.Net.NetworkCredential(smtpCRED.SmtpUserEmail, smtpCRED.SmtpPassword);
                emailClient1.Credentials = credentials1;
                emailClient1.EnableSsl = true; // Enable SSL for secure communication
                                               // Add the following line to enable STARTTLS
                emailClient1.DeliveryMethod = SmtpDeliveryMethod.Network;

                var Msg1 = "";
                try
                {
                    emailClient1.Send(mailMsg1);
                    Msg1 = "Successs";
                }
                catch (Exception ex)
                {
                    string register = ex.ToString();
                    Msg1 = "Faliure";
                }


                responseObj.isSuccess = true;
                responseObj.message = "Success";
                responseObj.responseCode = 200;
                responseObj.data = "Download Successful..!";
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
