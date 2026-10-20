import type { Hero } from "../types/hero.types";
import apiClient from "../../../shared/services/apiClient";

export async function getHero(): Promise<Hero> {
  const response = await apiClient.get<Hero>("/hero");
  return response.data;
}