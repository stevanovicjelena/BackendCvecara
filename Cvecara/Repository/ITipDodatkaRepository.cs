using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface ITipDodatkaRepository
    {
        TipDodatka GetTipDodatkaById(int tipDodatkaId);
        List<TipDodatka> GetAllTipoveDodataka(string nazivTipa);
        TipDodatka CreateTipDodatka(TipDodatka tipDodatka);
        void UpdateTipDodatka(TipDodatka tipDodatka);
        void DeleteTipDodatka(int tipDodatkaId);
        bool SaveChanges();
    }

}
