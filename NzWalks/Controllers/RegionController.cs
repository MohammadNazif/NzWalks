using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NzWalks.Models.DomainModel;
using NzWalks.Models.DTOs;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly DatabaseContext _db;
        private readonly IRegionRepository _repoRegion;

        public RegionController(DatabaseContext db,IRegionRepository repoRegion)
        {
            _db = db;
            _repoRegion = repoRegion;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regionsdomain = await _repoRegion.GetAllAsync();
            //convert domain to dto that is sent to client
            var regionsDto = regionsdomain.Select(x => new regionDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                RegionImageUrl = x.RegionImageUrl
            }).ToList();

            return Ok(regionsDto);
        }



        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionsdomain = await  _repoRegion.GetByIdAsync(id);
            if (regionsdomain == null)
            {
                return NotFound();
            }

            //convert domain to dto that is sent to client
            var regionDto = new regionDto
            {
                Id = regionsdomain.Id,
                Name = regionsdomain.Name,
                Code = regionsdomain.Code,
                RegionImageUrl = regionsdomain.RegionImageUrl
            };
            return Ok(regionDto);

        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _repoRegion.DeleteAsync(id);
            await _db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] addRequestRegionDto requetsRegionDto)
        {
            //convert dto to domain for saving to database
            var requetsRegionDomain = new Region
            {
                Name = requetsRegionDto.Name,
                Code = requetsRegionDto.Code,
                RegionImageUrl = requetsRegionDto.RegionImageUrl
            };
            await _repoRegion.CreateAsync(requetsRegionDomain);
           
            //convert domain to dto bcsz response sent to client is always dto
            var RegionDto = new regionDto
            {
                Id = requetsRegionDomain.Id,
                Name = requetsRegionDomain.Name,
                Code = requetsRegionDomain.Code,
                RegionImageUrl = requetsRegionDomain.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById),new {id = RegionDto.Id }, RegionDto);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] updateRequestRegionDto updateDto)
        {
            //Convert to Domain
            var UpdateDomain = new Region
            {
                Name = updateDto.Name,
                Code = updateDto.Code,
                RegionImageUrl = updateDto.RegionImageUrl
            };

             UpdateDomain = await _repoRegion.UpdateAsync(id, UpdateDomain);
           
            //convert domain to dto that is sent to client
            var updateRegionDto = new regionDto
            {
                Id = UpdateDomain.Id,
                Name = UpdateDomain.Name,
                Code = UpdateDomain.Code,
                RegionImageUrl = UpdateDomain.RegionImageUrl
            };
            return Ok(updateRegionDto);

        }
    }
}
