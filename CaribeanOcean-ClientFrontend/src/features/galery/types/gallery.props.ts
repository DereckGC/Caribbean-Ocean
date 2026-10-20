import type { GalleryImage } from "./gallery.types";
export interface MainImageProps{
  image : GalleryImage;
}

export interface ThumbnailsProps{
  images : GalleryImage[];
  onSelect: (id:number) => void;
}

export interface AboutUsProps{
  description: string
}