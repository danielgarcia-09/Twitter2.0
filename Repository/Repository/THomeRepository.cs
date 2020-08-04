using AutoMapper;
using Database.Model;
using Database.Models;
using DTOS;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter2.ViewModels;

namespace Repository.Repository
{

    public class THomeRepository : RepositoryBase<Publicaciones, Twitter2Context>
    {
        private readonly Twitter2Context _context;
        private readonly IMapper _mapper;
        
        public THomeRepository(Twitter2Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<THomeViewModel> Home(string name)
        {
           
            var info = await _context.Usuario.FirstOrDefaultAsync(c => c.UserName == name);
            
            var filtro2 = new List<string>();
            var filtro3 = new List<string>();
            
            filtro2 = await _context.Amigos.Where(c => c.Amigo == info.UserName).Select(s => s.Usuario).ToListAsync();
            filtro3 = await _context.Amigos.Where(c => c.Usuario == info.UserName).Select(s => s.Usuario).ToListAsync();

            var Amigos = await _context.Amigos.Where(c => c.Usuario == info.UserName && c.Amigo != info.UserName).ToListAsync();

            var publicacions = await _context.Publicaciones.Where(c => filtro2.Contains(c.UserId) || filtro3.Contains(c.UserId)).OrderByDescending(a => a.Id).ToListAsync();

            if(publicacions.Count == 0)
            {
                publicacions = await _context.Publicaciones.Where(c => c.UserId == name).ToListAsync();
            }
            var viewModel = new THomeViewModel
            {
                Amigos = Amigos,
                Publicaciones = publicacions,
                Usuario = name,
                FotoUsuario = info.Foto
            };
            return viewModel;
        }

        public async Task<bool> newPubl(string foto,string name,THomeViewModel model)
        {
            if (model.TextoPublicacion == null)
            {

                return false;
            }

            if (model !=null)
            {
                
                var publ = new PublicacionViewModel
                {
                    UserId = name,
                    HoraPublicacion = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                    Texto = model.TextoPublicacion,
                    Foto = foto
                };
                var dto = _mapper.Map<PublicacionDTO>(publ);
                var final = _mapper.Map<Publicaciones>(dto);
                await Add(final);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> delPubl(int id)
        {
            
            var publicacion = await getById(id);
            var comentarios = await _context.Comentarios.Where(c => c.IdPost == id).ToListAsync();
           
            var comentariosId = await _context.Comentarios.Where(c => c.IdPost == id).Select(s => s.Id).ToListAsync();


            int[] idPost = comentariosId.ToArray();

            var replies = new List<Replies>();
            for (var i = 0; i < idPost.Length; i++)
            {
                replies.AddRange(await _context.Replies.Where(c => c.IdComentario == idPost[i]).ToListAsync());
            }
            if (publicacion !=null)
            {
               await Delete(id);
               _context.Comentarios.RemoveRange(comentarios);
               _context.Replies.RemoveRange(replies);
               await _context.SaveChangesAsync();
               return true;     
            }    

            return false;
        }

        public async Task<PublicacionViewModel> EditarPubl(int id,string user)
        {
            var publicacion = await getById(id);
            if (publicacion.UserId != user)
            {
                return null;
            }
            else
            {
                if (publicacion == null)
                {
                    return null;
                }
                else
                {

                    var newPubl = _mapper.Map<PublicacionViewModel>(publicacion);

                    return newPubl;
                }
            }
        }

        public async Task<bool> Edit(int id,PublicacionViewModel viewModel)
        {
            var publicacion = await getById(id);

            if (publicacion == null)
            {
                return false;
            }
            else
            {
                
                publicacion.HoraPublicacion = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                publicacion.Texto = viewModel.Texto;
               
                await Update(publicacion);
                await _context.SaveChangesAsync();
                return true;
            }

        }
    }
}
