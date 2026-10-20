export interface FooterBrand {
    title: string;
    subtitle: string;
    description: string;
}

export interface HotelContact{
    type: string;
    contact: string;
}

export interface HotelLocation {
    location: string;
    url_location : string;
}

export interface HotelContactInfo {
    contacts: HotelContact[];
    location: HotelLocation;
}