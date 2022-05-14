using AutoMapper;
using Cvecara.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Profiles
{
    public class Porudzbina_DodatakProfile : Profile
    {
        public Porudzbina_DodatakProfile()
        {

            CreateMap<Porudzbina_Dodatak, Porudzbina_Dodatak>();

        }
    }
}
