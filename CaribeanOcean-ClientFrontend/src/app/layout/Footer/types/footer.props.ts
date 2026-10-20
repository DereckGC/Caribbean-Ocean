import type { FooterBrand, HotelContact, HotelLocation} from "./footer.types";

export interface FooterBrandProps{
    footerBrand : FooterBrand;
    socialMedia: HotelContact[];
}

export interface FooterContactProps{
    phoneNumbers : HotelContact[];
    emails : HotelContact[];
}

export interface FooterSocialIconsProps{
    socialMedia: HotelContact[];
}

export interface FooterLocationProps{
    location : HotelLocation;
}