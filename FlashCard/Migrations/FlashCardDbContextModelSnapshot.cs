﻿// <auto-generated />
using System;
using FlashCard.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlashCard.Migrations
{
    [DbContext(typeof(FlashCardDbContext))]
    partial class FlashCardDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlashCard.Model.Domain.Language", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LanguageName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = new Guid("54eae254-972b-4ce1-b08b-192f1ac2567f"),
                            LanguageName = "English"
                        },
                        new
                        {
                            LanguageId = new Guid("33526ce8-8842-49d6-8780-ba61be8069ab"),
                            LanguageName = "French"
                        },
                        new
                        {
                            LanguageId = new Guid("8703ae8f-2886-46e9-a0ec-4a28b29ceb5a"),
                            LanguageName = "German"
                        },
                        new
                        {
                            LanguageId = new Guid("14e4d292-a580-4a4d-9b0c-ed49a85179d2"),
                            LanguageName = "Spanish"
                        },
                        new
                        {
                            LanguageId = new Guid("92965b17-28a6-4336-b13f-d1ff1c1b1444"),
                            LanguageName = "Italian"
                        },
                        new
                        {
                            LanguageId = new Guid("c840e6bf-2cfb-4eaf-8322-95bc9f70b975"),
                            LanguageName = "Russian"
                        });
                });

            modelBuilder.Entity("FlashCard.Model.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Level", b =>
                {
                    b.Property<Guid>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LevelId");

                    b.ToTable("Levels");

                    b.HasData(
                        new
                        {
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            LevelName = "A1"
                        },
                        new
                        {
                            LevelId = new Guid("4802a577-ed4c-4646-ad2b-b9604ceac574"),
                            LevelName = "A2"
                        },
                        new
                        {
                            LevelId = new Guid("47538f2b-04ee-4bcd-bc2e-810d462081bd"),
                            LevelName = "B1"
                        },
                        new
                        {
                            LevelId = new Guid("026d2af9-62f1-441f-ab29-23ad4a1aae97"),
                            LevelName = "B2"
                        },
                        new
                        {
                            LevelId = new Guid("f3323772-4d35-4d27-a1a3-f86453c5e6df"),
                            LevelName = "C1"
                        },
                        new
                        {
                            LevelId = new Guid("ebd87f8f-2dfa-482d-bbb8-17d6ba5d2637"),
                            LevelName = "C2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Translation", b =>
                {
                    b.Property<Guid>("TranslationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SourceWordId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TargetWordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TranslationId");

                    b.HasIndex("TargetWordId");

                    b.HasIndex("SourceWordId", "TargetWordId")
                        .IsUnique();

                    b.ToTable("Translations");

                    b.HasData(
                        new
                        {
                            TranslationId = new Guid("ee963a82-4a19-49e4-84aa-8939ea4b9237"),
                            SourceWordId = new Guid("11237266-725e-45b2-b426-7abee30acfde"),
                            TargetWordId = new Guid("df279714-8907-4800-ac56-2ea35b84f467")
                        },
                        new
                        {
                            TranslationId = new Guid("45657d21-a8ef-4ea6-87a9-208a201b4f98"),
                            SourceWordId = new Guid("f96065c6-6d30-430a-8c65-3ce715d6574c"),
                            TargetWordId = new Guid("fe8dcbad-1001-4d63-8eca-a97bef5ec6ec")
                        },
                        new
                        {
                            TranslationId = new Guid("1ebac63e-6f19-4e3c-88d0-383083be1336"),
                            SourceWordId = new Guid("11237266-725e-45b2-b426-7abee30acfde"),
                            TargetWordId = new Guid("b977b0c1-1284-4f3a-8329-9040965386ad")
                        },
                        new
                        {
                            TranslationId = new Guid("bdabcef1-c344-47d3-a4a8-e23fc03a1200"),
                            SourceWordId = new Guid("b977b0c1-1284-4f3a-8329-9040965386ad"),
                            TargetWordId = new Guid("11237266-725e-45b2-b426-7abee30acfde")
                        });
                });

            modelBuilder.Entity("Word", b =>
                {
                    b.Property<Guid>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WordText")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("WordId");

                    b.HasIndex("LevelId");

                    b.HasIndex("LanguageId", "WordText")
                        .IsUnique()
                        .HasFilter("[WordText] IS NOT NULL");

                    b.ToTable("Words");

                    b.HasData(
                        new
                        {
                            WordId = new Guid("11237266-725e-45b2-b426-7abee30acfde"),
                            ImageUrl = "",
                            LanguageId = new Guid("54eae254-972b-4ce1-b08b-192f1ac2567f"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Hello"
                        },
                        new
                        {
                            WordId = new Guid("df279714-8907-4800-ac56-2ea35b84f467"),
                            ImageUrl = "",
                            LanguageId = new Guid("33526ce8-8842-49d6-8780-ba61be8069ab"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Bonjour"
                        },
                        new
                        {
                            WordId = new Guid("f96065c6-6d30-430a-8c65-3ce715d6574c"),
                            ImageUrl = "",
                            LanguageId = new Guid("8703ae8f-2886-46e9-a0ec-4a28b29ceb5a"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Guten Tag"
                        },
                        new
                        {
                            WordId = new Guid("fe8dcbad-1001-4d63-8eca-a97bef5ec6ec"),
                            ImageUrl = "",
                            LanguageId = new Guid("14e4d292-a580-4a4d-9b0c-ed49a85179d2"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Hola"
                        },
                        new
                        {
                            WordId = new Guid("cd2225d1-a9d1-454f-af85-eac853bbd45c"),
                            ImageUrl = "",
                            LanguageId = new Guid("92965b17-28a6-4336-b13f-d1ff1c1b1444"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Ciao"
                        },
                        new
                        {
                            WordId = new Guid("b977b0c1-1284-4f3a-8329-9040965386ad"),
                            ImageUrl = "",
                            LanguageId = new Guid("c840e6bf-2cfb-4eaf-8322-95bc9f70b975"),
                            LevelId = new Guid("495996f4-ee53-4769-a942-832a706b8178"),
                            WordText = "Привет"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FlashCard.Model.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FlashCard.Model.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlashCard.Model.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FlashCard.Model.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
