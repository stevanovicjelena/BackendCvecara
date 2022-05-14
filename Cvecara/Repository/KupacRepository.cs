using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class KupacRepository : IKupacRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public KupacRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Kupac> GetAllKupci()
        {
            return context.Kupac.ToList();
        }
        public Kupac GetKupacById(int kupacId)
        {
            return context.Kupac.FirstOrDefault(a => a.kupacID == kupacId);
        }
        public Kupac CreateKupac(Kupac kupac)
        {
            var createdEntity = context.Add(kupac);
            return mapper.Map<Kupac>(createdEntity.Entity);
        }

        public void DeleteKupac(int kupacId)
        {
            var kupac = GetKupacById(kupacId);
            context.Remove(kupac);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateKupac(Kupac kupac)
        {
            throw new NotImplementedException();
        }
    }
}
