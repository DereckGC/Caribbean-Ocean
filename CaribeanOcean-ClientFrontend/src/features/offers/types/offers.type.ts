export interface Offer {
  id: number,
  name: string,
  description: string,
  discount: number,
  startDate: string,
  endDate: string,
  roomType: string,
  urlImage?: string,
  roomTypeId: number
}