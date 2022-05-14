using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class ZaposleniRepository : IZaposleniRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public ZaposleniRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Zaposleni> GetAllZaposlene()
        {
            return context.Zaposleni.ToList();
        }
        public Zaposleni GetZaposleniById(int zaposleniId)
        {
            return context.Zaposleni.FirstOrDefault(a => a.zaposleniID == zaposleniId);
        }
        public Zaposleni CreateZaposleni(Zaposleni zaposleni)
        {
            var createdEntity = context.Add(zaposleni);
            return mapper.Map<Zaposleni>(createdEntity.Entity);
        }

        public void DeleteZaposleni(int zaposleniId)
        {
            var zaposleni = GetZaposleniById(zaposleniId);
            context.Remove(zaposleni);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateZaposleni(Zaposleni zaposleni)
        {
            throw new NotImplementedException();
        }
    }
}
