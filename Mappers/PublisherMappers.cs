using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Dtos.Publisher;
using BookStore.Models;

namespace BookStore.Mappers
{
    public static class PublisherMappers
    {
        public static PublisherDto ToPublisherDto(this Publisher publisher) {
            return new PublisherDto {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public static Publisher ToPublisherFromCreateDto(this CreatePublisherDto publisherDto) {
            return new Publisher {
                Name = publisherDto.Name,
                Address = publisherDto.Address,
                PhoneNumber = publisherDto.PhoneNumber,
            };
        }
    }
}