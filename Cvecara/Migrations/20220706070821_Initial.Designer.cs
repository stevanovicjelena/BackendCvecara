﻿// <auto-generated />
using System;
using Cvecara.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cvecara.Migrations
{
    [DbContext(typeof(CvecaraContext))]
    [Migration("20220706070821_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cvecara.Entities.Cvet", b =>
                {
                    b.Property<int>("cvetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bojaCveta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("cenaCveta")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("kolicina")
                        .HasColumnType("int");

                    b.Property<int>("vrstaCvetaID")
                        .HasColumnType("int");

                    b.HasKey("cvetID");

                    b.ToTable("Cvet");
                });

            modelBuilder.Entity("Cvecara.Entities.CvetniAranzman", b =>
                {
                    b.Property<int>("cvetniAranzmanID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("cenaAranzmana")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("kolicina")
                        .HasColumnType("int");

                    b.Property<string>("nazivAranzmana")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opisAranzmana")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pakovanjeID")
                        .HasColumnType("int");

                    b.HasKey("cvetniAranzmanID");

                    b.ToTable("CvetniAranzman");
                });

            modelBuilder.Entity("Cvecara.Entities.CvetniAranzman_Cvet", b =>
                {
                    b.Property<int>("cvetniAranzman_Cvet_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("brojCvetova")
                        .HasColumnType("int");

                    b.Property<int>("cvetID")
                        .HasColumnType("int");

                    b.Property<int>("cvetniAranzmanID")
                        .HasColumnType("int");

                    b.HasKey("cvetniAranzman_Cvet_ID");

                    b.ToTable("CvetniAranzman_Cvet");
                });

            modelBuilder.Entity("Cvecara.Entities.Dodatak", b =>
                {
                    b.Property<int>("dodatakID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bojaDodatka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("cenaDodatka")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("kolicina")
                        .HasColumnType("int");

                    b.Property<int>("tipDodatkaID")
                        .HasColumnType("int");

                    b.HasKey("dodatakID");

                    b.ToTable("Dodatak");
                });

            modelBuilder.Entity("Cvecara.Entities.Lokacije", b =>
                {
                    b.Property<int>("lokacijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nazivLokacije")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("lokacijaID");

                    b.ToTable("Lokacije");
                });

            modelBuilder.Entity("Cvecara.Entities.Pakovanje", b =>
                {
                    b.Property<int>("pakovanjeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bojaPakovanja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("cenaPakovanja")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("kolicina")
                        .HasColumnType("int");

                    b.Property<string>("nazivPakovanja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opisPakovanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("pakovanjeID");

                    b.ToTable("Pakovanje");
                });

            modelBuilder.Entity("Cvecara.Entities.Porudzbina", b =>
                {
                    b.Property<int>("porudzbinaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("cenaPorudzbine")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("cvetniAranzmanID")
                        .HasColumnType("int");

                    b.Property<DateTime>("datumPorudzbine")
                        .HasColumnType("datetime2");

                    b.Property<int>("kolicina")
                        .HasColumnType("int");

                    b.Property<int>("kupacID")
                        .HasColumnType("int");

                    b.Property<int>("lokacijaID")
                        .HasColumnType("int");

                    b.Property<string>("statusPorudzbine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("zaposleniID")
                        .HasColumnType("int");

                    b.HasKey("porudzbinaID");

                    b.ToTable("Porudzbina");
                });

            modelBuilder.Entity("Cvecara.Entities.Porudzbina_Dodatak", b =>
                {
                    b.Property<int>("porudzbina_Dodatak_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("dodatakID")
                        .HasColumnType("int");

                    b.Property<int>("kolicinaDodatka")
                        .HasColumnType("int");

                    b.Property<int>("porudzbinaID")
                        .HasColumnType("int");

                    b.HasKey("porudzbina_Dodatak_ID");

                    b.ToTable("Porudzbina_Dodatak");
                });

            modelBuilder.Entity("Cvecara.Entities.TipDodatka", b =>
                {
                    b.Property<int>("tipDodatkaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nazivTipaDodatka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opisTipaDodatka")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipDodatkaID");

                    b.ToTable("TipDodatka");
                });

            modelBuilder.Entity("Cvecara.Entities.User", b =>
                {
                    b.Property<int>("userID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("emailUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imeUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("korisnickoImeUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lozinkaUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prezimeUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("uloga")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userID");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("Cvecara.Entities.VrstaCveta", b =>
                {
                    b.Property<int>("vrstaCvetaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nazivVrste")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("opisVrste")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("vrstaCvetaID");

                    b.ToTable("VrstaCveta");
                });
#pragma warning restore 612, 618
        }
    }
}
