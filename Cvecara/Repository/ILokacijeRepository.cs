using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface ILokacijeRepository
    {
        Lokacije GetLokacijeById(int lokacijeId);
        List<Lokacije> GetAllLokacije(string nazivLokacije);
        Lokacije CreateLokacije(Lokacije lokacije);
        void UpdateLokacije(Lokacije lokacije);
        void DeleteLokacije(int lokacijeId);
        bool SaveChanges();
    }
}
