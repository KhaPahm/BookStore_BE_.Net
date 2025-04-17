using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Dtos.Publisher;
using BookStore.Interfaces;
using BookStore.Interfaces.Services;
using BookStore.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.V1
{
    [Route("api/v1/publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var publishersDto = await _publisherService.GetAllPublisherDtoAsync();
            return Ok(publishersDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            var publisherDto = await _publisherService.GetPublisherDtoByIdAsync(id);
            return Ok(publisherDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisherDto createPublisherDto) {
            var publisherDto = await _publisherService.CreatePublisherAsync(createPublisherDto);

            return CreatedAtAction(nameof(GetById), new { id = publisherDto.Id }, publisherDto);
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePublisherDto updatePublisherDto) {
            var publisherDto = await _publisherService.UpdatePublisherAsync(id, updatePublisherDto);

            return Ok(publisherDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {
            await _publisherService.DeletePublisherAsync(id);
            
            return NoContent();
        }
    }
}