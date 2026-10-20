import { useEffect, useState } from "react";
import { getRooms } from "../services/rooms.service";
import type { RoomSection } from "../types/rooms.types";

export function useRooms() {
  const [data, setData] = useState<RoomSection>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getRooms().then((res) => {
      setData(res);
      setLoading(false);
    });
  }, []);

  return { data, loading };
}