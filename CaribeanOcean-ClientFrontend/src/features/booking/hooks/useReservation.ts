import { useAsyncState } from "../../../shared/hooks/useLoading";
import { bookingService } from "../services/booking.service";
import type { BookingRequestDto } from "../types/booking.types";

export const useReservations = () => {
  const createReservationState = useAsyncState();
  const checkRoomState = useAsyncState();

  const createReservation = async (data: BookingRequestDto) => {
    return await createReservationState.withAsync(() => bookingService.createReservation(data));
  };

  const hasAvailableRoom = async (startDate: string, endDate: string, roomTypeId: number) => {
    return await checkRoomState.withAsync(() => bookingService.hasAvailableRoom(startDate, endDate, roomTypeId));
  };

  return {
    create: {
      execute: createReservation,
      isLoading: createReservationState.isLoading,
      error: createReservationState.error,
    },
    hasAvailableRoom: {
      execute: hasAvailableRoom,
      isLoading: checkRoomState.isLoading,
      error: checkRoomState.error,
    }
  };
};
