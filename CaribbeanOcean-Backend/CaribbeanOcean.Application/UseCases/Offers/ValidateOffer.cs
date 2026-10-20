using CaribbeanOcean.Application.DTOs.Offer;
using CaribbeanOcean.Domain.Entities;
using CaribbeanOcean.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CaribbeanOcean.Application.UseCases.Offers
{
    public class ValidateOffer
    {
        public static bool Validate(OfferAdminDTO offer)
        {
            if (string.IsNullOrWhiteSpace(offer.Name))
            {
                throw new ValidationException("Offer name cannot be empty.");
            }
            if (offer.Discount < 0 || offer.Discount > 100)
            {
                throw new ValidationException("Discount percentage must be between 0 and 100.");
            }
            if (DateTime.Parse(offer.StartDate) >= DateTime.Parse(offer.EndDate))
            {
                throw new ValidationException("Start time must be before end time.");
            }
            if (!IsAlphaNumeric(offer.Name))
            {
                throw new ValidationException("Offer name must be alphanumeric.");
            }
            if (!IsAlphaNumeric(offer.Description))
            {
                throw new ValidationException("Offer description must be alphanumeric.");
            }
            if (offer.RoomTypeId <= 0 || offer.RoomTypeId == null || string.IsNullOrWhiteSpace(offer.RoomType))
            {
                throw new ValidationException("Room type is required.");
            }
            if (offer.Discount > 100)
            {
                throw new ValidationException("The discount amount must be less than 100%..");
            }

            return true;
        }

        private static bool IsAlphaNumeric(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;

            // El patrón ^[a-zA-Z0-9]+$ significa:
            // ^ : Inicio de la cadena
            // [a-zA-Z0-9] : Solo letras (mayúsculas/minúsculas) y números
            // + : Uno o más caracteres
            // $ : Fin de la cadena
            return Regex.IsMatch(text, @"^[a-zA-Z0-9 .-]+$");
        }
    }
}
