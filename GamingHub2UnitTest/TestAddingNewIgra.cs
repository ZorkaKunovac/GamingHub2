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
    public class TestAddingNewIgra
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewIgra()
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
        //   [InlineData("Half-Life VR", "Developer", "Izdavac", "2012-11-1", new byte[] { 0 })]
        [InlineData("Grand Theft Auto VI", "Rockstar Games", "Rockstar Games", null, null)]
        [InlineData("Witcher 4", "Rockstar Games", "Rockstar Games", null, new byte[] { 0 })]
        public void AddIgra_EmptyField_ShouldWork(string naziv, string developer, string izdavac, DateTime? datumizlaska, byte[] slika)
        {
            IgraUpsertRequest request = new IgraUpsertRequest()
            {
                Naziv = naziv,
                Developer = developer,
                Izdavac = izdavac,
                DatumIzlaska = datumizlaska,
                SlikaLink = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "IgraContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                IgraService _service = new IgraService(_context, _mapper);
                //assert & act
                var validIgra = _service.Insert(request);
                Assert.NotNull(validIgra);
            }
        }

        [Theory]
        [InlineData("", "Developer", "Izdavac", null, null, "Naziv")]
        [InlineData("NazivIgre", "", "Izdavac", null, null, "Developer")]
        [InlineData("NazivIgre", "Developer", "", null, null, "Izdavac")]

        public void AddIgra_EmptyField_ShouldFail(string naziv, string developer, string izdavac, DateTime? datumizlaska, byte[] slika, string param)
        {
            IgraUpsertRequest request = new IgraUpsertRequest()
            {
                Naziv = naziv,
                Developer = developer,
                Izdavac = izdavac,
                DatumIzlaska = datumizlaska,
                SlikaLink = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(databaseName: "IgraContext2")
              .Options;

            using (_context = new ApplicationDbContext(options))
            {
                IgraService _service = new IgraService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


        [Theory]
        [InlineData("Igra", "Developer", "Izdavac", null, new byte[] { 0 }, "Naziv")]
        [InlineData("NazivIgrePrekoDozvoljenogBrojaKarakteraNekiNazivIgre", "Developer", "Izdavac", null, new byte[] { 0 }, "Naziv")]
        [InlineData("Final Fantasy XV", "D", "Izdavac", null, new byte[] { 0 }, "Developer")]
        [InlineData("NazivIgre", "Developer", "I", null, new byte[] { 0 }, "Izdavac")]
        public void AddIgra_Format_ShouldFail(string naziv, string developer, string izdavac, DateTime? datumizlaska, byte[] slika, string param)
        {
            IgraUpsertRequest request = new IgraUpsertRequest()
            {
                Naziv = naziv,
                Developer = developer,
                Izdavac = izdavac,
                DatumIzlaska = datumizlaska,
                SlikaLink = slika
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "IgraContext3")
               .Options;

            using (_context = new ApplicationDbContext(options))
            {
                IgraService _service = new IgraService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

    }
}
