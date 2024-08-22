using Microsoft.AspNetCore.Mvc;
using MovieWebsite.Models.DomainModel;
using Newtonsoft.Json;
using System.Net.Http;
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


    }
}
