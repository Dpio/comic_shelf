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

                    b.Property<bool>("IsWantList");

                    b.Property<string>("Name");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cover");

                    b.Property<string>("Draftsman");

                    b.Property<byte[]>("Image");

                    b.Property<int>("Issue");

                    b.Property<string>("Note");

                    b.Property<DateTime>("PremierDate");

                    b.Property<string>("Publisher");

                    b.Property<string>("ScriptWriter");

                    b.Property<string>("Series");

                    b.Property<string>("Title");

                    b.Property<string>("Translator");

                    b.Property<int>("Volume");

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

                    b.Property<int>("CollectionId");

                    b.Property<int>("ComicId");

                    b.HasKey("Id");

                    b.HasIndex("CollectionId");

                    b.HasIndex("ComicId");

                    b.HasIndex("Id")
                        .IsUnique();

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

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.Collection", b =>
                {
                    b.HasOne("ComicShelf.DataAccess.Entities.User")
                        .WithMany("Collections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ComicShelf.DataAccess.Entities.ComicCollection", b =>
                {
                    b.HasOne("ComicShelf.DataAccess.Entities.Collection", "Collection")
                        .WithMany("ComicsCollection")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ComicShelf.DataAccess.Entities.Comic", "Comic")
                        .WithMany()
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
