//import apiClient from "../../../shared/services/apiClient";
import apiClient from "../../../shared/services/apiClient";
import type { Location } from "../types/location.types";

export async function getLocation(): Promise<Location> {
  const response = await apiClient.get<Location>("/Locations");
  return response.data;
}

