export interface Room {
  id: string;
  title: string;
  description: string;
  dailyRate: number;
  imageUrl: string;
  features: string[];
  active: boolean;
}

export interface RoomSection {
  title: string;
  subtitle: string;
  facilities: Room[];
}