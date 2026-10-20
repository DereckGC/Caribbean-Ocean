import { useAsyncState } from "../../../shared/hooks/useLoading";
import { bookingService } from "../services/booking.service";
import type { BookingFilters } from "../types/booking.types";

export const useRoomTypes = () => {
  const searchState = useAsyncState();
  const catalogState = useAsyncState(); // New state isolated for fetching the catalog

  const searchAvailableRooms = async (filters: BookingFilters) => {
    return await searchState.withAsync(() => bookingService.searchAvailableRooms(filters));
  };

  const fetchRoomTypeCatalog = async () => {
    return await catalogState.withAsync(() => bookingService.getAllRoomTypes());
  };

  return {
    searchAvailableRooms,
    isSearching: searchState.isLoading,
    searchError: searchState.error,

    fetchRoomTypeCatalog,
    isFetchingCatalog: catalogState.isLoading,
    catalogError: catalogState.error,
  };
};
