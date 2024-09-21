using dashboardManger.Data;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using System.Collections.Generic;
using System.Linq;
using dashboardManger.DTOs;
using System.Security.Claims;
using AutoMapper;
namespace dashboardManger.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
    }
}
