﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

#nullable disable

namespace UnclewoodCleanArchitecture.Infrastructure.Database.Migrations
{
    [DbContext(typeof(UnclewoodDbContext))]
    partial class UnclewoodDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Common.Entities.MealIngredient", b =>
                {
                    b.Property<Guid>("MealId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid");

                    b.HasKey("MealId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("MealIngrediants");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Common.Entities.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 2
                        });
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Meal.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("BestSeller")
                        .HasColumnType("boolean");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Promotion")
                        .HasColumnType("boolean");

                    b.Property<double>("PromotionRate")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Permission.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "mealdelete"
                        },
                        new
                        {
                            Id = 6,
                            Name = "mealadd"
                        },
                        new
                        {
                            Id = 2,
                            Name = "ingredientread"
                        },
                        new
                        {
                            Id = 3,
                            Name = "ingredientdelete"
                        },
                        new
                        {
                            Id = 7,
                            Name = "ingredientadd"
                        },
                        new
                        {
                            Id = 4,
                            Name = "userread"
                        },
                        new
                        {
                            Id = 5,
                            Name = "userdelete"
                        },
                        new
                        {
                            Id = 8,
                            Name = "useradd"
                        });
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Role.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "manager"
                        },
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        });
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdentityId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTime>("OccurredOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("UnclewoodCleanArchitecture.Domain.Role.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnclewoodCleanArchitecture.Domain.User.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Common.Entities.MealIngredient", b =>
                {
                    b.HasOne("UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient", "Ingredient")
                        .WithMany("MealIngrediants")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnclewoodCleanArchitecture.Domain.Meal.Meal", "Meal")
                        .WithMany("MealIngredients")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient", b =>
                {
                    b.OwnsMany("UnclewoodCleanArchitecture.Domain.Common.Enum.Location", "DisponibleIn", b1 =>
                        {
                            b1.Property<Guid>("IngredientId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("DisponibleInName");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("DisponibleInValue");

                            b1.HasKey("IngredientId", "Id");

                            b1.ToTable("Location");

                            b1.WithOwner()
                                .HasForeignKey("IngredientId");
                        });

                    b.OwnsOne("UnclewoodCleanArchitecture.Domain.Ingredient.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("IngredientId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric");

                            b1.HasKey("IngredientId");

                            b1.ToTable("Ingredients");

                            b1.WithOwner()
                                .HasForeignKey("IngredientId");
                        });

                    b.Navigation("DisponibleIn");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Meal.Meal", b =>
                {
                    b.OwnsMany("UnclewoodCleanArchitecture.Domain.Meal.Entities.Photo", "Photos", b1 =>
                        {
                            b1.Property<Guid>("MealId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("ContainerName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<bool>("IsMain")
                                .HasColumnType("boolean");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PublicId")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(2048)
                                .HasColumnType("character varying(2048)");

                            b1.HasKey("MealId", "Id");

                            b1.ToTable("Photo");

                            b1.WithOwner()
                                .HasForeignKey("MealId");
                        });

                    b.OwnsMany("UnclewoodCleanArchitecture.Domain.Meal.ValueObjects.Price", "Prices", b1 =>
                        {
                            b1.Property<Guid>("MealId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("Currency");

                            b1.Property<int>("Location")
                                .HasColumnType("integer")
                                .HasColumnName("Location");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("Value");

                            b1.HasKey("MealId", "Id");

                            b1.ToTable("Price");

                            b1.WithOwner()
                                .HasForeignKey("MealId");
                        });

                    b.Navigation("Photos");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Ingredient.Ingredient", b =>
                {
                    b.Navigation("MealIngrediants");
                });

            modelBuilder.Entity("UnclewoodCleanArchitecture.Domain.Meal.Meal", b =>
                {
                    b.Navigation("MealIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
