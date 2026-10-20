import apiClient from "../../../shared/services/apiClient";
import type { RoomSection } from "../types/rooms.types";

export async function getRooms(): Promise<RoomSection> {
  const response = await apiClient.get<RoomSection>("/RoomType/PageClient");
  return response.data;
}
