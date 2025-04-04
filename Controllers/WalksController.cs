using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;
        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository=walkRepository;
        }

        //CREATE WALKS 
        //POST:/api/waks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDtoObject)
        {
            //Map DTO TO Domain Model 
            var WalkDomainModel=_mapper.Map<Walk>(addWalkRequestDtoObject);

            //Call the repository to save the data
            await _walkRepository.CreateAsync(WalkDomainModel);

            //Return and Map Domain Model to DTO 
            return Ok(_mapper.Map<WalkDTO>(WalkDomainModel));
        }


        //GET ALL WALKS
        //GET:/api/walks?filterOn=Name&filterQuery=Track&sortBy=ColumnName&isAscending=true
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] String? filterOn, [FromQuery] String? filterQuery, [FromQuery] String? sortBy, [FromQuery] bool? isAsecending, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize=5)
        {
            //Call the repository to get all the data
            var WalksDomainModel = await _walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAsecending,PageNumber,PageSize);

            //Create an Exception 
            //throw new Exception("This is a new exception");

            //Map Domain Model TO DTO 
            return Ok(_mapper.Map<List<WalkDTO>>(WalksDomainModel));

        }


        //Get Walk by Id
        //GET:/api/walks/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id )
        {
            //Call the Repository to get the data by ID 
            var WalkkDomainModel = await _walkRepository.GetByIdAsync(id);

            //Map Domain Model to DTO
            return Ok(_mapper.Map<WalkDTO>(WalkkDomainModel));
        }


        //Update Walk by Id
        //PUT:/api/walks/{id}
        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDtoObject)
        {
            //Map DTO to Domain Model 
            var WalkDominModel = _mapper.Map<Walk>(updateWalkRequestDtoObject);

            if(WalkDominModel==null)
                return NotFound();

            //Call The Repository to Update the Data 
            WalkDominModel = await _walkRepository.UpdateAsync(id, WalkDominModel);

            //Map Domain Model to DTO
            return Ok(_mapper.Map<WalkDTO>(WalkDominModel));
        }

        //Delete Walk By Id 
        //Delete:/api/walks/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            //Check if the Walk exists 
            var WalkDomainModel=await _walkRepository.DeleteAsync(id);
            if( WalkDomainModel==null) 
                return NotFound();
            
            //Domain Model to DTO 
            return Ok(_mapper.Map<WalkDTO>(WalkDomainModel));

        }
    }
}
