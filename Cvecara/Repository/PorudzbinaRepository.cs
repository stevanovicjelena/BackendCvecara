using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public PorudzbinaRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Porudzbina> GetAllPorudzbine()
        {
            return context.Porudzbina.ToList();
        }
        public Porudzbina GetPorudzbinaById(int porudzbinaId)
        {
            return context.Porudzbina.FirstOrDefault(a => a.porudzbinaID == porudzbinaId);
        }
        public Porudzbina CreatePorudzbina(Porudzbina porudzbina)
        {
            var createdEntity = context.Add(porudzbina);
            return mapper.Map<Porudzbina>(createdEntity.Entity);
        }

        public void DeletePorudzbina(int porudzbinaId)
        {
            var porudzbina = GetPorudzbinaById(porudzbinaId);
            context.Remove(porudzbina);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePorudzbina(Porudzbina porudzbina)
        {
            throw new NotImplementedException();
        }
    }
}
