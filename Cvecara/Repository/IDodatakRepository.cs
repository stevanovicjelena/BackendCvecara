using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IDodatakRepository
    {
        Dodatak GetDodatakById(int dodatakId);
        List<Dodatak> GetAllDodaci(string bojaDodatka);
        Dodatak CreateDodatak(Dodatak dodatak);
        void UpdateDodatak(Dodatak dodatak);
        void DeleteDodatak(int dodatakId);
        bool SaveChanges();
    }
}
