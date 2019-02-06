﻿// <auto-generated />
using System;
using ComicShelf.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ComicShelf.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("Name");

                    b.Property<int?>("UserCollectionId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserCollectionId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<byte>("Image");

                    b.Property<string>("Issue");

                    b.Property<string>("Note");

                    b.Property<string>("Publisher");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.ComicCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ComicId");

                    b.Property<int?>("UserCollectionId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserCollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("ComicCollections");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("GivenName");

                    b.Property<string>("GoogleId");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.UserCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CollectionId");

                    b.Property<int>("ComicCollectionId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("UserCollections");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.Collection", b =>
                {
                    b.HasOne("ComicShelf.DataAccess.Entities.UserCollection")
                        .WithMany("Collection")
                        .HasForeignKey("UserCollectionId");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.ComicCollection", b =>
                {
                    b.HasOne("ComicShelf.DataAccess.Entities.Comic", "Comic")
                        .WithMany("ComicsCollection")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComicShelf.DataAccess.Entities.UserCollection")
                        .WithMany("ComicCollections")
                        .HasForeignKey("UserCollectionId");

                    b.HasOne("ComicShelf.DataAccess.Entities.User", "User")
                        .WithMany("ComicsCollection")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
