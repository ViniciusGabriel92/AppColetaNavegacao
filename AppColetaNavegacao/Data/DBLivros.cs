using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppColetaNavegacao.Models;

namespace AppColetaNavegacao.Data
{
    public class DBLivros : IdentityDbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Livro> Livros { get; set; }

        public DBLivros(DbContextOptions<DBLivros> options)
            : base(options)
        {
        }
    }
}
