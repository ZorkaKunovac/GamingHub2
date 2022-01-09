using AutoMapper;
using GamingHub2;
using GamingHub2.Model.Requests;
using GamingHub2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GamingHub2UnitTest
{
    public class TestAddingNewKupac
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewKupac()
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
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000",1)]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "", "Mostar", "Bosna i Hercegovina", "88000", 1)]
        public void AddKupac_EmptyField_ShouldWork(string ime, string prezime, string email, string brojTelefona, string adresa1, string adresa2, string grad, string drzava, string postanskiBroj , int korisnikId)
        {
            KupacUpsertRequest request = new KupacUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                BrojTelefona = brojTelefona,
                Adresa1 = adresa1,
                Adresa2 = adresa2,
                Grad = grad,
                Drzava = drzava,
                PostanskiBroj = postanskiBroj,
                KorisnikId = korisnikId
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KupacContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KupacService _service = new KupacService(_context, _mapper);
                //assert & act
                var validKupac = _service.Insert(request);
                Assert.NotNull(validKupac);
            }
        }

        [Theory]
        [InlineData("", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Ime")]
        [InlineData("Zorka", "", "zorka.kunovac@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Prezime")]
        [InlineData("Zorka", "Kunovac", "", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Email")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "BrojTelefona")]

        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Adresa1")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "", "Bosna i Hercegovina", "88000", 1, "Grad")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "", "88000", 1, "Drzava")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "", 1, "PostanskiBroj")]
        [InlineData("Zorka", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 0, "KorisnikId")]
        public void AddKupac_EmptyField_ShouldFail(string ime, string prezime, string email, string brojTelefona, string adresa1, string adresa2, string grad, string drzava, string postanskiBroj, int korisnikId, string param)
        {
            KupacUpsertRequest request = new KupacUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                BrojTelefona = brojTelefona,
                Adresa1 = adresa1,
                Adresa2 = adresa2,
                Grad = grad,
                Drzava = drzava,
                PostanskiBroj = postanskiBroj,
                KorisnikId = korisnikId
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KupacContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KupacService _service = new KupacService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

        [Theory]
        [InlineData("Ana-Marija", "Brlic-Mazuranic", "ana-marija@hotmail.com", "+38761000111", "Neka-Adresa", "Neka-Adresa", "Mostar", "Bosna i Hercegovina","SlovniPostanskiBroj",2)]

        public void AddKupac_Format_ShouldWork(string ime, string prezime, string email, string brojTelefona, string adresa1, string adresa2, string grad, string drzava, string postanskiBroj, int korisnikId)
        {
            KupacUpsertRequest request = new KupacUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                BrojTelefona = brojTelefona,
                Adresa1 = adresa1,
                Adresa2 = adresa2,
                Grad = grad,
                Drzava = drzava,
                PostanskiBroj = postanskiBroj,
                KorisnikId = korisnikId
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KupacContext3")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KupacService _service = new KupacService(_context, _mapper);
                //assert & act
                var validKupac = _service.Insert(request);
                Assert.NotNull(validKupac);
            }
        }

        [Theory]
        [InlineData("ParametarImePrekoDozvoljenogBrojaKaraktera1Br0j3v1", "Kunovac", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Ime")]
        [InlineData("Zorka", "ParametarPrezimePrekoDozvoljenogBrojaKarakteraBr0j3v1", "zorka.kunovac1@hotmail.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Prezime")]
        [InlineData("Zorka", "Kunovac", "example.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Email")]
        [InlineData("Zorka", "Kunovac", "zorka@example.com", "+3876100011231", "Sjeverni logor", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "BrojTelefona")]
        [InlineData("Zorka", "Kunovac", "zorka@example.com", "+38761000111", "New", "Sjeverni logor", "Mostar", "Bosna i Hercegovina", "88000", 1, "Adresa1")]
        [InlineData("Zorka", "Kunovac", "zorka@example.com", "+38761000111", "Sjeverni logor", "Sjeverni logor", "New", "Bosna i Hercegovina", "88000", 1, "Grad")]

        public void AddKupac_Format_ShouldFail(string ime, string prezime, string email, string brojTelefona, string adresa1, string adresa2, string grad, string drzava, string postanskiBroj, int korisnikId, string param)
        {
            KupacUpsertRequest request = new KupacUpsertRequest()
            {
                Ime = ime,
                Prezime = prezime,
                Email = email,
                BrojTelefona = brojTelefona,
                Adresa1 = adresa1,
                Adresa2 = adresa2,
                Grad = grad,
                Drzava = drzava,
                PostanskiBroj = postanskiBroj,
                KorisnikId = korisnikId
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KupacContext4")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KupacService _service = new KupacService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

    }
}
