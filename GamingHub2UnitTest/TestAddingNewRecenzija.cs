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
    public class TestAddingNewRecenzija
    {
        public static ApplicationDbContext _context;
        private static IMapper _mapper;

        public TestAddingNewRecenzija()
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

        [Fact]
        public void AddRecenzija_EmptyField_ShouldWork()
        {
            RecenzijaUpsertRequest request = new RecenzijaUpsertRequest()
            {
                Naslov = "Monster Hunt",
                Sadrzaj = "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.",
                Slika = new byte[] { 0 },
                VideoRecenzija = "https://www.youtube.com/watch?v=YRflCrrr-fw",
                KorisnikId = 1,
                IgraId = 1,
                DatumObjave = DateTime.Now
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "RecenzijaContext1")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                RecenzijaService _service = new RecenzijaService(_context, _mapper);
                //assert & act
                var validRecenzija = _service.Insert(request);
                Assert.NotNull(validRecenzija);
            }
        }

        [Theory]
        [InlineData("", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", new byte[] { 0 }, "LinkVideoRecenzije", 1, 1,"Naslov")]
        [InlineData("Monster Hunt", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", null, "LinkVideoRecenzije", 1, 1, "Slika")]
        [InlineData("Monster Hunt", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", new byte[] { 0 }, "LinkVideoRecenzije", 0, 1, "KorisnikId")]
        [InlineData("Monster Hunt", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", new byte[] { 0 }, "LinkVideoRecenzije", 1, 0, "IgraId")]

        [InlineData("Monster Hunt", "", new byte[] { 0 }, "LinkVideoRecenzije", 1, 1,"Sadrzaj")]

        public void AddRecenzija_EmptyField_ShouldFail(string naslov, string sadrzaj, byte[] slika, string videorencezija, int korisnikid, int igraid, string param)
        {
            RecenzijaUpsertRequest request = new RecenzijaUpsertRequest()
            {
                Naslov = naslov,
                Sadrzaj = sadrzaj,
                Slika = slika,
                VideoRecenzija = videorencezija,
                KorisnikId = korisnikid,
                IgraId = igraid,
                DatumObjave = DateTime.Now
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "RecenzijaContext2")
            .Options;

            using (_context = new ApplicationDbContext(options))
            {
                RecenzijaService _service = new RecenzijaService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }

        [Theory]
        [InlineData("Game", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", new byte[] { 0 }, "LinkVideoRecenzije", 1, 1, "Naslov")]
        [InlineData("Monster Hunt", "Kratki sadrzaj", new byte[] { 0 }, "LinkVideoRecenzije", 1, 1, "Sadrzaj")]
        [InlineData("Monster Hunt", "Lov je u igrama nekoć bio aktivnost rezervirana za Cabeline igre C produkcije. Situacija se počela mijenjati tamo negdje dolaskom Red Dead Redemptiona, a onda je uslijedio val igara u kojima je lov bio jedna od osnovnih mehanika. Far Cry 3, The Witcher 3 i Horizon Zero Dawn samo su neke od igara u kojima smo predatorski vrebali plijen, nerijetko veći od likova koje smo kontrolirali. No, prije svih tih igara postojao je Monster Hunter.", new byte[] { 0 }, "https://www.youtube.com/watch?v=YRflCrrr-fwhttps://www.youtube.com/watch?v=YRflCrrr-fwhttps://www.youtube.com/", 1, 1, "VideoRecenzija")]
        public void AddRecenzija_Format_ShouldFail(string naslov, string sadrzaj, byte[] slika, string videorencezija, int korisnikid, int igraid, string param)
        {
            RecenzijaUpsertRequest request = new RecenzijaUpsertRequest()
            {
                Naslov = naslov,
                Sadrzaj = sadrzaj,
                Slika = slika,
                VideoRecenzija = videorencezija,
                KorisnikId = korisnikid,
                IgraId = igraid,
                DatumObjave = DateTime.Now
            };
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(databaseName: "RecenzijaContext3")
                      .Options;

            using (_context = new ApplicationDbContext(options))
            {
                RecenzijaService _service = new RecenzijaService(_context, _mapper);
                //assert & act
                Assert.Throws<ArgumentException>(param, () => _service.Insert(request));
            }
        }


    }
}
