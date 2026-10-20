import { useState } from 'react';

export function usePagination(totalItems: number, itemsToShow: number) {
  const [currentIndex, setCurrentIndex] = useState(0);

  const next = () => {
    if (totalItems <= itemsToShow) return;
    setCurrentIndex((prev) => {
      if (prev + itemsToShow >= totalItems) return 0;
      return prev + 1;
    });
  };

  const prev = () => {
    if (totalItems <= itemsToShow) return;
    setCurrentIndex((prev) => {
      if (prev <= 0) return Math.max(0, totalItems - itemsToShow);
      return prev - 1;
    });
  };

  return { currentIndex, next, prev };
}
