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

                            b1.Property<int>("Id1")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id1"));

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

                            b1.HasKey("MealId", "Id1");

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
