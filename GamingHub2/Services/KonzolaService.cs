using AutoMapper;
using GamingHub2.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GamingHub2.Services
{
    [Authorize(Roles = "Administrator")]

    public class KonzolaService : BaseCRUDService<Model.Konzola, Database.Konzola, object, KonzolaUpsertRequest, KonzolaUpsertRequest>, IKonzolaService
    {
        public KonzolaService(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public override Model.Konzola Insert(KonzolaUpsertRequest request)
        {
            var entity = _mapper.Map<Database.Konzola>(request);

            AddKonzola(entity);

            return _mapper.Map<Model.Konzola>(entity);
        }

        public void AddKonzola(Database.Konzola entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Naziv))
            {
                throw new ArgumentException("Invalid parameter ", "Naziv");
            }
            else
            {
                if (!Regex.IsMatch(entity.Naziv, @"^[\w \t]{2,40}$"))
                {
                    throw new ArgumentException("Invalid format ", "Naziv");

                }
            }
            if ((entity.Detalji.Length >= 500))
            {
                throw new ArgumentException("Max. number of characters ", "Detalji");
            }

            Context.Set<Database.Konzola>().Add(entity);
            Context.SaveChanges();

        }
    }
}
