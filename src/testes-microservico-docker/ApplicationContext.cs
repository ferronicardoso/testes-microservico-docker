using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestesMicroservicoDocker.Entities;
using TestesMicroservicoDocker.Mapping;

namespace TestesMicroservicoDocker;

public class ApplicationContext : DbContext
{
    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Pessoatipo> Pessoatipos { get; set; }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}
