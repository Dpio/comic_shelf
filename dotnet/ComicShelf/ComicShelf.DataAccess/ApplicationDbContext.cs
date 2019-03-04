using ComicShelf.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComicShelf.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Comic> Comics { get; set; }
		public DbSet<Collection> Collections { get; set; }
		public DbSet<ComicCollection> ComicCollections { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<Comic>().HasIndex(x => x.Id).IsUnique();
			modelBuilder.Entity<Collection>().HasIndex(x => x.Id).IsUnique();;
			modelBuilder.Entity<ComicCollection>().HasIndex(x => x.Id).IsUnique();;
		}
	}
}
