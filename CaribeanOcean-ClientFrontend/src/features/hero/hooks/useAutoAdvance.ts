import { useRef, useCallback, useEffect } from "react";

export function useAutoAdvance(callback: () => void, interval: number, active: boolean = true) {
  const intervalRef = useRef<ReturnType<typeof setInterval> | null>(null);

  const start = useCallback(() => {
    if (!active) return;
    if (intervalRef.current) clearInterval(intervalRef.current);
    intervalRef.current = setInterval(callback, interval);
  }, [callback, interval, active]);

  useEffect(() => {
    start();
    return () => {
      if (intervalRef.current) clearInterval(intervalRef.current);
    };
  }, [start]);

  return start;
}