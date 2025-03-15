using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Publisher;
using BookStore.Interfaces;
using BookStore.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepo;

        public PublisherController(IPublisherRepository publisherRepo)
        {
            _publisherRepo = publisherRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var publishers = await _publisherRepo.GetAllAsync();
            var publishersDto = publishers.Select(p => p.ToPublisherDto());
            return Ok(publishersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var publisher = await _publisherRepo.GetByIdAsync(id);

            if(publisher == null)
                return NotFound();

            return Ok(publisher.ToPublisherDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisherDto publisherDto) {
            var publisherModel = publisherDto.ToPublisherFromCreateDto();
            var publisher = await _publisherRepo.CreateAsync(publisherModel);

            return CreatedAtAction(nameof(GetById), new {id = publisher.Id}, publisher.ToPublisherDto());
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePublisherDto publisherDto) {
            var publisher = await _publisherRepo.UpdateAsync(id, publisherDto);

            if(publisher == null)
                return NotFound();

            return Ok(publisher.ToPublisherDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            var publisher = await _publisherRepo.DeleteAsync(id);

            if(publisher == null)
                return NotFound();

            return NoContent();
        }
    }
}