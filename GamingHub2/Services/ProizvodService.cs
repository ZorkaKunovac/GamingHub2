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
    public class ProizvodService : BaseCRUDService<Model.Proizvod, Database.Proizvod, ProizvodSearchRequest, ProizvodInsertRequest, ProizvodUpdateRequest>, IProizvodService
    {
        public ProizvodService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override IEnumerable<Model.Proizvod> Get(ProizvodSearchRequest search = null)
        {
            var entity = Context.Set<Database.Proizvod>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entity = entity.Where(x => x.NazivProizvoda.Contains(search.Naziv));
            }

            if (!string.IsNullOrWhiteSpace(search?.NazivKonzole))
            {
                entity = entity.Where(x => x.NazivProizvoda.Contains(search.NazivKonzole));
            }

            if (search.IncludeIgraKonzola.HasValue && search.IncludeIgraKonzola == true)
            {
                entity = entity.Include(x => x.IgraKonzola);
            }

            var list = entity.ToList();
            var mappedList = _mapper.Map<List<Model.Proizvod>>(list);

            if (search.IncludeIgraKonzola.HasValue && search.IncludeIgraKonzola == true)
            {
                foreach (var item in mappedList)
                {
                    item.Slika = Context.Igra.Find(item.IgraKonzola.IgraID).SlikaLink;
                }
            }

            return mappedList;
        }

        public override Model.Proizvod GetById(int id)
        {
            var set = Context.Set<Database.Proizvod>();
            var entity = set.Include(x => x.IgraKonzola).Where(x => x.ID == id).FirstOrDefault();
            Model.Proizvod mappedEntity = _mapper.Map<Model.Proizvod>(entity);

            mappedEntity.Slika = Context.Igra.Find(entity.IgraKonzola.IgraID).SlikaLink;
            return mappedEntity;
        }

        public override Model.Proizvod Insert(ProizvodInsertRequest request)
        {
            var entity = _mapper.Map<Database.Proizvod>(request);

            AddProizvod(entity);

            return _mapper.Map<Model.Proizvod>(entity);
        }

        public void AddProizvod(Database.Proizvod entity)
        {
            if (string.IsNullOrEmpty(entity.ProdajnaCijena.ToString()))
            {
                throw new ArgumentException("Invalid parameter ", "ProdajnaCijena");
            }
            else if (entity.ProdajnaCijena < 0)
            {
                throw new ArgumentException("Invalid format ", "ProdajnaCijena");
            }
            if (!string.IsNullOrWhiteSpace(entity.Popust.ToString()) && entity.Popust < 0)
            {
                throw new ArgumentException("Invalid format ", "Popust");
            }
            if (string.IsNullOrWhiteSpace(entity.Status.ToString()))
            {
                throw new ArgumentException("Invalid parameter ", "Status");
            }

            if (entity.IgraKonzolaID == 0)
            {
                throw new ArgumentException("Invalid format ", "IgraKonzolaID");
            }

            Context.Set<Database.Proizvod>().Add(entity);
            Context.SaveChanges();
        }
    }
}
