
import styles from "./Booking.module.css";
import { BookingSearchForm } from "./BookingSearchForm";
import { CheckoutForm } from "./CheckoutForm";
import { BookingSuccess } from "./BookingSuccess";
import { useBookingSearch } from "../hooks/useBookingSearch";
import { useCheckout } from "../hooks/useCheckout";
import { BookingSearchResults } from "./BookingSearchResults";
import { Spinner } from "../../../shared/components/Spinner/Spinner";
import { useSearchParams } from "react-router-dom";
import { useEffect } from "react";

export const Booking = () => {
  const search = useBookingSearch();
  const checkout = useCheckout();
  const [searchParams] = useSearchParams();

  const offerIdParam = searchParams.get("id"); // or "offerId" if you rename it
  const roomTypeIdParam = searchParams.get("roomTypeId");
  const applyOffersParam = searchParams.get("applyOffers");;

  const offerContext = {
    applyOffers: applyOffersParam === "true" && !!offerIdParam,
    selectedOfferId: offerIdParam ? Number(offerIdParam) : null,
    roomTypeId: roomTypeIdParam ? Number(roomTypeIdParam) : null,
  };

  useEffect(() => {
    if (offerContext.roomTypeId) {
      search.actions.updateFilter("roomType", offerContext.roomTypeId.toString())

    }
  }, [offerContext.roomTypeId])



  const handleFullReset = () => {
    checkout.actions.resetCheckout();
    search.actions.resetSearch();
  };

  // --- 1. RENDER DE ÉXITO (Paso 4) ---
  if (checkout.state.bookingComplete) {
    return (
      <BookingSuccess
        guestName={checkout.state.guestData.guestName}
        guestLastName={checkout.state.guestData.guestLastName}
        roomName={checkout.state.selectedRoom?.name || ""}
        reservationCode={checkout.state.reservationCode}
        onClose={handleFullReset}
      />
    );
  }
  // --- 2. RENDER DE CHECKOUT DE DATOS (Paso 3) ---
  if (checkout.state.selectedRoom) {
    return (
      <CheckoutForm
        isPriceLoading={checkout.state.isPriceLoading}
        priceError={checkout.state.priceError}
        pricePreview={checkout.state.pricePreview}
        selectedRoom={checkout.state.selectedRoom}
        guestData={checkout.state.guestData}
        updateGuestField={checkout.actions.updateGuestField}
        onAccept={(e) => checkout.actions.handleAcceptBooking(e, search.state.filter.startDate, search.state.filter.endDate, offerContext.applyOffers, offerContext.selectedOfferId)}
        onCancel={checkout.actions.handleCancelBooking}
      />
    );
  }
  // --- 3. RENDER DE BUSCADOR Y RESULTADOS (Paso 1 y 2) ---
  return (

    <div className={styles.pageContainer}>

      <BookingSearchForm
        filters={search.state.filter}
        updateFilter={search.actions.updateFilter}
        onSearch={search.actions.handleSearch}
        hasSearched={search.state.hasSearched}
        error={search.state.error}
        isLoading={search.state.isLoading}
        roomTypeCatalog={search.state.roomTypeCatalog}
        hasActiveOffer={offerContext.applyOffers}
      />
      {/* Si ESTÁ cargando, mostramos Spinner */}
      {search.state.isLoading && (
        <Spinner message="Searching for the perfect room..." />
      )}

      {search.state.hasSearched && !search.state.error && !search.state.isLoading && (
        <BookingSearchResults
          hasSearched={search.state.hasSearched}
          isRecommendation={search.state.isRecommendation}
          availableRooms={search.state.availableRooms}
          handleSelectRoom={(room) => checkout.actions.handleSelectRoom(room, search.state.filter.startDate, search.state.filter.endDate, offerContext.applyOffers, offerContext.selectedOfferId)}
        />
      )}
    </div>
  );
};
