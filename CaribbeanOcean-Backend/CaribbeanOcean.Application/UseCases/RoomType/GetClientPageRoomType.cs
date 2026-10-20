using System;
using System.Collections.Generic;
using System.Text;
using CaribbeanOcean.Application.DTOs;
using CaribbeanOcean.Application.DTOs.Facility;
using CaribbeanOcean.Application.Mappers;
using CaribbeanOcean.Domain.Exceptions;
using CaribbeanOcean.Domain.Interfaces;

namespace CaribbeanOcean.Application.UseCases.RoomType
{
    public class GetClientPageRoomType(IRoomTypeRepository roomTypeRepository, IPageContentRepository pageContentRepository)
    {
        public async Task<RoomTypePageDTO> ExecuteAsync()
        {
            var room_types = await roomTypeRepository.getAllRoomTypes();

            if (room_types == null)
            {
                throw new NotFoundException("Room Types content not found.");
            }
            var pageContent = await pageContentRepository.GetPageContentByTitle("room_type");

            if (pageContent == null)
            {
                throw new NotFoundException("Room Type Page cliente content not found.");
            }
            var page = RoomTypeMapper.ToPageDTO(pageContent, room_types);
            return page;

        }
    }
}
