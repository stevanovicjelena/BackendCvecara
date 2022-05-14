using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class PakovanjeRepository : IPakovanjeRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public PakovanjeRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Pakovanje> GetAllPakovanja()
        {
            return context.Pakovanje.ToList();
        }
        public Pakovanje GetPakovanjeById(int pakovanjeId)
        {
            return context.Pakovanje.FirstOrDefault(a => a.pakovanjeID == pakovanjeId);
        }
        public Pakovanje CreatePakovanje(Pakovanje pakovanje)
        {
            var createdEntity = context.Add(pakovanje);
            return mapper.Map<Pakovanje>(createdEntity.Entity);
        }

        public void DeletePakovanje(int pakovanjeId)
        {
            var pakovanje = GetPakovanjeById(pakovanjeId);
            context.Remove(pakovanje);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdatePakovanje(Pakovanje pakovanje)
        {
            throw new NotImplementedException();
        }
    }
}
