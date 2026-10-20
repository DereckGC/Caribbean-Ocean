import type { FacilitiesSection, paginationParams  } from "../types/facilities.types";
import apiClient from "../../../shared/services/apiClient";


export async function getFacility(params: paginationParams): Promise<FacilitiesSection> {
  const startIndex = 0;
  const endIndex = startIndex + params.numberOfItems;

  const response = await apiClient.get<FacilitiesSection>("/Facility");
  const section = response.data;
  return {
    ...section,
    facilities: (section.facilities).slice(startIndex, endIndex)
  };
}

