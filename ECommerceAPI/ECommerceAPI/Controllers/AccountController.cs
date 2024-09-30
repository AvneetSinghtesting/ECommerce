using ECommerceAPI.Model;
using ECommerceAPI.Model.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationUserManager _appUserManager;
        private readonly ApplicationSignInManager _appSignInManager;
        private readonly ApplicationRoleManager _appRoleManager;
        public AccountController(ApplicationUserManager appUserManager,
                                   ApplicationSignInManager appSignInManager,
                                   ApplicationRoleManager appRoleManager)
        {
            _appUserManager = appUserManager;
            _appSignInManager = appSignInManager;
            _appRoleManager = appRoleManager;
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateUser(ApplicationUser applicationUser)
        //{
        //    var user=await _appUserManager.CreateAsync(applicationUser,"Avneet@123");
        //    if (!user.Succeeded)
        //        return BadRequest(user);
        //    return Ok(user);

        //}
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //adding roles in the roles table

                //await _appRoleManager.CreateAsync(new ApplicationRole() { Name=SD.Role_User_Admin});
                //if (!await _appRoleManager.RoleExistsAsync(SD.Role_User_Admin))
                //{
                //    await _appRoleManager.CreateAsync(new ApplicationRole(SD.Role_User_Admin));
                //}
                //if (!await _appRoleManager.RoleExistsAsync(SD.Role_User_Company))
                //{
                //    await _appRoleManager.CreateAsync(new ApplicationRole() { Name = SD.Role_User_Company });
                //}
                //if (!await _appRoleManager.RoleExistsAsync(SD.Role_User_Employee))
                //{
                //    await _appRoleManager.CreateAsync(new ApplicationRole() { Name = SD.Role_User_Employee });
                //}
                //if (!await _appRoleManager.RoleExistsAsync(SD.Role_User_Individual))
                //{
                //    await _appRoleManager.CreateAsync(new ApplicationRole() { Name = SD.Role_User_Individual });
                //}


                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                var result = await _appUserManager.CreateAsync(user, model.Password);

                //assigning role to the user
                await _appUserManager.AddToRoleAsync(user, SD.Role_User_Individual);

                if (result.Succeeded)
                {
                    // Optionally sign in the user after registration
                    await _appSignInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new { Message = "User registered successfully." });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest("Invalid user registration details.");
        }

        [HttpGet("getUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _appUserManager.FindByIdAsync(userId);
            var role = await _appUserManager.GetRolesAsync(user);
            var rolesId = await _appRoleManager.FindByNameAsync(role[0].ToString());
            //var roleId= await _appRoleStore.Roles.Where(u=>u.Id==userId).FirstOrDefaultAsync();

            //var userVM = new userVM()
            //{
            //    userModel = new UserModel { UserName = user.UserName, Email = user.Email },
            //    sdRole
            //};
            //var uservm = new userVM()
            //{
            //    userModel = new UserModel
            //    {
            //        UserName = user.UserName,
            //        Email = user.Email,
            //        Id = user.Id
            //    },
            //    sdRole = role[0]
            //};

            if (user != null)
            {
                //var userDetails = new RegisterModel
                //{
                //    UserName = user.UserName,
                //    Email = user.Email
                //    // Add other necessary properties
                //};

                var userDetails = new userVM()
                {
                    userModel = new UserModel
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        Id = user.Id
                    },
                    Role = role[0],
                    RoleId = rolesId.Id
                };

                return Ok(userDetails);
            }

            return NotFound("User not found.");
        }


        [HttpGet("getAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _appUserManager.Users.ToListAsync();
            var userList = new List<userVM>();

            if (user == null)
                return BadRequest();
            foreach (var item in user)
            {
                var role = await _appUserManager.GetRolesAsync(item);
                var rolesDetails = await _appRoleManager.FindByNameAsync(role[0].ToString());
                var users = new userVM()
                {
                    userModel = new UserModel()
                    {
                        Email = item.Email,
                        Id = item.Id,
                        UserName = item.UserName
                    },
                    Role = role[0],
                    RoleId=rolesDetails.Id
                };
                //var userDetials = new UserModel
                //{
                //    Id = item.Id,
                //    UserName = item.UserName,
                //    Email = item.Email
                //};
                userList.Add(users);
            }
            return Ok(userList);
        }


        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _appUserManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    // Update other necessary properties

                    var result = await _appUserManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "User updated successfully." });
                    }

                    return BadRequest(result.Errors);
                }

                return NotFound("User not found.");
            }

            return BadRequest("Invalid update details.");
        }

        [HttpDelete("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _appUserManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _appUserManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User deleted successfully." });
                }

                return BadRequest(result.Errors);
            }

            return NotFound("User not found.");
        }
    }
}
