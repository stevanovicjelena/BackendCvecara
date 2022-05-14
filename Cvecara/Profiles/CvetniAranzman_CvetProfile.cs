using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Profiles
{
    public class CvetniAranzman_CvetProfile : Profile
    {
        public CvetniAranzman_CvetProfile()
        {

            CreateMap<CvetniAranzman_Cvet, CvetniAranzman_Cvet>();
        }
    }
}
