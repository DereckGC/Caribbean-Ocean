import apiClient from "../../../shared/services/apiClient";
import type { AboutUs } from "../types/aboutUs.types";

export async function getAboutUs(): Promise<AboutUs> {
  const aboutUs = await apiClient.get<AboutUs>("/AboutUs");
  return aboutUs.data;
}