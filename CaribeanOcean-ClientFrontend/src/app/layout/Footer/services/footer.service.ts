import apiClient from "../../../../shared/services/apiClient";
import type { FooterBrand, HotelContact, HotelLocation, HotelContactInfo } from "../types/footer.types";

export async function getHotelContact(): Promise<HotelContactInfo> {

    const { data: hotelContacts } = await apiClient.get<HotelContact[]>("/ContactUs");
    const responseLocation = await apiClient.get<any>("/Locations");

    const hotelLocation: HotelLocation = {
        location: responseLocation.data.description,
        url_location: responseLocation.data.images[0].url
    }

    console.log(hotelLocation)
    console.log(hotelLocation.url_location)

    const hotelContactInfo: HotelContactInfo = {
        contacts: hotelContacts,
        location: hotelLocation
    }

    return hotelContactInfo;
}

export async function getFooterBrand(): Promise<FooterBrand> {

    const data: FooterBrand = {
        title: "Caribbean Ocean",
        subtitle: "Resort & Spa",
        description: "Where the jungle meets the sea. Discover paradise on the shores of Puerto Viejo de Talamanca, Costa Rica.",
    };

    return data;
}