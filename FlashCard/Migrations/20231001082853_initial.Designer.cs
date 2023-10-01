﻿// <auto-generated />
using FlashCard.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlashCard.Migrations
{
    [DbContext(typeof(FlashCardDbContext))]
    [Migration("20231001082853_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlashCard.Model.Domain.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"));

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = 1,
                            LanguageName = "English"
                        },
                        new
                        {
                            LanguageId = 2,
                            LanguageName = "French"
                        },
                        new
                        {
                            LanguageId = 3,
                            LanguageName = "German"
                        },
                        new
                        {
                            LanguageId = 4,
                            LanguageName = "Spanish"
                        },
                        new
                        {
                            LanguageId = 5,
                            LanguageName = "Italian"
                        },
                        new
                        {
                            LanguageId = 6,
                            LanguageName = "Russian"
                        });
                });

            modelBuilder.Entity("Level", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LevelId"));

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LevelId");

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            LevelId = 1,
                            LevelName = "A1"
                        },
                        new
                        {
                            LevelId = 2,
                            LevelName = "A2"
                        },
                        new
                        {
                            LevelId = 3,
                            LevelName = "B1"
                        },
                        new
                        {
                            LevelId = 4,
                            LevelName = "B2"
                        },
                        new
                        {
                            LevelId = 5,
                            LevelName = "C1"
                        },
                        new
                        {
                            LevelId = 6,
                            LevelName = "C2"
                        });
                });

            modelBuilder.Entity("Translation", b =>
                {
                    b.Property<int>("TranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TranslationId"));

                    b.Property<int>("SourceWordId")
                        .HasColumnType("int");

                    b.Property<int>("TargetWordId")
                        .HasColumnType("int");

                    b.HasKey("TranslationId");

                    b.HasIndex("SourceWordId");

                    b.HasIndex("TargetWordId");

                    b.ToTable("Translations");

                    b.HasData(
                        new
                        {
                            TranslationId = 1,
                            SourceWordId = 1,
                            TargetWordId = 2
                        },
                        new
                        {
                            TranslationId = 2,
                            SourceWordId = 2,
                            TargetWordId = 1
                        },
                        new
                        {
                            TranslationId = 3,
                            SourceWordId = 3,
                            TargetWordId = 4
                        },
                        new
                        {
                            TranslationId = 4,
                            SourceWordId = 4,
                            TargetWordId = 3
                        },
                        new
                        {
                            TranslationId = 5,
                            SourceWordId = 5,
                            TargetWordId = 5
                        });
                });

            modelBuilder.Entity("Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WordId"));

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<int>("LevelId")
                        .HasColumnType("int");

                    b.Property<string>("WordText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WordId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LevelId");

                    b.ToTable("Words");

                    b.HasData(
                        new
                        {
                            WordId = 1,
                            ImageUrl = "",
                            LanguageId = 1,
                            LevelId = 1,
                            WordText = "Hello"
                        },
                        new
                        {
                            WordId = 2,
                            ImageUrl = "",
                            LanguageId = 2,
                            LevelId = 1,
                            WordText = "Bonjour"
                        },
                        new
                        {
                            WordId = 3,
                            ImageUrl = "",
                            LanguageId = 3,
                            LevelId = 1,
                            WordText = "Guten Tag"
                        },
                        new
                        {
                            WordId = 4,
                            ImageUrl = "",
                            LanguageId = 4,
                            LevelId = 1,
                            WordText = "Hola"
                        },
                        new
                        {
                            WordId = 5,
                            ImageUrl = "",
                            LanguageId = 5,
                            LevelId = 1,
                            WordText = "Ciao"
                        });
                });

            modelBuilder.Entity("Translation", b =>
                {
                    b.HasOne("Word", "SourceWord")
                        .WithMany()
                        .HasForeignKey("SourceWordId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Word", "TargetWord")
                        .WithMany()
                        .HasForeignKey("TargetWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SourceWord");

                    b.Navigation("TargetWord");
                });

            modelBuilder.Entity("Word", b =>
                {
                    b.HasOne("FlashCard.Model.Domain.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Level", "Level")
                        .WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Level");
                });
#pragma warning restore 612, 618
        }
    }
}
