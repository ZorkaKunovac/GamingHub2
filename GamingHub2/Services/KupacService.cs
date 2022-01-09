using AutoMapper;
using GamingHub2.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingHub2.Services
{
    public class KupacService : BaseCRUDService<Model.Kupac, Database.Kupac, KupacSearchRequest, KupacUpsertRequest, KupacUpsertRequest>
    {
        public KupacService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public override IEnumerable<Model.Kupac> Get(KupacSearchRequest search)
        {
            var entity = Context.Set<Database.Kupac>().AsQueryable();
            if (search.KorisnikId.HasValue)
            {
                entity = entity.Where(x => x.KorisnikId == search.KorisnikId.Value);
            }

            var list = entity.ToList();
            return _mapper.Map<List<Model.Kupac>>(list);
        }

        public override Model.Kupac Insert(KupacUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Kupac>(request);

            AddKupac(entity);

            return _mapper.Map<Model.Kupac>(entity);
        }

        public void AddKupac(Database.Kupac entity)
        {
            if (entity.KorisnikId == 0)
            {
                throw new ArgumentException("Invalid parameter ", "KorisnikId");
            }

            if (string.IsNullOrWhiteSpace(entity.Ime))
            {
                throw new ArgumentException("Invalid parameter ", "Ime");
            }
            else
            {
                if (!Regex.IsMatch(entity.Ime, @"^[A-Z][A-Za-z \t-]{2,50}$"))
                {
                    throw new ArgumentException("Invalid format ", "Ime");
                }
            }

            if (string.IsNullOrWhiteSpace(entity.Prezime))
            {
                throw new ArgumentException("Invalid parameter ", "Prezime");
            }
            else
            {
                if (!Regex.IsMatch(entity.Prezime, @"^[A-Z][A-Za-z \t-]{2,50}$"))
                {
                    throw new ArgumentException("Invalid format ", "Prezime");
                }
            }

            if (string.IsNullOrWhiteSpace(entity.Email))
            {
                throw new ArgumentException("Invalid parameter ", "Email");
            }
            else
            {
                if (!Regex.IsMatch(entity.Email, @"^[a-z]+[-+.'\w]+@[a-z]+\.[-.\w]+$", RegexOptions.IgnoreCase))
                {
                    throw new ArgumentException("Invalid format ", "Email");
                }
            }

            if (string.IsNullOrWhiteSpace(entity.BrojTelefona))
            {
                throw new ArgumentException("Invalid parameter ", "BrojTelefona");
            }
            else
            {
                if (!Regex.IsMatch(entity.BrojTelefona, @"^[+]?\d{3}[ ]?\d{2}[ ]?\d{3}[ ]?\d{3,4}$", RegexOptions.IgnoreCase))
                    throw new ArgumentException("Invalid format ", "BrojTelefona");
            }

            if (string.IsNullOrWhiteSpace(entity.Adresa1))
                throw new ArgumentException("Invalid parameter ", "Adresa1");
            else if (entity.Adresa1.Length < 5 || entity.Adresa1.Length > 70)
                throw new ArgumentException("Invalid format ", "Adresa1");

            if (!string.IsNullOrEmpty(entity.Adresa2) && (entity.Adresa2.Length < 2 || entity.Adresa2.Length > 70))
                throw new ArgumentException("Invalid format ", "Adresa2");

            if (string.IsNullOrWhiteSpace(entity.Drzava))
                throw new ArgumentException("Invalid parameter ", "Drzava");
            else if (entity.Drzava.Length < 3 || entity.Drzava.Length > 20)
                throw new ArgumentException("Invalid format ", "Drzava");

            if (string.IsNullOrWhiteSpace(entity.Grad))
                throw new ArgumentException("Invalid parameter ", "Grad");
            else if (entity.Grad.Length < 5 || entity.Grad.Length > 70)
                throw new ArgumentException("Invalid format ", "Grad");

            if (string.IsNullOrWhiteSpace(entity.PostanskiBroj))
                throw new ArgumentException("Invalid parameter ", "PostanskiBroj");
            else if (entity.PostanskiBroj.Length < 3 || entity.PostanskiBroj.Length > 20)
                throw new ArgumentException("Invalid format ", "PostanskiBroj");

            Context.Set<Database.Kupac>().Add(entity);
            Context.SaveChanges();
        }

    }
}