using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class CvetniAranzmanRepository : ICvetniAranzmanRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public CvetniAranzmanRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<CvetniAranzman> GetAllCvetniAranzmani()
        {
            return context.CvetniAranzman.ToList();
        }

        public CvetniAranzman GetCvetniAranzmanById(int cvetniAranzmanId)
        {
            return context.CvetniAranzman.FirstOrDefault(a => a.cvetniAranzmanID == cvetniAranzmanId);
        }

        public CvetniAranzman CreateCvetniAranzman(CvetniAranzman cvetniAranzman)
        {
            var createdEntity = context.Add(cvetniAranzman);
            return mapper.Map<CvetniAranzman>(createdEntity.Entity);
        }

        public void DeleteCvetniAranzman(int cvetniAranzmanId)
        {
            var cvetniAranzman = GetCvetniAranzmanById(cvetniAranzmanId);
            context.Remove(cvetniAranzman);
        }

        public void UpdateCvetniAranzman(CvetniAranzman cvetniAranzman)
        {
            throw new NotImplementedException();
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
