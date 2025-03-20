using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        public RegionController(NZWalksDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        //GET: All Regions 
        //GET: https://localhost:5001/api/region
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data from database - AppDomain Models 
            var regionDomains=_dbContext.Regions.ToList();

            //Map Domain Model to DTOs
            var regionDto= new List<RegionDTO>();
            foreach(var region in regionDomains)
            {
                regionDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageURL = region.RegionImageURL
                });
            }
            return Ok(regionDto);

        }

        //GET: Region by Id
        //GET: https://localhost:5001/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            //Get Data from database - AppDomain Models 
            var regionDomain = _dbContext.Regions.FirstOrDefault(x => x.Id == id);
            //Map Domain Model to DTO
            if(regionDomain == null) {
                return NotFound();
            }
            var regionDto = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageURL = regionDomain.RegionImageURL
            };
            return Ok(regionDto);
        }

        //Post to Create a New region
        //POST: https://localhost:5001/api/region
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto AddRegionRequestsDtoObject)
        {
            //Map DTO TO Domain Model 
            var regionDomainModel = new Region()
            {
                Code = AddRegionRequestsDtoObject.Code,
                Name = AddRegionRequestsDtoObject.Name,
                RegionImageURL = AddRegionRequestsDtoObject.RegionImageURL
            };

            _dbContext.Regions.Add(regionDomainModel);
            _dbContext.SaveChanges();

            //Map Domain Model to DTO
            var RegionDTOdomainModel = new RegionDTO()
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, RegionDTOdomainModel);

        }


        //UPDATE 
        //PUT: https://localhost:5001/api/region/{id}
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto UpdateRegionRequestsDtoObject)
        {
            //Check if the region exists
            var regionDomainModel = _dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
                return NotFound();

            //Map DTO TO Domain Model 
            regionDomainModel.Code=UpdateRegionRequestsDtoObject.Code;
            regionDomainModel.Name = UpdateRegionRequestsDtoObject.Name;
            regionDomainModel.RegionImageURL = UpdateRegionRequestsDtoObject.RegionImageURL;

            _dbContext.SaveChanges();

            //Map Domain Model to DTO 
            var regionDto = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };

            return Ok(regionDto);
        }

        //DELETE
        //DELETE: https://localhost:5001/api/region/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var regionDomainModel=_dbContext.Regions.FirstOrDefault(x=>x.Id==id);

            if (regionDomainModel == null)
                return NotFound();

            _dbContext.Regions.Remove(regionDomainModel);
            _dbContext.SaveChanges();

            //Domain Model to DTO 
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };
            return Ok(regionDTO);
        }
    }
}
