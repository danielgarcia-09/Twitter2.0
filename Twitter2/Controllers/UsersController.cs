using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;

namespace Twitter2.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersRepository _repository;
        private readonly IMapper _mapper;
        public UsersController(UsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.Index(User.Identity.Name));
        }
        public async Task<IActionResult> AgregarAmigo(string user)
        {
            if(await _repository.AgregarAmigo(user, User.Identity.Name) == 1)
            {
                return RedirectToAction("AmigoAgregado");
            }else if(await _repository.AgregarAmigo(user, User.Identity.Name) == 2)
            {
                return RedirectToAction("ErrorAmigo");
            }
            else
            {
                return RedirectToAction("Index");
            }
           
            
        }

        public IActionResult AmigoAgregado()
        {
            return View();
        }

        public IActionResult ErrorAmigo()
        {
            return View();
        }

    }
}