import { useResponsiveItems } from './useResponsiveItems';
import { usePagination } from './usePagination';
import type { OfferCarruselProps } from "../types/offers.props";

export function useCarousel({offers} : OfferCarruselProps) {
  const itemsToShow = useResponsiveItems(3);
  const safeItems = offers || [];
  
  const { currentIndex, next, prev } = usePagination(safeItems.length, itemsToShow);

  const visibleItems = safeItems.slice(currentIndex, currentIndex + itemsToShow);

  return {
    currentIndex,
    next,
    prev,
    visibleItems,
    itemsToShow,
  };
}