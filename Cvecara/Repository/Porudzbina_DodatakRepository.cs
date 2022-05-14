using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class Porudzbina_DodatakRepository : IPorudzbina_DodatakRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public Porudzbina_DodatakRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Porudzbina_Dodatak> GetAllPorudzbina_Dodatak()
        {
            return context.Porudzbina_Dodatak.ToList();
        }
        public Porudzbina_Dodatak GetPorudzbina_DodatakById(int porudzbina_DodatakId)
        {
            return context.Porudzbina_Dodatak.FirstOrDefault(a => a.porudzbina_Dodatak_ID == porudzbina_DodatakId);
        }

        public Porudzbina_Dodatak CreatePorudzbina_Dodatak(Porudzbina_Dodatak porudzbina_Dodatak)
        {
            var createdEntity = context.Add(porudzbina_Dodatak);
            return mapper.Map<Porudzbina_Dodatak>(createdEntity.Entity);
        }

        public void DeletePorudzbina_Dodatak(int porudzbina_DodatakId)
        {
            var paorudzbina_Dodatak = GetPorudzbina_DodatakById(porudzbina_DodatakId);
            context.Remove(paorudzbina_Dodatak);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePorudzbina_Dodatak(Porudzbina_Dodatak porudzbina_Dodatak)
        {
            throw new NotImplementedException();
        }
    }
}
