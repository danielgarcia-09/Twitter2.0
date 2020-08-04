using Database.Model;
using Database.Models;
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
    public class UsersRepository : RepositoryBase<Usuario, Twitter2Context>
    {
        private readonly Twitter2Context _context;
        public UsersRepository(Twitter2Context context) : base(context)
        {
            _context = context;
        }

        public async Task<THomeViewModel> Index(string name)
        {
            var all = await getAll();
            var users = new THomeViewModel
            {
                Usuarios = all,
                Usuario = name
            };
            return users;
        }

        public async Task<int> AgregarAmigo(string user, string name)
        {

            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.UserName == user);

            var filtro = await _context.Amigos.Where(c => c.Usuario == name).Select(s => s.Amigo).ToListAsync();
            var filtro2 = await _context.Amigos.Where(c => c.Amigo == usuario.UserName).Select(s => s.Usuario).ToListAsync();

            if (usuario != null)
            {
                var newAmigo = new Amigos
                {
                    Usuario = name,
                    Amigo = usuario.UserName
                };
                var newAmigo2 = new Amigos
                {
                    Usuario = usuario.UserName,
                    Amigo = name
                };
                if (usuario.UserName == name || filtro.Contains(usuario.UserName) || filtro2.Contains(name))
                {
                    return 2;
                }
                else
                {
                    _context.Amigos.Add(newAmigo);
                    _context.Amigos.Add(newAmigo2);
                    await _context.SaveChangesAsync();
                    return 1;
                }

            }

            return 0;
        }
    }
}
