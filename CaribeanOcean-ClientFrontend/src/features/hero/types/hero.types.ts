export interface Hero {
  title: string;
  subtitle : string;
  description: string;
  images: HeroImage[];
}

export type HeroImage = {
  url : string,
  alt : string
}