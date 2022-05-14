using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public class VrstaCvetaRepository : IVrstaCvetaRepository
    {
        private readonly CvecaraContext context;
        private readonly IMapper mapper;

        public VrstaCvetaRepository(CvecaraContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public List<VrstaCveta> GetAllVrsteCvetova()
        {
            return context.VrstaCveta.ToList();
        }
        public VrstaCveta GetVrstaCvetaById(int vrstaCvetaId)
        {
            return context.VrstaCveta.FirstOrDefault(a => a.vrstaCvetaID == vrstaCvetaId);
        }
        public VrstaCveta CreateVrstaCveta(VrstaCveta vrstaCveta)
        {
            var createdEntity = context.Add(vrstaCveta);
            return mapper.Map<VrstaCveta>(createdEntity.Entity);
        }

        public void DeleteVrstaCveta(int vrstaCvetaId)
        {
            var vrstaCveta = GetVrstaCvetaById(vrstaCvetaId);
            context.Remove(vrstaCveta);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateVrstaCveta(VrstaCveta vrstaCveta)
        {
            throw new NotImplementedException();
        }
    }
}
