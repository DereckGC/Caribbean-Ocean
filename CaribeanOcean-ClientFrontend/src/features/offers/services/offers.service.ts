import apiClient from "../../../shared/services/apiClient";
import type { Offer } from "../types/offers.type";

export async function getOffers(): Promise<Offer[]> {
  const response = await apiClient.get<Offer[]>("/offer");
  
  return response.data;
}