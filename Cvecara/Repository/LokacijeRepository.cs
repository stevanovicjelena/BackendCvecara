using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class LokacijeRepository : ILokacijeRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public LokacijeRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Lokacije> GetAllLokacije()
        {
            return context.Lokacije.ToList();
        }
        public Lokacije GetLokacijeById(int lokacijeId)
        {
            return context.Lokacije.FirstOrDefault(a => a.lokacijaID == lokacijeId);
        }
        public Lokacije CreateLokacije(Lokacije lokacije)
        {
            var createdEntity = context.Add(lokacije);
            return mapper.Map<Lokacije>(createdEntity.Entity);
        }

        public void DeleteLokacije(int lokacijeId)
        {
            var lokacija = GetLokacijeById(lokacijeId);
            context.Remove(lokacija);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateLokacije(Lokacije lokacije)
        {
            throw new NotImplementedException();
        }
    }
}
