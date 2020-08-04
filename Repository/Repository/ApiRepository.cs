using AutoMapper;
using Database.Model;
using Database.Models;
using DTOS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter2.ViewModels;

namespace Repository.Repository
{
    public class ApiRepository : RepositoryBase<Publicaciones, Twitter2Context>
    {
        private readonly Twitter2Context _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public ApiRepository(Twitter2Context context, IMapper mapper,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

       
        public async Task<AllDTO> All(string user)
        {
            var publicacion = await _context.Publicaciones.FirstOrDefaultAsync(c => c.UserId == user);
            if (publicacion == null)
            {
                return null;
            }
            else
            {
                var comentarios = await _context.Comentarios.Where(c => c.IdPost == publicacion.Id).OrderByDescending(a => a.Id).ToListAsync();
                var comentariosId = await _context.Comentarios.Where(c => c.IdPost == publicacion.Id).Select(s => s.Id).ToListAsync();


                int[] idPost = comentariosId.ToArray();

                var replies = new List<Replies>();
                for (var i = 0; i < idPost.Length; i++)
                {
                    replies.AddRange(await _context.Replies.Where(c => c.IdComentario == idPost[i]).ToListAsync());
                }
                var publicacion2 = await _context.Publicaciones.Where(e => e.UserId == user).ToListAsync();
                var dto = new AllDTO
                {
                    Publicacion = publicacion2,
                    Comentarios = comentarios,
                    Replies = replies
                };
                return dto;
            }
        }

        public async Task<List<AmigosDTO>> Amigos(string user)
        {
            var Amigos = await _context.Amigos.Where(c => c.Usuario == user && c.Amigo != user).ToListAsync();
            if(Amigos == null)
            {
                return null;
            }
            var dto = new List<AmigosDTO>();
            foreach(var item in Amigos)
            {
                var i = new AmigosDTO
                {
                    Id = item.Id,
                    Usuario = item.Usuario,
                    Amigo = item.Amigo
                };
                dto.Add(i);
            }
            
            return dto;
        }

        public async Task<bool> Publicar(ApiPublicacionDTO data)
        {
            var user = await _userManager.FindByNameAsync(data.user);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(data.user, data.password, false, false);
                if (result.Succeeded)
                {
                    var publicacion = new Publicaciones
                    {
                        HoraPublicacion = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                        UserId = data.user,
                        Texto = data.texto
                    };
                    await Add(publicacion);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> addAmigo(ApiAmigosDTO data)
        {
            var user = await _userManager.FindByNameAsync(data.user);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(data.user, data.password, false, false);
                if (result.Succeeded)
                {             
                                     
                        var newAmigo = new Amigos
                        {
                            Usuario = data.amigo,
                            Amigo = data.user
                        };
                        var newAmigo2 = new Amigos
                        {
                            Usuario = data.user,
                            Amigo = data.amigo
                        };
                       

                            _context.Amigos.Add(newAmigo);
                            _context.Amigos.Add(newAmigo2);
                            await _context.SaveChangesAsync();
                            return true;
                        

                }

                    return false;
                
            }
                return false;
            
         
        }

       
    }
}
