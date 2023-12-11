using AgilePMC.Models;
using AgilePMC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static AgilePMC.Utils.Entity;

namespace AgilePMC.Controllers
{
    [ApiController]
    public class AgileController : ControllerBase
    {
        agileadminContext _db;
        AgileService _agileService;
        public AgileController(agileadminContext db)
        {
            _db = db;
            _agileService = new AgileService(db);
        }

        [HttpPost]
        [Route("api/Agile/InsertOrUpdateSlider")]
        public async Task<object> InsertOrUpdateSlider(SliderReq request)
        {
            try
            {
                var response = await Task.FromResult(_agileService.InsertOrUpdateSlider(request));

                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace };
            }
        }

        [HttpPost]
        [Route("api/Agile/InsertOrUpdateNewsletter")]
        public async Task<object> InsertOrUpdateNewsletter(NewsLetterReq request)
        {
            try
            {
                var response = await Task.FromResult(_agileService.InsertOrUpdateNewsLetter(request));

                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response
                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new
                {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpGet]
        [Route("api/Agile/Login")]
        public async Task<object> AdminLogin(string UserName, string Password)
        {
            try
            {
                var response = await Task.FromResult(_agileService.AdminLogin(UserName, Password));
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpGet]
        [Route("api/Agile/GetAllSliders")]
        public async Task<object> GetAllSliders()
        {
            try
            {
                var response = await Task.FromResult(_agileService.GetAllSliders());
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new { status = "Error", time = DateTime.Now, data = ex.Message, data2 = ex.StackTrace };
            }
        }

        [HttpGet]
        [Route("api/Agile/GetAllNewsLetters")]
        public async Task<object> GetAllNewsLetters()
        {
            try
            {
                var response = await Task.FromResult(_agileService.GetAllNewsLetter());
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new { 
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpGet]
        [Route("api/Agile/GetAllCurrentNewsLetters")]
        public async Task<object> GetAllCurrentNewsLetters()
        {
            try
            {
                var response = await Task.FromResult(_agileService.GetAllCurrentNewsletter());
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new
                {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpGet]
        [Route("api/Agile/GetAllCurrentSliders")]
        public async Task<object> GetAllCurrentSlider()
        {
            try
            {
                var response = await Task.FromResult(_agileService.GetAllCurrentSliders());
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new
                {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpPost]
        [Route("api/Agile/InsertOrUpdateContactUs")]
        public async Task<object> InsertOrUpdateContactUs(ContactUsReq request)
        {
            try
            {
                var response = await Task.FromResult(_agileService.InsertOrUpdateContactUs(request));

                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new
                {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message,
                    data2 = ex.StackTrace
                };
            }
        }

        [HttpGet]
        [Route("api/Agile/GetAllContactUsList")]
        public async Task<object> GetAllContactUsList()
        {
            try
            {
                var response = await Task.FromResult(_agileService.GetAllContactUsList());
                return new
                {
                    issuccess = true,
                    time = DateTime.Now,
                    data = response

                };
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new { status = "Error", time = DateTime.Now, data = ex.Message, data2 = ex.StackTrace };
            }
        }

        [HttpPost]
        [Route("api/Agile/FileUpload")]
        public async Task<object> ProfilePicUpload()
        {
            try
            {
                var files = Request.Form.Files;

                var file = files[0];

                string physicalPath = "";
                string FileName = "";
                string fileName = "";
                try
                {
                    Guid gid = Guid.NewGuid();


                    FileName = gid.ToString() + "_" + file.FileName;

                    physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", FileName);

                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                    }

                    var path = Directory.GetCurrentDirectory() + "/Uploads/" + FileName;

                    var fileDetails = new
                    {
                        FileName = FileName,
                        Path = path,
                    };

                    return new { status = "Success", time = DateTime.Now, fileDetails = fileDetails };

                }
                catch (Exception ex)
                {
                    return new { status = "Error", time = DateTime.Now, data = "Something went wrong when uploading document" };

                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new
                {
                    status = "Error",
                    time = DateTime.Now,
                    data = ex.Message
                };
            }
        }
    }
}
