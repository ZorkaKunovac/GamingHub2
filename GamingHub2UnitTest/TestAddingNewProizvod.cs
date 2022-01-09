using AutoMapper;
using GamingHub2;
using GamingHub2.Model;
using GamingHub2.Model.Requests;
using GamingHub2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GamingHub2UnitTest
{
    public class TestAddingNewProizvod
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewProizvod()
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
        [InlineData(1, 30, 0, false)]
        [InlineData(2, 20.99 , 10, true)]

        public void AddProizvod_EmptyField_ShouldWork(int igrakonzolaid, decimal prodajnacijena, decimal popust, bool? status)
        {
            ProizvodInsertRequest request = new ProizvodInsertRequest()
            {
                IgraKonzolaID = igrakonzolaid,
                ProdajnaCijena = prodajnacijena,
                Popust = popust,
                Status = status
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ProizvodContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ProizvodService _service = new ProizvodService(_context, _mapper);
                //assert & act
                var validProizvod = _service.Insert(request);
                Assert.NotNull(validProizvod);
            }
        }

        [Theory]
        [InlineData(0, 30, 0, false, "IgraKonzolaID")]
        [InlineData(2, 29.99, 10, null, "Status")]

        public void AddProizvod_EmptyField_ShouldFail(int igrakonzolaid, decimal prodajnacijena, decimal popust, bool? status, string param)
        {
            ProizvodInsertRequest request = new ProizvodInsertRequest()
            {
                IgraKonzolaID = igrakonzolaid,
                ProdajnaCijena = prodajnacijena,
                Popust = popust,
                Status = status
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ProizvodContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ProizvodService _service = new ProizvodService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


        [Theory]
        [InlineData(1, -1, 0, true, "ProdajnaCijena")]
        [InlineData(2, 10, -1, true, "Popust")]

        public void AddProizvod_Format_ShouldFail(int igrakonzolaid, decimal prodajnacijena, decimal popust, bool? status, string param)
        {
            ProizvodInsertRequest request = new ProizvodInsertRequest()
            {
                IgraKonzolaID = igrakonzolaid,
                ProdajnaCijena = prodajnacijena,
                Popust = popust,
                Status = status
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ProizvodContext3")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                ProizvodService _service = new ProizvodService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }
    }
}
