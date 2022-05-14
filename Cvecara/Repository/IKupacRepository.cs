using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IKupacRepository
    {
        Kupac GetKupacById(int kupacId);
        List<Kupac> GetAllKupci();
        Kupac CreateKupac(Kupac kupac);
        void UpdateKupac(Kupac kupac);
        void DeleteKupac(int kupacId);
        bool SaveChanges();
    }
}
