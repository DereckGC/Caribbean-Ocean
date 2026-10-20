import { useState } from "react";
import { type RoomType, type GuestData, type BookingRequestDto, type ReservationPriceResult } from "../types/booking.types";
import { useReservations } from "./useReservation";
import { bookingService } from "../services/booking.service";

export const useCheckout = () => {
  const [selectedRoom, setSelectedRoom] = useState<RoomType | null>(null);
  const [guestData, setGuestData] = useState<GuestData>({
    guestName: "", guestLastName: "", guestEmail: "", creditCard: "",
  });

  const [bookingComplete, setBookingComplete] = useState(false);
  const [reservationCode, setReservationCode] = useState("");
  const [localError, setLocalError] = useState<string | null>(null);


  const [pricePreview, setPricePreview] = useState<ReservationPriceResult | null>(null);
  const [isPriceLoading, setIsPriceLoading] = useState(false);
  const [priceError, setPriceError] = useState<string | null>(null);


  // 1. Consume our new Data Hook
  const { create } = useReservations();

  const handleSelectRoom = async (
    room: RoomType,
    startDate: string,
    endDate: string,
    applyOffers = false,
    selectedOfferId: number | null = null

  ) => {
    setSelectedRoom(room);
    setPricePreview(null);
    setPriceError(null);

    try {
      setIsPriceLoading(true);
      console.log("room", room)
      console.log("startDate", startDate)
      console.log("endDate", endDate)
      console.log("applyOffers", applyOffers)
      console.log("selectedOfferId", selectedOfferId)
      
      const preview = await bookingService.previewReservationPrice({
        roomTypeId: room.id,
        checkIn: startDate,
        checkOut: endDate,
        applyOffers,
        selectedOfferId
      });

      setPricePreview(preview);
    } catch {
      setPriceError("Could not calculate reservation price.");
    } finally {
      setIsPriceLoading(false);
    }
  };

  const updateGuestField = (field: keyof GuestData, value: string) => {
    setGuestData((prev) => ({ ...prev, [field]: value }));
  };

  const handleCancelBooking = () => {
    setSelectedRoom(null);
    setLocalError(null);
  };

  const handleAcceptBooking = async (e: React.FormEvent, startDate: string, endDate: string, applyOffers = false, selectedOfferId:number | null=null) => {
    e.preventDefault();
    setLocalError(null);

    if (!selectedRoom) return;

    // Map UI State to Backend DTO Structure
    const reservationPayload: BookingRequestDto = {
      RoomTypeId: selectedRoom.id,
      ClientName: guestData.guestName,
      ClientLastname: guestData.guestLastName,
      email: guestData.guestEmail,
      CardNumber: guestData.creditCard,
      CheckIn: startDate,
      CheckOut: endDate,
      applyOffers,
      selectedOfferId
    };

    // 2. Execute Data Hook
    const { data, hasError, errorMessage } = await create.execute(reservationPayload);

    // 3. Handle Local Errors
    if (hasError) {
      setLocalError(errorMessage || "Failed to complete reservation. Please try again.");
      return;
    }

    if (data) {
      setReservationCode(data.confirmationCode);
      setBookingComplete(true);
    }
  };

  const resetCheckout = () => {
    setBookingComplete(false);
    setSelectedRoom(null);
    setLocalError(null);
    setGuestData({ guestName: "", guestLastName: "", guestEmail: "", creditCard: "" });
  };

  return {
    state: {
      selectedRoom,
      guestData,
      bookingComplete,
      reservationCode,
      error: localError,
      isLoading: create.isLoading, // You can pass this to your CheckoutForm Submit button!
      isPriceLoading,
      pricePreview,
      priceError,
    },
    actions: { handleSelectRoom, handleCancelBooking, handleAcceptBooking, resetCheckout, updateGuestField }
  };
};
