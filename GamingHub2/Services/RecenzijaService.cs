using AutoMapper;
using GamingHub2.Model.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingHub2.Services
{
    public class RecenzijaService : BaseCRUDService<Model.Recenzija, Database.Recenzija, RecenzijaSearchRequest, RecenzijaUpsertRequest, RecenzijaUpsertRequest>, IRecenzijaService
    {
        public RecenzijaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<Model.Recenzija> Get(RecenzijaSearchRequest search = null)
        {
            var entity = Context.Set<Database.Recenzija>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naslov))
            {
                entity = entity.Where(x => x.Naslov.Contains(search.Naslov));
            }

            if (search.KorisnikId != 0 && search.KorisnikId.HasValue)
            {
                entity = entity.Where(x => x.KorisnikId == search.KorisnikId);
            }

            if (search.IgraId != 0 && search.IgraId.HasValue)
            {
                entity = entity.Where(x => x.IgraId == search.IgraId);
            }

            var list = entity.Include(x => x.Korisnik).Include(x => x.Igra).OrderByDescending(x => x.DatumObjave).ToList();
            return _mapper.Map<List<Model.Recenzija>>(list);
        }

        public override Model.Recenzija Insert(RecenzijaUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Recenzija>(request);

            AddRecenzija(entity);

            return _mapper.Map<Model.Recenzija>(entity);
        }

        public void AddRecenzija(Database.Recenzija entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Naslov))
            {
                throw new ArgumentException("Invalid parameter ", "Naslov");
            }
            else
            {
                if (entity.Naslov.Length < 5 || entity.Naslov.Length > 250)
                {
                    throw new ArgumentException("Invalid format ", "Naslov");

                }
            }
            if (entity.VideoRecenzija.Length >= 100)
            {
                throw new ArgumentException("Max. number of characters ", "VideoRecenzija");
            }

            if (string.IsNullOrWhiteSpace(entity.Sadrzaj))
            {
                throw new ArgumentException("Invalid parameter ", "Sadrzaj");
            }
            else
            {
                if (entity.Sadrzaj.Length < 400 || entity.Sadrzaj.Length >= 20000)
                {
                    throw new ArgumentException("Invalid format ", "Sadrzaj");

                }
            }

            if (entity.Slika.Length == 0)
            {
                throw new ArgumentException("Invalid parameter ", "Slika");
            }

            if(entity.IgraId==0)
            {
                throw new ArgumentException("Invalid parameter ", "IgraId");
            }
            if (entity.KorisnikId == 0)
            {
                throw new ArgumentException("Invalid parameter ", "KorisnikId");
            }


            Context.Set<Database.Recenzija>().Add(entity);
            Context.SaveChanges();

        }

    }
}
