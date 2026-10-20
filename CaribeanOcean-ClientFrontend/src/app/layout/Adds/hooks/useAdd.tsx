import { useEffect, useState } from "react";
import { getAdds } from "../services/add.service";
import type { Add } from "../types/add.types";

export function usePublicity(intervalMs: number = 5000) {
  const [adds, setAdds] = useState<Add[]>([]);
  const [currentAdd, setCurrentAdd] = useState(0);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    getAdds()
      .then((data) => {
        // Filtrar solo los adds activos
        setAdds(data.filter((add) => add.active));
      })
      .catch(() => {
        setError("No se pudieron cargar los anuncios.");
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  useEffect(() => {
    if (adds.length === 0) return;

    const interval = setInterval(() => {
      setCurrentAdd((prev) => (prev + 1) % adds.length);
    }, intervalMs);

    return () => clearInterval(interval);
  }, [adds, intervalMs]);

  return {
    currentAdd: adds[currentAdd] ?? null,
    loading,
    error,
  };
}