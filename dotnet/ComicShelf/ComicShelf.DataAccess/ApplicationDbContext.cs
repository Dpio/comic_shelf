using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ComicShelf.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Comic> Comics { get; set; }
		public DbSet<Collection> Collections { get; set; }
		public DbSet<ComicCollection> ComicCollections { get; set; }
		public DbSet<Rent> Rents { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<Comic>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<Collection>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<ComicCollection>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<Rent>().Property(e => e.Status)
				.HasConversion(v => v.ToString(),
				v => (RentStatus)Enum.Parse(typeof(RentStatus), v));
			modelBuilder.Entity<Rent>().HasIndex(x => x.Id).IsUnique();
		}
	}
}
