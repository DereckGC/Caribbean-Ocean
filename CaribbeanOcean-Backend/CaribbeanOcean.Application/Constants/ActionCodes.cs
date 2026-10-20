using System;
using System.Collections.Generic;
using System.Text;

namespace CaribbeanOcean.Application.Constants
{
    public static class ActionCodes
    {
        // Authentication
        public const int Login = 101;
        public const int Logout = 102;
        public const int Register = 103;

        // Room Types
        public const int RoomTypeView = 201;
        public const int RoomTypeCreate = 202;
        public const int RoomTypeUpdate = 203;
        public const int RoomTypeDelete = 204;

        // Rooms
        public const int RoomView = 211;
        public const int RoomCreate = 212;
        public const int RoomUpdate = 213;
        public const int RoomDelete = 214;
        public const int RoomStatus = 215;

        // Gallery
        public const int GalleryView = 301;
        public const int GalleryCreate = 302;
        public const int GalleryUpdate = 303;
        public const int GalleryDelete = 304;

        // Reservations
        public const int ReservationView = 401;
        public const int ReservationCreate = 402;
        public const int ReservationUpdate = 403;
        public const int ReservationDelete = 404;

        // Status
        public const int StatusView = 501;
        public const int StatusPdf = 502;

        // Offers
        public const int OfferView = 601;
        public const int OfferCreate = 602;
        public const int OfferUpdate = 603;
        public const int OfferDelete = 604;

        // About Us
        public const int AboutView = 701;
        public const int AboutUpdate = 702;

        // Adds (Advertising)
        public const int AddView = 801;
        public const int AddCreate = 802;
        public const int AddUpdate = 803;
        public const int AddDelete = 804;

        // Facility
        public const int FacilityView = 901;
        public const int FacilityCreate = 902;
        public const int FacilityUpdate = 903;
        public const int FacilityDelete = 904;

        // Hero
        public const int HeroView = 1001;
        public const int HeroTextUpdate = 1002;
        public const int HeroImageCreate = 1003;
        public const int HeroImageUpdate = 1004;
        public const int HeroImageDelete = 1005;

        // Locations
        public const int LocationView = 1101;
        public const int LocationUpdate = 1102;

        //Seasons
        public const int SeasonView = 1201;
        public const int SeasonCreate = 1202;
        public const int SeasonUpdate = 1203;
        public const int SeasonDelete = 1204;

        // Hotel Contact
        public const int HotelContactView = 1301;
        public const int HotelContactCreate = 1302;
        public const int HotelContactUpdate = 1303;
        public const int HotelContactDelete = 1304;
    }
}
