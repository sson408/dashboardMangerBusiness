﻿using dashboardManger.Data;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using System.Collections.Generic;
using System.Linq;
using dashboardManger.DTOs;
using System.Security.Claims;
using AutoMapper;
using static dashboardManger.DTOs.PropertyDTO;

namespace dashboardManger.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public PropertyService(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public RealEstateProperty GetPropertyByGuid(string guid)
        {
            var property = _context.Property.SingleOrDefault(p => p.GUID.ToString() == guid);
            if (property == null)
            {
                return null;
            }

            return property;
        }

        public IEnumerable<RealEstateProperty> GetAllProperties()
        {
            return _context.Property.ToList();
        }

    }
}
