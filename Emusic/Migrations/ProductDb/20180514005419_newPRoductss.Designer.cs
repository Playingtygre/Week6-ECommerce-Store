﻿// <auto-generated />
using Emusic.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Emusic.Migrations.ProductDb
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20180514005419_newPRoductss")]
    partial class newPRoductss
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Emusic.Models.Basket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Closed");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Emusic.Models.BasketItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BasketId");

                    b.Property<long>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("ProductId");

                    b.ToTable("BasketItems");
                });

            modelBuilder.Entity("Emusic.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("ImageHref");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PolicyRequirement");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Emusic.Models.BasketItem", b =>
                {
                    b.HasOne("Emusic.Models.Basket", "Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Emusic.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
