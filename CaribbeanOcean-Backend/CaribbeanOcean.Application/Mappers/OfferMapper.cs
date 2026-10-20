using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Mappers
{
    public class OfferMapper
    {
        public static OfferDTO ToDTO(Offer entity)
        {
            if (entity == null) return null!;
            return new OfferDTO
            {
                Id = entity.id,
                Name = entity.name.Trim(),
                Description = entity.description?.Trim(),
                Discount = entity.discount_percentage,
                StartDate = entity.start_time.ToString("MM/dd/yyyy"),
                EndDate = entity.end_time.ToString("MM/dd/yyyy"),
                RoomType = entity.room_type,
                UrlImage = entity.url_room_type_image,
                RoomTypeId = entity.room_type_id
            };
        }

        public static Offer ToEntity(OfferDTO entity)
        {
            if (entity == null) return null!;
            return new Offer
            {

                name = entity.Name.Trim(),
                description = entity.Description?.Trim(),
                discount_percentage = entity.Discount,
                start_time = DateTime.Parse(entity.StartDate),
                end_time = DateTime.Parse(entity.EndDate),
                room_type = entity.RoomType,
                url_room_type_image = entity.UrlImage
            };
        }

        public static OfferAdminDTO ToAdminDTO(Offer entity)
        {
            if (entity == null) return null!;
            return new OfferAdminDTO
            {
                Id = entity.id,
                Name = entity.name.Trim(),
                Description = entity.description?.Trim(),
                Discount = entity.discount_percentage,
                StartDate = entity.start_time.ToString("MM/dd/yyyy"),
                EndDate = entity.end_time.ToString("MM/dd/yyyy"),
                RoomTypeId = entity.room_type_id,
                RoomType = entity.room_type?.Trim(),
                UpdatedBy = entity.updated_by
            };
        }

        public static Offer ToEntity(OfferAdminDTO entity)
        {
            if (entity == null) return null!;
            return new Offer
            {
                id = entity.Id,
                name = entity.Name?.Trim() ?? string.Empty,
                description = entity.Description?.Trim() ?? string.Empty,
                discount_percentage = entity.Discount,
                start_time = DateTime.Parse(entity.StartDate ?? DateTime.Now.ToString("MM/dd/yyyy")),
                end_time = DateTime.Parse(entity.EndDate ?? DateTime.Now.ToString("MM/dd/yyyy")),
                room_type_id = entity.RoomTypeId,
                room_type = entity.RoomType?.Trim() ?? string.Empty,
                updated_by = entity.UpdatedBy
            };
        }
    }
}