using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IPorudzbinaRepository
    {
        Porudzbina GetPorudzbinaById(int porudzbinaId);
        List<Porudzbina> GetAllPorudzbine();
        Porudzbina CreatePorudzbina(Porudzbina porudzbina);
        void UpdatePorudzbina(Porudzbina porudzbina);
        void DeletePorudzbina(int porudzbinaId);
        bool SaveChanges();
    }
}
