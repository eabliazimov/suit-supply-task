﻿// <auto-generated />
using System;
using Alteration.Application.Infrastructure.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Alteration.Application.Migrations
{
    [DbContext(typeof(AlterationApplicationDbContext))]
    [Migration("20230912142421_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Alteration.Application.Domain.AlterationForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("SuitId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SuitId");

                    b.ToTable("AlterationForms");
                });

            modelBuilder.Entity("Alteration.Application.Domain.AlterationInstruction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlterationFormId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AlterationLength")
                        .HasColumnType("float");

                    b.Property<int>("AlterationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AlterationFormId");

                    b.ToTable("AlterationInstructions");
                });

            modelBuilder.Entity("Alteration.Application.Domain.AlterationInstruction", b =>
                {
                    b.HasOne("Alteration.Application.Domain.AlterationForm", "AlterationForm")
                        .WithMany("AlterationInstructions")
                        .HasForeignKey("AlterationFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlterationForm");
                });

            modelBuilder.Entity("Alteration.Application.Domain.AlterationForm", b =>
                {
                    b.Navigation("AlterationInstructions");
                });
#pragma warning restore 612, 618
        }
    }
}
