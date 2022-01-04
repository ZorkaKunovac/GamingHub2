using AutoMapper;
using GamingHub2.Database;
using GamingHub2.Model;
using GamingHub2.Model.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingHub2.Services
{
    public class ZanrService : BaseCRUDService<Model.Zanr,Database.Zanr, ZanrSearchRequest, ZanrUpsertRequest, ZanrUpsertRequest>, IZanrService
    {
        //NA MJESTO object SE STAVLJA SEARCHOBJECT AKO POSTOJI tipa ZanrSearchObject/ZanrSearchRequest
        //public ApplicationDbContext Context { get; set; }
        //protected readonly IMapper _mapper;

        public ZanrService(ApplicationDbContext context, IMapper mapper):base(context,mapper)
        {
        }

        //OVO SE DODA
        //public override IEnumerable<Model.Zanr> Get(ZanrSearchObject search = null)
        //{
        //    return base.Get(search);
        //}

        public override IEnumerable<Model.Zanr> Get(ZanrSearchRequest search = null)
        {
            var entity = Context.Set<Database.Zanr>().AsQueryable();

             if (!string.IsNullOrWhiteSpace(search?.Naziv))
            {
                entity = entity.Where(x => x.Naziv.StartsWith(search.Naziv));
            }

            var list = entity.ToList();

            return _mapper.Map<List<Model.Zanr>>(list);
        }

        public override Model.Zanr Insert(ZanrUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Zanr>(request);

            AddZanr(entity);

            return _mapper.Map<Model.Zanr>(entity);
        }

        public void AddZanr(Database.Zanr entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Naziv))
            {
                throw new ArgumentException("Invalid parameter ", "Naziv");
            }
            else
            {
                if (!Regex.IsMatch(entity.Naziv, @"^[A-za-z \t]{3,40}$"))
                {
                    throw new ArgumentException("Invalid format ", "Naziv");

                }
            }
            if ((entity.Opis.Length >= 500))
            {
                throw new ArgumentException("Max. number of characters ", "Opis");
            }

            Context.Set<Database.Zanr>().Add(entity);
            Context.SaveChanges();

        }




        /* public override IEnumerable<Model.Zanr> Get(ZanrSearchObject search = null)
         {
             //return base.Get(search);
             var entity = Context.Set<Database.Proizvodi>().AsQueryable();

             //WARNING: NEVER DO THIS. EXECUTES QUERY ON DB
             //entity = entity.ToList();
             if (!string.IsNullOrWhiteSpace(search?.Naziv))
             {
                 entity = entity.Where(x => x.Naziv.Contains(search.Naziv));
             }

             if (search.JedinicaMjereId.HasValue)
             {
                 entity = entity.Where(x => x.JedinicaMjereId == search.JedinicaMjereId);
             }

             if (search.VrstaId.HasValue)
             {
                 entity = entity.Where(x => x.VrstaId == search.VrstaId);
             }

             if (search?.IncludeJedinicaMjere == true)
             {
                 entity = entity.Include(x => x.JedinicaMjere);
             }

             if (search?.IncludeList.Length > 0)
             {
                 foreach (var item in search.IncludeList)
                 {
                     entity = entity.Include(item);
                 }
             }

             var list = entity.ToList();

             return _mapper.Map<List<Model.Proizvodi>>(list);
         }*/



        //public ZanrService(ApplicationDbContext context, IMapper mapper)
        //{
        //    Context = context;
        //    _mapper = mapper;
        //}
        //public IList<Model.Zanr> Get() {
        //    return Context.Zanr.ToList().Select(x => _mapper.Map<Model.Zanr>(x)).ToList();
        //}


        //[HttpGet ("{id}") ]
        //public Model.Zanr GetById(int id)
        //{
        //    return Context.Zanr.FirstOrDefault(x => x.ID == id);

        //}

        //[HttpPost]
        //public Model.Zanr Insert(Zanr zanr)
        //{
        //    Context.Zanr.Add(zanr);
        //    return zanr;
        //}


        //[HttpPut("{id}")]
        //public Model.Zanr Update(int id, Zanr zanr)
        //{
        //    var current= Context.Zanr.FirstOrDefault(x => x.ID == id);
        //    current.Naziv = zanr.Naziv;
        //    return current;
        //}
    }
}
