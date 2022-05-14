using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IVrstaCvetaRepository
    {
        VrstaCveta GetVrstaCvetaById(int vrstaCvetaId);
        List<VrstaCveta> GetAllVrsteCvetova();
        VrstaCveta CreateVrstaCveta(VrstaCveta vrstaCveta);
        void UpdateVrstaCveta(VrstaCveta vrstaCveta);
        void DeleteVrstaCveta(int vrstaCvetaId);
        bool SaveChanges();
    }
}
