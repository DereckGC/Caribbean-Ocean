export interface GalleryImage {
  id: number; 
  url: string;
  alt: string; 
}

export interface GalleryResponse {
  title: string;
  subtitle: string;
  description: string;
  images: GalleryImage[];
}