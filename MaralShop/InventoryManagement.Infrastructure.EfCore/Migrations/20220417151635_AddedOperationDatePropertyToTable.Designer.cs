﻿// <auto-generated />
using System;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventoryManagement.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(InventoryContext))]
    [Migration("20220417151635_AddedOperationDatePropertyToTable")]
    partial class AddedOperationDatePropertyToTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("InventoryManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Inventories", (string)null);
                });

            modelBuilder.Entity("InventoryManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.OwnsMany("InventoryManagement.Domain.InventoryAgg.InventoryOperation", "InventoryOperations", b1 =>
                        {
                            b1.Property<long>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<long>("Id"), 1L, 1);

                            b1.Property<long>("Count")
                                .HasColumnType("bigint");

                            b1.Property<long>("CurrentCount")
                                .HasColumnType("bigint");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("nvarchar(1000)");

                            b1.Property<long>("InventoryId")
                                .HasColumnType("bigint");

                            b1.Property<bool>("Operation")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("OperationDate")
                                .HasColumnType("datetime2");

                            b1.Property<long>("OperatorId")
                                .HasColumnType("bigint");

                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.HasKey("Id");

                            b1.HasIndex("InventoryId");

                            b1.ToTable("InventoryOperations", (string)null);

                            b1.WithOwner("Inventory")
                                .HasForeignKey("InventoryId");

                            b1.Navigation("Inventory");
                        });

                    b.Navigation("InventoryOperations");
                });
#pragma warning restore 612, 618
        }
    }
}