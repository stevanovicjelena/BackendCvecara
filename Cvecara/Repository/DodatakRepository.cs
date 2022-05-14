using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class DodatakRepository : IDodatakRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public DodatakRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Dodatak> GetAllDodaci()
        {
            return context.Dodatak.ToList();
        }
        public Dodatak GetDodatakById(int dodatakId)
        {
            return context.Dodatak.FirstOrDefault(a => a.dodatakID == dodatakId);
        }

        public Dodatak CreateDodatak(Dodatak dodatak)
        {
            var createdEntity = context.Add(dodatak);
            return mapper.Map<Dodatak>(createdEntity.Entity);
        }

        public void DeleteDodatak(int dodatakId)
        {
            var dodatak = GetDodatakById(dodatakId);
            context.Remove(dodatak);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateDodatak(Dodatak dodatak)
        {
            throw new NotImplementedException();
        }
    }
}
