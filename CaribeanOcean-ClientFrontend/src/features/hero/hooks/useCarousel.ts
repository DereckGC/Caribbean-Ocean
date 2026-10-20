import { useAutoAdvance } from "./useAutoAdvance";
import { useSlideNavigation } from "./useSlideNavigation";

export function useCarousel(totalSlides: number, slideInterval: number = 4000) {

  const { currentSlide, goToSlide: baseGoToSlide, prevSlide: basePrevSlide, nextSlide: baseNextSlide } = useSlideNavigation(totalSlides);

  const restartAutoAdvance = useAutoAdvance(baseNextSlide, slideInterval, totalSlides > 1);

  const goToSlide = (index: number) => {
    baseGoToSlide(index);
    restartAutoAdvance();
  }

  const prevSlide = () => {
    basePrevSlide();
    restartAutoAdvance();
  }

  const nextSlide = () => {
    baseNextSlide();
    restartAutoAdvance();
  }

  return {
    currentSlide,
    goToSlide,
    prevSlide,
    nextSlide,
  };
}