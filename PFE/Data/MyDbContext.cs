using Microsoft.EntityFrameworkCore;
using PFE.Models;

namespace PFE.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Collab> Collabs { get; set; }

        public DbSet<Projet> Projets { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<CollabProjet> CollabProjets { get; set; }
        public DbSet<Objectif> Objectifs { get; set; }
    }
}
