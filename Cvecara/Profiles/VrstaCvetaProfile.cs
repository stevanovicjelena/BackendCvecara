using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Profiles
{
    public class VrstaCvetaProfile : Profile
    {
        public VrstaCvetaProfile()
        {

            CreateMap<VrstaCveta, VrstaCveta>();

        }
    }
}
