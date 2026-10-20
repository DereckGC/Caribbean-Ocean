export interface Booking {
  id: string;
  name: string;
}

export const MOCK_ROOM_TYPES = [
  {
    id: 1, // Ahora es numérico para alinear con tu DB (Int)
    type: "deluxe",
    name: "Deluxe Ocean View",              // Ahora es name
    description: "Despierta con el sonido de las olas...",
    daily_rate: 180,                        // Ahora es daily_rate
    image_url: "",                          // Ahora es image_url
    bookedDates: [                          // Simulando fechas donde TODOS los de este tipo están llenos
      { start: "2026-04-10", end: "2026-04-15" }
    ]
  },
  {
    id: 2,
    type: "suite",
    name: "Presidential Suite",
    description: "Nuestra suite más lujosa...",
    daily_rate: 350,
    image_url: "",
    bookedDates: [
      { start: "2026-04-12", end: "2026-04-20" }
    ]
  },
  {
    id: 3,
    type: "standard",
    name: "Standard Garden Queen",
    description: "Una habitación acogedora...",
    daily_rate: 120,
    image_url: "",
    bookedDates: []
  }
];

export interface RoomType {
  id: number;
  type: string;
  name: string;
  description: string;
  dailyRate: number;
  imageUrl: string;
}


export interface BookingSearchFromProps {
  filters: BookingFilters;
  updateFilter: (field: keyof BookingFilters, value: string) => void;
  onSearch: (e: React.SubmitEvent) => void;
  hasSearched: boolean;
  error: string | null;
  isLoading: boolean;
  roomTypeCatalog: RoomType[];
  hasActiveOffer?: boolean; 
}
export interface BookingSearchResultProps {
  hasSearched: boolean;
  isRecommendation: boolean;
  availableRooms: RoomType[];
  handleSelectRoom: (room: RoomType) => void;
}

export interface GuestData {
  guestName: string;
  guestLastName: string;
  guestEmail: string;
  creditCard: string;
}

export interface BookingFilters {
  startDate: string;
  endDate: string;
  roomType: string;
}



export interface BookingRequestDto {
  RoomTypeId: number;
  ClientName: string;
  ClientLastname: string;
  CardNumber: string;
  CheckIn: string;
  CheckOut: string;
  email: string;
  applyOffers: boolean;
  selectedOfferId: number | null;
}

export interface BookingResponseDto {
  confirmationCode: string;
  status: string;
}

export interface CalculateReservationPriceRequest {
  roomTypeId: number;
  checkIn: string;
  checkOut: string;
  applyOffers: boolean;
  selectedOfferId: number | null;
}

export interface ReservationNightPriceDetailResult {
  stayDate: string;
  basePrice: number;
  seasonId: number | null;
  seasonName: string | null;
  seasonAdjustmentPercentage: number;
  offerId: number | null;
  offerName: string | null;
  offerDiscountPercentage: number;
  finalNightPrice: number;
}

export interface ReservationPriceResult {
  totalAmount: number;
  nightCount: number;
  averageNightlyRate: number;
  nights: ReservationNightPriceDetailResult[];
}

