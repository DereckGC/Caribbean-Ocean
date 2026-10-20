import apiClient from "../../../shared/services/apiClient";
import type { BookingFilters, RoomType, BookingRequestDto, BookingResponseDto, CalculateReservationPriceRequest, ReservationPriceResult } from "../types/booking.types";

export const bookingService = {
  // 1. Search Availability
  searchAvailableRooms: async (filters: BookingFilters): Promise<RoomType[]> => {

    // A. Specific Room Type selected (Call POST by ID)
    if (filters.roomType) {
      const payload = {
        checkIn: filters.startDate,
        checkOut: filters.endDate,
        roomTypeId: parseInt(filters.roomType)
      };


      const response = await apiClient.post<RoomType>(`/RoomType/Available/${filters.roomType}`, payload);


      return [response.data];
    }

    // B. "Any" Room Type selected (Call POST for all available)
    const payload = {
      checkIn: filters.startDate,
      checkOut: filters.endDate,
      roomTypeId: null
    };

    const response = await apiClient.post<RoomType[]>("/RoomType/AvailableCount", payload);
    return response.data;
  },

  // 2. Submit Reservation mapping to [HttpPost] Create
  createReservation: async (data: BookingRequestDto): Promise<BookingResponseDto> => {
    // We send the data. Assuming the backend returns { code: "ABC" }
    const response = await apiClient.post<{ code: string }>("/Reservation", data);

    return {
      confirmationCode: response.data.code,
      status: "CONFIRMED"
    };
  },

  // 3. Has Available Room mapping to [HttpPost("HasAvailableRoom")]
  hasAvailableRoom: async (startDate: string, endDate: string, roomTypeId: number): Promise<boolean> => {
    const payload = { checkIn: startDate, checkOut: endDate, roomTypeId };
    const response = await apiClient.post<{ hasAvailableRoom: boolean }>("/Reservation/HasAvailableRoom", payload);

    return response.data.hasAvailableRoom;
  },

  getAllRoomTypes: async (): Promise<RoomType[]> => {
    const response = await apiClient.get<RoomType[]>("/RoomType");
    return response.data;
  },

  /**PRICING METHODS */
  // 2. Preview reservation price
  previewReservationPrice: async (
    data: CalculateReservationPriceRequest): Promise<ReservationPriceResult> => {
    const response = await apiClient.post<ReservationPriceResult>(
      "/Reservation/price-preview",
      data
    );

    return response.data;
  },
};
