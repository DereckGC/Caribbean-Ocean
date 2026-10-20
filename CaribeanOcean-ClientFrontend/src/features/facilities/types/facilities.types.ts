export interface Facility {
  id: string;
  name: string;
  description: string;
  image_Url: string;
  features: string[];
}

export interface FacilitiesSection {
  title: string;
  subtitle: string;
  facilities: Facility[];
}

export interface paginationParams {
  numberOfItems: number;
}

