﻿// <auto-generated />
using System;
using GamingHub2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GamingHub2.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GamingHub2.Database.Drzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("GamingHub2.Database.Igra", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatumIzlaska")
                        .HasColumnType("datetime2");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Izdavac")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("SlikaLink")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Igra");
                });

            modelBuilder.Entity("GamingHub2.Database.IgraKonzola", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzmjene")
                        .HasColumnType("datetime2");

                    b.Property<int>("IgraID")
                        .HasColumnType("int");

                    b.Property<int>("KonzolaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IgraID");

                    b.HasIndex("KonzolaID");

                    b.ToTable("IgraKonzola");
                });

            modelBuilder.Entity("GamingHub2.Database.IgraZanr", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzmjene")
                        .HasColumnType("datetime2");

                    b.Property<int>("IgraID")
                        .HasColumnType("int");

                    b.Property<int>("ZanrID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IgraID");

                    b.HasIndex("ZanrID");

                    b.ToTable("IgraZanr");
                });

            modelBuilder.Entity("GamingHub2.Database.Konzola", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detalji")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Konzola");
                });

            modelBuilder.Entity("GamingHub2.Database.KorisniciUloge", b =>
                {
                    b.Property<int>("KorisnikUlogaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIzmjene")
                        .HasColumnType("datetime2");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("UlogaId")
                        .HasColumnType("int");

                    b.HasKey("KorisnikUlogaId");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("UlogaId");

                    b.ToTable("KorisniciUloge");
                });

            modelBuilder.Entity("GamingHub2.Database.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("KorisnickoIme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LozinkaHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LozinkaSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikId");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("GamingHub2.Database.KreditnaKartica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojKartice")
                        .HasColumnType("int");

                    b.Property<int>("GodinaVazenja")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int>("MjesecVazenja")
                        .HasColumnType("int");

                    b.Property<int>("SigurnosniKod")
                        .HasColumnType("int");

                    b.Property<int>("TipKarticeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TipKarticeID");

                    b.ToTable("KreditnaKartica");
                });

            modelBuilder.Entity("GamingHub2.Database.Kupac", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa1")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Adresa2")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("PostanskiBroj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("DrzavaID");

                    b.HasIndex("KorisnikId")
                        .IsUnique();

                    b.ToTable("Kupac");
                });

            modelBuilder.Entity("GamingHub2.Database.Proizvod", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IgraKonzolaID")
                        .HasColumnType("int");

                    b.Property<string>("NazivProizvoda")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Popust")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal>("ProdajnaCijena")
                        .HasColumnType("decimal(8,2)");

                    b.Property<bool?>("Status")
                        .IsRequired()
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("IgraKonzolaID")
                        .IsUnique();

                    b.ToTable("Proizvod");
                });

            modelBuilder.Entity("GamingHub2.Database.Recenzija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatumObjave")
                        .HasColumnType("datetime2");

                    b.Property<int>("IgraId")
                        .HasColumnType("int");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VideoRecenzija")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("IgraId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("Recenzija");
                });

            modelBuilder.Entity("GamingHub2.Database.TipKartice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("TipKartice");
                });

            modelBuilder.Entity("GamingHub2.Database.Uloge", b =>
                {
                    b.Property<int>("UlogaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("UlogaId");

                    b.ToTable("Uloge");
                });

            modelBuilder.Entity("GamingHub2.Database.Zanr", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Opis")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("ID");

                    b.ToTable("Zanr");
                });

            modelBuilder.Entity("GamingHub2.Database.IgraKonzola", b =>
                {
                    b.HasOne("GamingHub2.Database.Igra", "Igra")
                        .WithMany("IgraKonzola")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.Konzola", "Konzola")
                        .WithMany("IgraKonzola")
                        .HasForeignKey("KonzolaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Igra");

                    b.Navigation("Konzola");
                });

            modelBuilder.Entity("GamingHub2.Database.IgraZanr", b =>
                {
                    b.HasOne("GamingHub2.Database.Igra", "Igra")
                        .WithMany("IgraZanr")
                        .HasForeignKey("IgraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.Zanr", "Zanr")
                        .WithMany("IgraZanr")
                        .HasForeignKey("ZanrID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Igra");

                    b.Navigation("Zanr");
                });

            modelBuilder.Entity("GamingHub2.Database.KorisniciUloge", b =>
                {
                    b.HasOne("GamingHub2.Database.Korisnik", "Korisnik")
                        .WithMany("KorisniciUloge")
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.Uloge", "Uloga")
                        .WithMany("KorisniciUloge")
                        .HasForeignKey("UlogaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("Uloga");
                });

            modelBuilder.Entity("GamingHub2.Database.KreditnaKartica", b =>
                {
                    b.HasOne("GamingHub2.Database.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.TipKartice", "TipKartice")
                        .WithMany()
                        .HasForeignKey("TipKarticeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Korisnik");

                    b.Navigation("TipKartice");
                });

            modelBuilder.Entity("GamingHub2.Database.Kupac", b =>
                {
                    b.HasOne("GamingHub2.Database.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.Korisnik", "Korisnik")
                        .WithOne("Kupac")
                        .HasForeignKey("GamingHub2.Database.Kupac", "KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drzava");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GamingHub2.Database.Proizvod", b =>
                {
                    b.HasOne("GamingHub2.Database.IgraKonzola", "IgraKonzola")
                        .WithOne("Proizvod")
                        .HasForeignKey("GamingHub2.Database.Proizvod", "IgraKonzolaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IgraKonzola");
                });

            modelBuilder.Entity("GamingHub2.Database.Recenzija", b =>
                {
                    b.HasOne("GamingHub2.Database.Igra", "Igra")
                        .WithMany()
                        .HasForeignKey("IgraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GamingHub2.Database.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Igra");

                    b.Navigation("Korisnik");
                });

            modelBuilder.Entity("GamingHub2.Database.Igra", b =>
                {
                    b.Navigation("IgraKonzola");

                    b.Navigation("IgraZanr");
                });

            modelBuilder.Entity("GamingHub2.Database.IgraKonzola", b =>
                {
                    b.Navigation("Proizvod");
                });

            modelBuilder.Entity("GamingHub2.Database.Konzola", b =>
                {
                    b.Navigation("IgraKonzola");
                });

            modelBuilder.Entity("GamingHub2.Database.Korisnik", b =>
                {
                    b.Navigation("KorisniciUloge");

                    b.Navigation("Kupac");
                });

            modelBuilder.Entity("GamingHub2.Database.Uloge", b =>
                {
                    b.Navigation("KorisniciUloge");
                });

            modelBuilder.Entity("GamingHub2.Database.Zanr", b =>
                {
                    b.Navigation("IgraZanr");
                });
#pragma warning restore 612, 618
        }
    }
}
