using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class CvetniAranzman_CvetRepository : ICvetniAranzman_CvetRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public CvetniAranzman_CvetRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<CvetniAranzman_Cvet> GetAllCvetniAranzman_Cvet()
        {
            return context.CvetniAranzman_Cvet.ToList();
        }

        public CvetniAranzman_Cvet GetCvetniAranzman_CvetById(int cvetniAranzman_CvetId)
        {
            return context.CvetniAranzman_Cvet.FirstOrDefault(a => a.cvetniAranzman_Cvet_ID == cvetniAranzman_CvetId);
        }

        public CvetniAranzman_Cvet CreateCvetniAranzman_Cvet(CvetniAranzman_Cvet cvetniAranzman_Cvet)
        {
            var createdEntity = context.Add(cvetniAranzman_Cvet);
            return mapper.Map<CvetniAranzman_Cvet>(createdEntity.Entity);
        }

        public void DeleteCvetniAranzman_Cvet(int cvetniAranzman_CvetId)
        {
            var cvetniAranzman_Cvet = GetCvetniAranzman_CvetById(cvetniAranzman_CvetId);
            context.Remove(cvetniAranzman_Cvet);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateCvetniAranzman_Cvet(CvetniAranzman_Cvet cvetniAranzman_Cvet)
        {
            throw new NotImplementedException();
        }
    }
}
