using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface ICvetRepository
    {
        Cvet GetCvetById(int cvetId);
        List<Cvet> GetAllCvetovi();
        Cvet CreateCvet(Cvet cvet);
        void UpdateCvet(Cvet cvet);
        void DeleteCvet(int cvetId);
        bool SaveChanges();
    }
}
