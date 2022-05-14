using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IPakovanjeRepository
    {
        Pakovanje GetPakovanjeById(int pakovanjeId);
        List<Pakovanje> GetAllPakovanja();
        Pakovanje CreatePakovanje(Pakovanje pakovanje);
        void UpdatePakovanje(Pakovanje pakovanje);
        void DeletePakovanje(int pakovanjeId);
        bool SaveChanges();
    }
}
