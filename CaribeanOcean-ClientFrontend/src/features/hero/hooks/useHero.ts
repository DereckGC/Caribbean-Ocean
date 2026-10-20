import { useEffect, useState } from "react";
import type { Hero } from "../types/hero.types";
import { getHero } from "../services/hero.service";

export function useHero() {
  const [data, setData] = useState<Hero | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    try {
      getHero().then((res) => {
        setData(res);
        setLoading(false);
      });
    } catch (error) {
      console.error("Error fetching hero:", error);
      setLoading(false);
    }
  }, []);

  return { data, loading };
}