using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Models.DomainModel;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebsite.Controllers
{
    public class UserController : Controller
    {
        // Hiển thị thông tin người dùng
        public async Task<IActionResult> ShowInfo(Guid userId)
        {
            User user = null;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"https://localhost:7271/api/auth/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(apiResponse);
                }
            }
            return View(user);
        }

        // Cập nhật thông tin người dùng
        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateUserDto = new
            {
                Name = updatedUser.Name,
                Email = updatedUser.Email,
                PhoneNumber = updatedUser.PhoneNumber,
                BirthDate = updatedUser.BirthDate,
                Password = updatedUser.Password
            };

            using (var httpClient = new HttpClient())
            {
                var updateApiUrl = $"https://localhost:7271/api/auth/Update/{updatedUser.Id}";
                var json = JsonConvert.SerializeObject(updateUserDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(updateApiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, errorContent);
                }
            }
        }
       
        // [HttpPost]
        //public async Task<IActionResult> Login(LoginRequest loginRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Message = "Dữ liệu không hợp lệ!";
        //        return View(loginRequest);
        //    }

        //    using (var httpClient = new HttpClient())
        //    {
        //        var loginUrl = "https://localhost:7271/api/auth/login";

        //        var json = JsonConvert.SerializeObject(loginRequest);
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //        var response = await httpClient.PostAsync(loginUrl, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var apiResponse = await response.Content.ReadAsStringAsync();
        //            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(apiResponse);

        //            // Lưu trạng thái đăng nhập vào session
        //            HttpContext.Session.SetString("IsLoggedIn", "true");

        //            // Chuyển hướng đến trang chính sau khi đăng nhập thành công
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Email hoặc mật khẩu không chính xác!";
        //        }
        //    }

        //    return View(loginRequest);
        //}

    }
}
