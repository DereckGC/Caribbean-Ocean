import { useEffect, useState } from "react";
import { getLocation } from "../services/locations.service";
import type { Location } from "../types/location.types";

export function useLocations() {
  const [data, setData] = useState<Location>();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getLocation()
    .then((res) => {
      setData(res);
      setLoading(false);
    });
  }, []);

  return { data, loading };
}