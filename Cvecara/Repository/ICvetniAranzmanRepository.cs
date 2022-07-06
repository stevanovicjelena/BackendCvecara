using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Repository
{
    public interface ICvetniAranzmanRepository
    {
        CvetniAranzman GetCvetniAranzmanById(int cvetniAranzmanId);
        List<CvetniAranzman> GetAllCvetniAranzmani(string nazivCvetnogAranzmana);
        CvetniAranzman CreateCvetniAranzman(CvetniAranzman cvetniAranzman);
        void UpdateCvetniAranzman(CvetniAranzman cvetniAranzman);
        void DeleteCvetniAranzman(int cvetniAranzmanId);
        bool SaveChanges();
    }
}
