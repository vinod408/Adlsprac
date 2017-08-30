using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
namespace Application.ValAutoMapper
{
    public class ValidationMapper<E, D>
        where E : class
        where D : class
    {
        public ValidationMapper()
        {
            Mapper.CreateMap<E, D>();
            Mapper.CreateMap<D, E>();
        }

        public D Translate(E obj)
        {
            return Mapper.Map<E, D>(obj);
        }
    }
}