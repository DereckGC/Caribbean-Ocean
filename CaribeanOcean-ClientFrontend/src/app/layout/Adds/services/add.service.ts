import apiClient from "../../../../shared/services/apiClient";
import type { Add } from "../types/add.types";

export interface Publicity {
  id: number;
  image: string;
}



export const getAdds = async (): Promise<Add[]> => {
  const response = await apiClient.get<Add[]>("admin/adds");
  return response.data;
};