using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class TipDodatkaRepository : ITipDodatkaRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public TipDodatkaRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<TipDodatka> GetAllTipoveDodataka(string nazivTipa)
        {
            var tipoviDodataka = context.TipDodatka.ToList();
            return (from td in tipoviDodataka
                    where string.IsNullOrEmpty(nazivTipa) || td.nazivTipaDodatka == nazivTipa
                    select td).ToList();
        }
        public TipDodatka GetTipDodatkaById(int tipDodatkaId)
        {
            return context.TipDodatka.FirstOrDefault(a => a.tipDodatkaID == tipDodatkaId);
        }
        public TipDodatka CreateTipDodatka(TipDodatka tipDodatka)
        {
            var createdEntity = context.Add(tipDodatka);
            return mapper.Map<TipDodatka>(createdEntity.Entity);
        }

        public void DeleteTipDodatka(int tipDodatkaId)
        {
            var tipDodatka = GetTipDodatkaById(tipDodatkaId);
            context.Remove(tipDodatka);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateTipDodatka(TipDodatka tipDodatka)
        {
            throw new NotImplementedException();
        }
    }
}
