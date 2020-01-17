﻿// <auto-generated />
using System;
using DandyBox.Core.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DandyBox.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("DandyBox.Core.DataModels.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.Idol", b =>
                {
                    b.Property<int>("IdolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("IdolId");

                    b.ToTable("Idols");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.IdolMovie", b =>
                {
                    b.Property<int>("IdolId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdolId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("IdolMovie");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.MediaFile", b =>
                {
                    b.Property<int>("MediaFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MediaFileId");

                    b.HasIndex("MovieId");

                    b.ToTable("MediaFiles");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Director")
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .HasColumnType("TEXT");

                    b.Property<int>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Studio")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("MovieId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.MovieGenre", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenreId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.IdolMovie", b =>
                {
                    b.HasOne("DandyBox.Core.DataModels.Idol", "Idol")
                        .WithMany("ActorMovies")
                        .HasForeignKey("IdolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DandyBox.Core.DataModels.Movie", "Movie")
                        .WithMany("IdolMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.MediaFile", b =>
                {
                    b.HasOne("DandyBox.Core.DataModels.Movie", null)
                        .WithMany("MediaFiles")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("DandyBox.Core.DataModels.MovieGenre", b =>
                {
                    b.HasOne("DandyBox.Core.DataModels.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DandyBox.Core.DataModels.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
