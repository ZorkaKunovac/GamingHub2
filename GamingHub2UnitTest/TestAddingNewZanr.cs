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
    public class TestAddingNewZanr
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewZanr()
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
        [InlineData("Zanr", "Opis")]
        [InlineData("Zanr", "")]
        public void AddZanr_EmptyField_ShouldWork(string naziv, string opis)
        {
            ZanrUpsertRequest request = new ZanrUpsertRequest()
            {
                Naziv = naziv,
                Opis = opis
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ZanrContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ZanrService _service = new ZanrService(_context, _mapper);
                //assert & act
                var validZanr = _service.Insert(request);
                Assert.NotNull(validZanr);
            }
        }

        [Theory]
        [InlineData("", "Opis", "Naziv")]
        public void AddZanr_EmptyField_ShouldFail(string naziv, string opis, string param)
        {
            ZanrUpsertRequest request = new ZanrUpsertRequest()
            {
                Naziv = naziv,
                Opis = opis
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ZanrContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ZanrService _service = new ZanrService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));

            }
        }


        [Theory]
        //   [InlineData("Zanr", "Opis")]
        [InlineData("A", "opis", "Naziv")]
        public void AddZanr_ShouldFail(string naziv, string opis, string param)
        {
            ZanrUpsertRequest request = new ZanrUpsertRequest()
            {
                Naziv = naziv,
                Opis = opis
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ZanrContext3")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ZanrService _service = new ZanrService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


    }
}
