using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AprendaDotNet.VideoOnDemand.Data;
using AprendaDotNet.VideoOnDemand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprendaDotNet.VideoOnDemand.Controllers
{
    public class SecurityController : Controller
    {

        private  string _userId;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<MyIdentityRole> _roleManager;
    



        public SecurityController(IHttpContextAccessor httpContextAccessor, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<MyIdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            var user = httpContextAccessor.HttpContext.User;
            _userId = userManager.GetUserId(user);
        }

     

        public async Task<ActionResult> Edit()
        {
            //MyIdentityRole role = new MyIdentityRole()
            //{

            //    Name = "Admin"
            //};
            //IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

            ApplicationUser user = await _userManager.FindByNameAsync("fabiogalantemans@gmail.com");

            var userResult = await _userManager.AddToRoleAsync(user, "Admin");

            return View();
        }


    }



}