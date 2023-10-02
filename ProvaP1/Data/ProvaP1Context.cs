using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProvaP1.Models;

namespace ProvaP1.Data
{
    public class ProvaP1Context : DbContext
    {
        public ProvaP1Context (DbContextOptions<ProvaP1Context> options)
            : base(options)
        {
        }

        public DbSet<ProvaP1.Models.Cliente> Cliente { get; set; } = default!;

        public DbSet<ProvaP1.Models.Fornecedor>? Fornecedor { get; set; }

        public DbSet<ProvaP1.Models.Funcionario>? Funcionario { get; set; }

        public DbSet<ProvaP1.Models.Produto>? Produto { get; set; }

        public DbSet<ProvaP1.Models.Estoque>? Estoque { get; set; }

        public DbSet<ProvaP1.Models.Venda>? Venda { get; set; }
    }
}
