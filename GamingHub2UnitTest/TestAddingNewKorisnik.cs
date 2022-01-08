using AutoMapper;
using GamingHub2;
using GamingHub2.Model.Requests;
using GamingHub2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GamingHub2UnitTest
{
    public class TestAddingNewKorisnik
    {

        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewKorisnik()
        {

            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new GamingHub2.Mapping.GamingHubProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }


        [Theory]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "ZorkaKunovac1", "Mostar2022!", "Mostar2022!", new byte[] { 0 })]
        public void AddKorisnik_EmptyField_ShouldWork(string ime, string prezime, string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                var validKorisnik = _service.Insert(request);
                Assert.NotNull(validKorisnik);
            }
        }

        [Theory]
        [InlineData("", "Kunovac", "zorka.kunovac@hotmail.com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Ime")]
        [InlineData("Zorka", "", "zorka.kunovac2@hotmail.com", "+38761000111", "ZorkaKunovac2", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Prezime")]
        [InlineData("Zorka", "Kunovac", "", "+38761000111", "ZorkaKunovac3", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac3@hotmail.com", "", "ZorkaKunovac4", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Telefon")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac4@hotmail.com", "+38761000111", "", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "KorisnickoIme")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac5@hotmail.com", "+38761000111", "ZorkaKunovac5", "", "Mostar2022!", new byte[] { 0 }, "Password")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac6@hotmail.com", "+38761000111", "ZorkaKunovac6", "Mostar2022!", "", new byte[] { 0 }, "PasswordPotvrda")]
        public void AddKorisnik_EmptyField_ShouldFail(string ime, string prezime, string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika, string param)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

        [Fact]
        public void AddKorisnik_PasswordMatch_ShouldWork()
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = "Zorka",
                Prezime = "Kunovac",
                Email = "zorka.kunovac6@hotmail.com",
                Telefon = "+38761000111",
                KorisnickoIme = "ZorkaKunovac6",
                Password = "Mostar2022!",
                PasswordPotvrda = "Mostar2022!",
                Slika = new byte[] { 0 }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "KorisnikContext3")
           .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                var validKorisnik = _service.Insert(request);
                Assert.NotNull(validKorisnik);
            }
        }
        [Fact]
        public void AddKorisnik_PasswordNoMatch_ShouldFail()
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = "Zorka",
                Prezime = "Kunovac",
                Email = "zorka.kunovac6@hotmail.com",
                Telefon = "+38761000111",
                KorisnickoIme = "ZorkaKunovac6",
                Password = "Mostar2022!",
                PasswordPotvrda = "Mostar20222!",
                Slika = new byte[] { 0 }
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "KorisnikContext4")
           .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(() => _service.Insert(request));
            }
        }

        [Theory]
        [InlineData( "zorka.kunovac@hotmail.com", "+38761000111", "ZorkaKunovac", "MostarMostar", "MostarMostar", new byte[] { 0 }, "Password")]
        [InlineData( "zorka.kunovac2@hotmail.com", "+38761000111", "ZorkaKunovac2", "M0$t", "M0$t", new byte[] { 0 }, "Password")]
        [InlineData( "zorka.kunovac3@hotmail.com", "+38761000111", "ZorkaKunovac3", "mostarMOSTAR", "mostarMOSTAR", new byte[] { 0 }, "Password")]
        [InlineData("zorka.kunovac4@hotmail.com", "+38761000111", "ZorkaKunovac4", "14253536577568", "14253536577568", new byte[] { 0 }, "Password")]
        public void AddKorisnik_PasswordFormat_ShouldFail(string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika, string param)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = "Zorka",
                Prezime = "Kunovac",
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext5")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


        [Theory]
        [InlineData("Ana-Marija", "Brlic-Mazuranic", "ana-marija@hotmail.com", "+38761000111", "Ana-Marija", "Mostar2022!", "Mostar2022!", new byte[] { 0 })]
        public void AddKorisnik_Format_ShouldWork(string ime, string prezime, string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext6")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                var validKorisnik = _service.Insert(request);
                Assert.NotNull(validKorisnik);
            }
        }

        [Theory]
        [InlineData("ParametarImePrekoDozvoljenogBrojaKaraktera1Br0j3v1", "Kunovac", "zorka.kunovac@hotmail.com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Ime")]
        [InlineData("Zorka", "ParametarPrezimePrekoDozvoljenogBrojaKarakteraBr0j3v1", "zorka.kunovac2@hotmail.com", "+38761000111", "ZorkaKunovac2", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Prezime")]
        public void AddKorisnik_Format_ShouldFail(string ime, string prezime, string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika, string param)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext7")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

        [Theory]
        [InlineData("ana-marija@hotmail.com", "+38761000111", "Ana-Marija", "Mostar2022!", "Mostar2022!", new byte[] { 0 })]
        public void AddKorisnik_EmailFormat_ShouldWork(string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = "Ana-Marija",
                Prezime = "Brlic-Mazuranic",
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext8")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                var validKorisnik = _service.Insert(request);
                Assert.NotNull(validKorisnik);
            }
        }


        [Theory]
        [InlineData("zorka.kunovac@edu.fit.ba", "+38761000111", "ZorkaKunovac4", "Mostar2022!", "Mostar2022!", "Email")]
        [InlineData("zorka.kunovac@hotmail.com", "+38761000111", "Administrator", "Mostar2022!", "Mostar2022!", "KorisnickoIme")]
        public void AddKorisnik_ExistingUser_ShouldFail(string email, string telefon, string korisnickoime, string password, string passwordpotvrda, string param)
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(databaseName: "KorisnikContext9")
             .Options;

            // Insert seed data
            using (_context = new ApplicationDbContext(options))
            {
                if (_context.Korisnik.Count() == 0)
                {
                    _context.Korisnik.Add(new GamingHub2.Database.Korisnik
                    {
                        Ime = "Zorka",
                        Prezime = "Kunovac",
                        Email = "zorka.kunovac@edu.fit.ba",
                        Telefon = "+387 61 000 1113",
                        KorisnickoIme = "Administrator",
                        LozinkaSalt = "Mostar2022!",
                        LozinkaHash = "Mostar2022!",
                        Slika = new byte[] { 0 }
                    });

                    _context.SaveChanges();
                }
            }

            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = "Zorka",
                Prezime = "Kunovac",
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = new byte[] { 0 }
            };

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

        [Theory]
        [InlineData("Zorka", "Kunovac", "example.com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        [InlineData("Zorka", "Kunovac", "zorka@hotmail", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        [InlineData("Zorka", "Kunovac", "myemail@address,com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        [InlineData("Zorka", "Kunovac", "user4 @company.com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        [InlineData("Zorka", "Kunovac", "name@again@example.com", "+38761000111", "ZorkaKunovac", "Mostar2022!", "Mostar2022!", new byte[] { 0 }, "Email")]
        public void AddKorisnik_EmailFormat_ShouldFail(string ime, string prezime, string email, string telefon, string korisnickoime, string password, string passwordpotvrda, byte[] slika, string param)
        {
            KorisniciUpsertRequest request = new KorisniciUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                KorisnickoIme = korisnickoime,
                Password = password,
                PasswordPotvrda = passwordpotvrda,
                Slika = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KorisnikContext10")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


    }
}