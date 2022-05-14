using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IPorudzbina_DodatakRepository
    {
        Porudzbina_Dodatak GetPorudzbina_DodatakById(int porudzbina_DodatakId);
        List<Porudzbina_Dodatak> GetAllPorudzbina_Dodatak();
        Porudzbina_Dodatak CreatePorudzbina_Dodatak(Porudzbina_Dodatak porudzbina_Dodatak);
        void UpdatePorudzbina_Dodatak(Porudzbina_Dodatak porudzbina_Dodatak);
        void DeletePorudzbina_Dodatak(int porudzbina_DodatakId);
        bool SaveChanges();
    }
}
