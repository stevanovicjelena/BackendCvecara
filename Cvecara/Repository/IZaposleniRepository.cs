using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface IZaposleniRepository
    {
        Zaposleni GetZaposleniById(int zaposleniId);
        List<Zaposleni> GetAllZaposlene();
        Zaposleni CreateZaposleni(Zaposleni zaposleni);
        void UpdateZaposleni(Zaposleni zaposleni);
        void DeleteZaposleni(int zaposleniId);
        bool SaveChanges();
    }
}
