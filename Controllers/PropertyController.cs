using AutoMapper;
using dashboardManger.Data;
using dashboardManger.DTOs;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace dashboardManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        //Property AddProperty(PropertyUpdateSummary propertyUpdateSummary);
        //List<PropertyDTO> ListAllProperties();
    }
}
