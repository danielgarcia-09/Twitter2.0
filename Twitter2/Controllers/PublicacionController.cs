using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using Twitter2.ViewModels;

namespace Twitter2.Controllers
{
    [Authorize]
    public class PublicacionController : Controller
    {
        private readonly PublicacionRepository _repository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Twitter2Context _context;
        public PublicacionController(PublicacionRepository repository,IWebHostEnvironment hostingEnvironment, Twitter2Context context)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public async Task<IActionResult> Index(int id)
        { return View(await _repository.Index(id)); }

        [HttpPost]
        public async Task<IActionResult> addComentario(int id, FullPublicacionViewModel viewModel)
        {
            if(viewModel.Comentario != null)
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(c => c.UserName == User.Identity.Name);
                if (await _repository.AddComentario(usuario.Foto,User.Identity.Name, id, viewModel))
                {
                    
                 
                    return RedirectToAction("Index", new { id = id });
                }
            }
            return RedirectToAction("Index", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> addReply(int publ,int id, FullPublicacionViewModel viewModel)
        {
            if(viewModel.Reply != null)
            {
                if(await _repository.addReply(User.Identity.Name,id, viewModel))
                {
                    return RedirectToAction("Index", new { id = publ });
                }
                else
                {
                    return RedirectToAction("Index", new { id = publ });
                }
            }
            return RedirectToAction("Index", new { id = publ });
        }
    }
}