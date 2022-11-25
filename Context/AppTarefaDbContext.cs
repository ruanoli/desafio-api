using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_api.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_api.Context
{
    public class AppTarefaDbContext : DbContext
    {
        public AppTarefaDbContext(DbContextOptions<AppTarefaDbContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas {get; set;}
    }
}