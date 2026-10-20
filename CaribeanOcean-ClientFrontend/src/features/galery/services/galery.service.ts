import apiClient from "../../../shared/services/apiClient";
import type { GalleryImage, GalleryResponse } from "../types/gallery.types";

export async function getGalleryImages(): Promise<GalleryImage[]> {
  const response = await apiClient.get<GalleryResponse>("/Gallery");
  return response.data.images;
}