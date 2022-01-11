using AutoMapper;
using GamingHub2;
using GamingHub2.Helper;
using GamingHub2.Model;
using GamingHub2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GamingHub2UnitTest
{
    public class TestLogin
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestLogin()
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
        [InlineData("Administrator", "test12345")]
        public void Login_ShouldWork(string username, string password)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "LoginContext1")
            .Options;

            var salt = LozinkaHash.GenerateSalt();
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
                        LozinkaSalt = salt,
                        LozinkaHash = LozinkaHash.GenerateHash(salt, "test12345"),
                        Slika = new byte[] { 0 }
                    });
                    _context.SaveChanges();
                }
            }
            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                var validLogin = _service.Authenticiraj(username, password);
                Assert.NotNull(validLogin);
            }
        }


        [Theory]
        [InlineData("Moderator", "test12345")]
        [InlineData("Administrator", "test")]
        [InlineData("", "test")]
        [InlineData("Administrator", "")]

        public void Login_ShouldFail(string username, string password)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "LoginContext2")
            .Options;

            var salt = LozinkaHash.GenerateSalt();
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
                        LozinkaSalt = salt,
                        LozinkaHash = LozinkaHash.GenerateHash(salt, "test12345"),
                        Slika = new byte[] { 0 }
                    });
                    _context.SaveChanges();
                }
            }
            using (_context = new ApplicationDbContext(options))
            {
                KorisniciService _service = new KorisniciService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(() => _service.Authenticiraj(username, password));
            }
        }


    }
}
