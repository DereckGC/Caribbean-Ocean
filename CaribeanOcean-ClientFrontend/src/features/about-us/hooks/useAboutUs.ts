import { useEffect, useState } from "react";
import { getAboutUs } from "../services/aboutUs.service";
import type { AboutUs } from "../types/aboutUs.types";

export function useAboutUs() {

  const [data, setData] = useState<AboutUs>();
  const [loading, setLoading] = useState(true);

  const [error, setError] = useState<Error | null>();

  useEffect(() => {
    getAboutUs()
      .then((res) => setData(res))
      .catch((error) => setError(error))
      .finally(() => setLoading(false))
  }, []);

  return { data, loading, error };
}