﻿using System.Net.WebSockets;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilter;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        public RegionController(NZWalksDbContext dbcontext, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = dbcontext;
            _regionRepository=regionRepository;
            _mapper = mapper;
        }

        //GET: All Regions 
        //GET: https://localhost:5001/api/region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data from database - AppDomain Models 
            var regionDomains=await _regionRepository.GetAllAsync();

            //Map Domain Model to DTOs
            //var regionDto= new List<RegionDTO>();
            //foreach(var region in regionDomains)
            //{
            //    regionDto.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Code = region.Code,
            //        Name = region.Name,
            //        RegionImageURL = region.RegionImageURL
            //    });
            //}
            var regionDto= _mapper.Map<List<RegionDTO>>(regionDomains);
            return Ok(regionDto);

        }

        //GET: Region by Id
        //GET: https://localhost:5001/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            //Get Data from database - AppDomain Models 
            var regionDomain =await _regionRepository.GetByIdAsync(id);
            //Map Domain Model to DTO
            if(regionDomain == null) {
                return NotFound();
            }
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageURL = regionDomain.RegionImageURL
            //};
            var regionDto=_mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDto);
        }

        //Post to Create a New region
        //POST: https://localhost:5001/api/region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto AddRegionRequestsDtoObject)
        {
            if (ModelState.IsValid)
            {
                //Map DTO TO Domain Model 
                var regionDomainModel = _mapper.Map<Region>(AddRegionRequestsDtoObject);

                //var regionDomainModel = new Region()
                //{
                //    Code = AddRegionRequestsDtoObject.Code,
                //    Name = AddRegionRequestsDtoObject.Name,
                //    RegionImageURL = AddRegionRequestsDtoObject.RegionImageURL
                //};

                regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

                //Map Domain Model to DTO
                var RegionDTOdomainModel = _mapper.Map<RegionDTO>(regionDomainModel);

                //var RegionDTOdomainModel = new RegionDTO()
                //{
                //    Id = regionDomainModel.Id,
                //    Code = regionDomainModel.Code,
                //    Name = regionDomainModel.Name,
                //    RegionImageURL = regionDomainModel.RegionImageURL
                //};
                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, RegionDTOdomainModel);
            }
            return BadRequest(ModelState);

        }


        //UPDATE 
        //PUT: https://localhost:5001/api/region/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto UpdateRegionRequestsDtoObject)
        {

            //Map DTO to Domain Model 
            var regionDomainModel = _mapper.Map<Region>(UpdateRegionRequestsDtoObject);
            //var regionDomainModel = new Region
            //{
            //    Code = UpdateRegionRequestsDtoObject.Code,
            //    Name = UpdateRegionRequestsDtoObject.Name,
            //    RegionImageURL = UpdateRegionRequestsDtoObject.RegionImageURL
            //};
            //Check if the region exists
            regionDomainModel=await _regionRepository.UpdateAsync(id,regionDomainModel);


            if (regionDomainModel == null)
                return NotFound();

            //Map Domain Model to DTO 
            var regionDto=_mapper.Map<RegionDTO>(regionDomainModel);
            //var regionDto = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};

            return Ok(regionDto);
        }

        //DELETE
        //DELETE: https://localhost:5001/api/region/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
                return NotFound();

            //Domain Model to DTO 
            var regionDTO= _mapper.Map<RegionDTO>(regionDomainModel);

            //var regionDTO = new RegionDTO
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL
            //};

            return Ok(regionDTO);
        }
    }
}
