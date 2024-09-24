using AutoMapper;
using dashboardManger.Data;
using dashboardManger.DTOs;
using dashboardManger.Interfaces;
using dashboardManger.Models;
using dashboardManger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static dashboardManger.DTOs.PropertyDTO;

namespace dashboardManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(MyDbContext context, IPropertyService propertyService, IMapper mapper)
        {
            _context = context;
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RealEstateProperty>> GetAllProperties()
        {
            return Ok(_propertyService.GetAllProperties());
        }


        [HttpPost("listAll")]
        public ActionResult<List<PropertyDTOSummary>> ListAll([FromBody] PropertySearchSummary searchSummary, [FromQuery] int pageNum = 1, int pageSize = 10)
        {
            try
            {
                var dataList = _context.Property
                    .Include(l => l.ListingAgent1)
                    .Include(l => l.ListingAgent2)
                    .AsQueryable()
                    .OrderBy(l => l.DateTime);

                var totalItems = dataList.Count();

                //map data
                var dtoList = GetDtoDataList(dataList);


                //filter data
                if (searchSummary != null)
                {
                    if (searchSummary.StatusId > 0)
                    {
                        dtoList.RemoveAll(l => l.StatusId != searchSummary.StatusId);

                        totalItems = dtoList.Count();
                    }

                    if (searchSummary.TypeId > 0) { 
                        dtoList.RemoveAll(l => l.TypeId != searchSummary.TypeId);

                        totalItems = dtoList.Count();                        
                    }

                    if (!string.IsNullOrEmpty(searchSummary.FilterWord))
                    {
                        var filterWordList = searchSummary.FilterWord.ToLower().Split(' ');
                        foreach (var word in filterWordList)
                        {
                            dtoList.RemoveAll(l => !l.FilterWord.Contains(word));
                        }

                        totalItems = dtoList.Count();
                    }
                }

                var result = dtoList.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

                // Create pagination information
                var pageInfo = new PageInfo
                {
                    PageNum = pageNum,
                    PageSize = pageSize,
                    Total = totalItems
                };

                var response = new PagedApiResponse<PropertyDTOSummary>(200, "Success", pageInfo, result);

                return Ok(response);


            }
            catch (Exception ex)
            {
                //return error message
                return StatusCode(500, new ApiResponse<string>(500, "An error occurred while processing your request", null));
            }
        }


        public static List<PropertyDTOSummary> GetDtoDataList(IOrderedQueryable<RealEstateProperty> dataList)
        {
            var result = new List<PropertyDTOSummary>();
            if (dataList != null && dataList.Count() > 0)
            {
                foreach (var data in dataList)
                {
                    var dtoData = new PropertyDTOSummary(data);

                    //status
                    var statusId = data.StatusId;
                    if (statusId > 0) dtoData.Status = Enum.GetName(typeof(ProperyStatus), statusId);

                    //type
                    var typeId = data.TypeId;
                    if (typeId > 0) dtoData.Type = Enum.GetName(typeof(PropertyType), typeId);

                    //listing agent
                    var listingAgent1Id = data.ListingAgent1Id;
                    if (listingAgent1Id > 0 && data.ListingAgent1 != null)
                    {
                        dtoData.ListingAgent1Guid = data.ListingAgent1.Guid.ToString();
                        dtoData.ListingAgent1Name = $"{data.ListingAgent1.FirstName} {data.ListingAgent1.LastName}";
                    }
                    var listingAgent2Id = data.ListingAgent2Id;
                    if (listingAgent2Id > 0 && data.ListingAgent2 != null)
                    {
                        dtoData.ListingAgent2Guid = data.ListingAgent2.Guid.ToString();
                        dtoData.ListingAgent2Name = $"{data.ListingAgent2.FirstName} {data.ListingAgent2.LastName}";
                    }

                    dtoData.ListingAgentNameDisplay = !string.IsNullOrEmpty(dtoData.ListingAgent1Name) ? dtoData.ListingAgent1Name  : string.Empty;

                    if (!string.IsNullOrEmpty(dtoData.ListingAgent2Name))
                    {
                        if (!string.IsNullOrEmpty(dtoData.ListingAgentNameDisplay))
                        {
                            //add line break
                            dtoData.ListingAgentNameDisplay += "\n";
                        }
                        dtoData.ListingAgentNameDisplay += dtoData.ListingAgent2Name;
                    }

                    var filterWord = $"{data.Address} {dtoData.Status} {dtoData.Type} {dtoData.ListingAgentNameDisplay}";

                    dtoData.FilterWord = filterWord.ToLower();

                    result.Add(dtoData);
                }
            }

            return result;
        }

    }
}
