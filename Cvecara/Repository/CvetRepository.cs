using AutoMapper;
using Cvecara.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class CvetRepository : ICvetRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public CvetRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<Cvet> GetAllCvetovi()
        {
            return context.Cvet.ToList();
        }

        public Cvet GetCvetById(int cvetId)
        {
            return context.Cvet.FirstOrDefault(a => a.cvetID == cvetId);
        }

        public Cvet CreateCvet(Cvet cvet)
        {
            var createdEntity = context.Add(cvet);
            return mapper.Map<Cvet>(createdEntity.Entity);
        }
        public void UpdateCvet(Cvet cvet)
        {
            throw new NotImplementedException();
        }

        public void DeleteCvet(int cvetId)
        {
            var cvet = GetCvetById(cvetId);
            context.Remove(cvet);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    }
}
