using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFE.Data;
using PFE.Models;

namespace PFE.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Hash the password
                string hashedPassword = HashPassword(user.Password);

                // Create a new User object with the hashed password
                User newUser = new User
                {
                    Nom = user.Nom,
                    Email = user.Email,
                    Password = hashedPassword
                    
                };

                // Save the user to the database
                _context.Users.Add(newUser);
                _context.SaveChanges();

                // Redirect to a success page or login page
                return RedirectToAction("Login");
            }

            // If the model is not valid, return the registration view with validation errors
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Hash the provided password
            string hashedPassword = HashPassword(password);

            // Check if the provided email and hashed password match a user in the database
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == hashedPassword);

            if (user != null)
            {
                // User found, redirect to a success page
                return RedirectToAction("Index", "Home");
            }

            // User not found, display an error message
            ViewBag.ErrorMessage = "Email ou mot de passe incorrects";
            return View();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}