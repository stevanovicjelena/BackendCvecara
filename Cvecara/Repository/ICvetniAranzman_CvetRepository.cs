using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface ICvetniAranzman_CvetRepository
    {
        CvetniAranzman_Cvet GetCvetniAranzman_CvetById(int cvetniAranzman_CvetId);
        List<CvetniAranzman_Cvet> GetAllCvetniAranzman_Cvet();
        CvetniAranzman_Cvet CreateCvetniAranzman_Cvet(CvetniAranzman_Cvet cvetniAranzman_Cvet);
        void UpdateCvetniAranzman_Cvet(CvetniAranzman_Cvet cvetniAranzman_Cvet);
        void DeleteCvetniAranzman_Cvet(int cvetniAranzman_CvetId);
        bool SaveChanges();
    }
}
