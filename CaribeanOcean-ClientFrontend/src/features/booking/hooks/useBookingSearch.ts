import { useState, useEffect } from "react";
import { type RoomType, type BookingFilters } from "../types/booking.types";
import { useRoomTypes } from "./useRoomTypes";

export const useBookingSearch = () => {
  const [filter, setFilters] = useState<BookingFilters>({
    startDate: "",
    endDate: "",
    roomType: "",
  });

  const [hasSearched, setHasSearched] = useState(false);
  const [availableRooms, setAvailableRooms] = useState<RoomType[]>([]);
  const [isRecommendation, setIsRecommendation] = useState(false);
  const [localError, setLocalError] = useState<string | null>(null);

  // NEW: State for the Select Dropdown
  const [roomTypeCatalog, setRoomTypeCatalog] = useState<RoomType[]>([]);

  // Consume our Data Hook
  const { searchAvailableRooms, isSearching, fetchRoomTypeCatalog, isFetchingCatalog } = useRoomTypes();

  // NEW: Fetch the catalog on mount
  useEffect(() => {
    const loadCatalog = async () => {
      const { data, hasError } = await fetchRoomTypeCatalog();
      if (!hasError && data) {
        setRoomTypeCatalog(data);
      }
    };
    loadCatalog();
  }, []); // Empty array ensures this only runs once when the page loads!

  const updateFilter = (field: keyof BookingFilters, value: string) => {
    setFilters((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    setHasSearched(true);
    setIsRecommendation(false);
    setLocalError(null);

    if (new Date(filter.startDate) >= new Date(filter.endDate)) {
      setLocalError("Oops! It looks like your check-out date is before your arrival. Please adjust your dates so we can find your perfect room.");
      setAvailableRooms([]);
      return;
    }

    const { data: results, hasError, errorMessage } = await searchAvailableRooms(filter);

    if (hasError) {
      setLocalError(errorMessage || "We couldn't connect to our servers. Please try again.");
      return;
    }

    if (!results || results.length === 0) {
      setIsRecommendation(true);
      // We now use the REAL catalog as the recommendations instead of the mock!
      setAvailableRooms(roomTypeCatalog); 
    } else {
      setAvailableRooms(results);
    }
  };

  const resetSearch = () => {
    setHasSearched(false);
    setAvailableRooms([]);
    setIsRecommendation(false);
    setLocalError(null);
    setFilters({ startDate: "", endDate: "", roomType: "" });
  };

  return {
    state: { 
      filter, 
      hasSearched, 
      availableRooms, 
      isRecommendation, 
      error: localError, 
      isLoading: isSearching,
      roomTypeCatalog, // Export this so your UI can map over it for the <select> options!
      isFetchingCatalog
    },
    actions: { updateFilter, handleSearch, resetSearch }
  };
};
