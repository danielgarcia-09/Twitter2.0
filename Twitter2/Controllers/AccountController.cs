using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Model;
using Database.Models;
using DTOS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Twitter2.ViewModels;

namespace Twitter2.Controllers
{
    public class AccountController : Controller
    {
        private readonly Twitter2Context _context;
        //private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public static string ConfirmarCodigo { get; set; }

        public AccountController(Twitter2Context context, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
           // _emailSender = emailSender;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "THome");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "THome");
            }
            var user = await _userManager.FindByNameAsync(vm.User);
            var usuario = await _context.Usuario.FirstOrDefaultAsync(c => c.UserName == vm.User);
            if (ModelState.IsValid)
            {
                if (user == null)
                {
                    ModelState.AddModelError("NoUser", "Usuario no Existente");
                }
                else
                {
                    if (await _userManager.GetAccessFailedCountAsync(user) > 2)
                    {
                        usuario.Estado = "Inactivo";
                        _context.Usuario.Update(usuario);
                        await _userManager.SetLockoutEnabledAsync(user, true);
                        await _context.SaveChangesAsync();
                        ModelState.AddModelError("UserDeactivated", "Usuario Inactivo, resuelva con el admin");

                    }
                    else if (usuario.Estado == "Inactivo")
                    {
                        ModelState.AddModelError("UserDeactivated", "Usuario Inactivo, resuelva con el admin");
                    }
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(vm.User, vm.Password, false, false);

                        if (result.Succeeded)
                        {
                         
                            
                                return RedirectToAction("Index", "THome");
                         


                        }
                        ModelState.AddModelError("UserOrPasswordInvalid", "Usuario o Contraseña inválida");
                        await _userManager.AccessFailedAsync(user);
                    }
                }




            }

            return View(vm);
        }



        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Admin");
            }

           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            if (ModelState.IsValid)
            {
                var users = await _context.Usuario.FirstOrDefaultAsync(c => c.UserName == vm.UserName);
                var correo = await _context.Usuario.FirstOrDefaultAsync(c => c.Correo == vm.Correo);
                if (users != null)
                {
                    ModelState.AddModelError("U/Exists", "Usuario ya existente");
                    return View(vm);
                }
                else if (correo != null)
                {
                    ModelState.AddModelError("C/Exists", "Correo ya usado por otro usuario");
                    return View(vm);
                }
                var user = new IdentityUser { UserName = vm.UserName, Email = vm.Correo };
                user.LockoutEnabled = true;
                var result = await _userManager.CreateAsync(user, vm.Password);



                if (result.Succeeded)
                {

                    string uniqueName = null;
                    if (vm.Photo != null)
                    {
                        var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "imgs/users");
                        uniqueName = Guid.NewGuid().ToString() + "_" + vm.Photo.FileName;
                        var filePath = Path.Combine(folderPath, uniqueName);

                        if (filePath != null)
                        {
                            var stream = new FileStream(filePath, mode: FileMode.Create);
                            vm.Photo.CopyTo(stream);
                            stream.Flush();
                            stream.Close();
                        }
                    }

                    var usuario = _mapper.Map<UserDTO>(vm);
                    usuario.Foto = uniqueName;
                    var usuarioF = _mapper.Map<Usuario>(usuario);
                    _context.Usuario.Add(usuarioF);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Login", "Account");
                    
                    
                }

                addErrors(result);
            }
            return View(vm);
        }

        private void addErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<IActionResult> CambiarPassword(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(viewModel.User);
                if(user != null)
                {
                    await _userManager.ChangePasswordAsync(user, viewModel.Password, viewModel.newPassword);
                    return RedirectToAction("Login");
                }
                
            }
            return RedirectToAction("Login");
        }


       


    }
}