using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.CustomActionFilter;
using NzWalks.Models.DomainModel;
using NzWalks.Models.DTOs;
using NzWalks.Repositories;

namespace NzWalks.Controllers
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
            _walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkDto addWalkDto)
        {
            
                //Map Dto to domain
                var walkDomain = _mapper.Map<Walk>(addWalkDto);
                await _walkRepository.CreateAsync(walkDomain);
                //Map domain to dto 
                return Ok(_mapper.Map<WalkDto>(walkDomain));
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walksDomain = await _walkRepository.GetAllAsync();
            return Ok(_mapper.Map<List<WalkDto>>(walksDomain));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var dataDomain = await _walkRepository.GetByIdAsync(id);
            if (dataDomain == null)
            {

                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(dataDomain));
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, updatewalkDto updateDto)
        {
            //map to domain
                var walkupdate = _mapper.Map<Walk>(updateDto);
                walkupdate = await _walkRepository.UpdateAsync(id, walkupdate);
                if (walkupdate == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<WalkDto>(walkupdate));

        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteWalkDomain = await _walkRepository.DeleteAsync(id);
            if (deleteWalkDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(deleteWalkDomain));
        }
    }
}
