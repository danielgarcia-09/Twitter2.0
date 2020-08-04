using AutoMapper;
using Database.Model;
using Database.Models;
using DTOS;
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
    public class PublicacionRepository : RepositoryBase<Publicaciones,Twitter2Context>
    {
        private readonly Twitter2Context _context;
        private readonly IMapper _mapper;
        public PublicacionRepository(Twitter2Context context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FullPublicacionViewModel> Index(int id)
        {
           
            var publicacion = await getById(id);
            if(publicacion == null)
            {
                return null;
            }
            else
            {
                var comentarios = await _context.Comentarios.Where(c => c.IdPost == id).OrderByDescending(a => a.Id).ToListAsync();
                var comentariosId = await _context.Comentarios.Where(c => c.IdPost == id).Select(s=> s.Id).ToListAsync();


                int[] idPost = comentariosId.ToArray();

                var replies = new List<Replies>();
                for (var i = 0; i < idPost.Length; i++){
                    replies.AddRange(await _context.Replies.Where(c => c.IdComentario == idPost[i]).ToListAsync());
                }
                
                var viewModel = new FullPublicacionViewModel
                {
                    Publicacion = publicacion,
                    Comentarios = comentarios,
                    Replies = replies
                };
                return viewModel;
            }
           

        }

        public async Task<bool> AddComentario(string foto,string name,int id,FullPublicacionViewModel viewModel)
        {
            if(viewModel.Comentario != null)
            {
                var vm = new ComentarioViewModel
                {
                    IdPost = id,
                    UserComm = name,
                    Comentario = viewModel.Comentario,
                    Foto = foto
                };
                var dto = _mapper.Map<ComentarioDTO>(vm);
                var final = _mapper.Map<Comentarios>(dto);
                _context.Comentarios.Add(final);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> addReply(string name, int id, FullPublicacionViewModel viewModel)
        {
            if(viewModel.Reply != null)
            {
                var dto = new RepliesDTO
                {
                    IdComentario = id,
                    HoraPublicacion = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                    Foto = name,
                    Texto = viewModel.Reply,
                    UserId = name
                };
                var final =  _mapper.Map<Replies>(dto);
                _context.Replies.Add(final);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
