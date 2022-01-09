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
    public class TestAddingNewKonzola
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewKonzola()
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
        [InlineData("Konzola", "Detalji")]
        [InlineData("Konzola", "")]
        public void AddKonzola_EmptyField_ShouldWork(string naziv, string detalji)
        {
            KonzolaUpsertRequest request = new KonzolaUpsertRequest()
            {
                Naziv = naziv,
                Detalji=detalji
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KonzolaContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KonzolaService _service = new KonzolaService(_context, _mapper);
                //assert & act
                var validKonzola = _service.Insert(request);
                Assert.NotNull(validKonzola);
            }
        }


        [Theory]
        [InlineData("", "Detalji", "Naziv")]
        public void AddKonzola_EmptyField_ShouldFail(string naziv, string detalji, string param)
        {
            KonzolaUpsertRequest request = new KonzolaUpsertRequest()
            {
                Naziv = naziv,
                Detalji = detalji
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KonzolaContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                KonzolaService _service = new KonzolaService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


        [Theory]
        [InlineData("K", "Opis", "Naziv")]
        [InlineData("NazivKonzolePrekoDozvoljenogBrojaKaraktera", "", "Naziv")]

        public void AddKonzola_Format_ShouldFail(string naziv, string detalji, string param)
        {
            KonzolaUpsertRequest request = new KonzolaUpsertRequest()
            {
                Naziv = naziv,
                Detalji = detalji
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "KonzolaContext3")
            .Options;
            using (_context = new ApplicationDbContext(options))
            {
                KonzolaService _service = new KonzolaService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

    }
}
