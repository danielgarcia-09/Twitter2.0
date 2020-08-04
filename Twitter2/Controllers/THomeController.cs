using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using Twitter2.ViewModels;

namespace Twitter2.Controllers
{
    [Authorize]
    public class THomeController : Controller
    {
        
        private readonly THomeRepository _repository;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public THomeController(THomeRepository repository,IWebHostEnvironment hostingEnvironment)
        {
            _repository = repository;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        { return View(await _repository.Home(User.Identity.Name)); }

        [HttpPost]
        public async Task<IActionResult> addPublicacion(THomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueName = null;
                if (model.FotoPublicacion != null)
                {
                    var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "imgs/publ");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.FotoPublicacion.FileName;
                    var filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null)
                    {
                        var stream = new FileStream(filePath, mode: FileMode.Create);
                        model.FotoPublicacion.CopyTo(stream);
                        stream.Flush();
                        stream.Close();
                    }
                }
                if (await _repository.newPubl(uniqueName,User.Identity.Name, model))
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            
            return View(model);
        }

        public async Task<IActionResult> DeletePubl(int id,string name)
        {
           if(await _repository.delPubl(id))
            {
                var folderPath = Path.Combine(_hostingEnvironment.WebRootPath, "imgs/publ");
                var filePathDelete = Path.Combine(folderPath, name);

                 var fileInfo = new FileInfo(filePathDelete);
                    fileInfo.Delete();
                
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditarPubl(int id)
        {
           
          if(await _repository.EditarPubl(id, User.Identity.Name) !=null)
            {
                return View(await _repository.EditarPubl(id, User.Identity.Name));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, PublicacionViewModel viewModel)
        {
          
            if (ModelState.IsValid)
            {
                if(await _repository.Edit(id, viewModel))
                {
                    return RedirectToAction("Index", "THome");
                }

                return RedirectToAction("Index", "THome");

            }
            return RedirectToAction("Index", "THome");

        }

    }
}