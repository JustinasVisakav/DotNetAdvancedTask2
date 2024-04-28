﻿// <auto-generated />
using System;
using CatalogService.Domain.ContextKeeper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CatalogService.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240427222429_UpdatedReq")]
    partial class UpdatedReq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CatalogService.Domain.Models.CategoryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ChildId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChildId")
                        .IsUnique()
                        .HasFilter("[ChildId] IS NOT NULL");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CatalogService.Domain.Models.ItemModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("CatalogService.Domain.Models.CategoryModel", b =>
                {
                    b.HasOne("CatalogService.Domain.Models.CategoryModel", "Child")
                        .WithOne("Parent")
                        .HasForeignKey("CatalogService.Domain.Models.CategoryModel", "ChildId");

                    b.Navigation("Child");
                });

            modelBuilder.Entity("CatalogService.Domain.Models.ItemModel", b =>
                {
                    b.HasOne("CatalogService.Domain.Models.CategoryModel", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CatalogService.Domain.Models.CategoryModel", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Parent");
                });
#pragma warning restore 612, 618
        }
    }
}