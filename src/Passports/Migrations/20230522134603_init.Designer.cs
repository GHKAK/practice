﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Passports.Data;
using Passports.Models;

#nullable disable

namespace Passports.Migrations
{
    [DbContext(typeof(PassportContext))]
    [Migration("20230522134603_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Passports.Models.Passport", b =>
                {
                    b.Property<short>("Series")
                        .HasColumnType("smallint");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Series", "Number");

                    b.ToTable("Passports");
                });
#pragma warning restore 612, 618
        }
    }
}