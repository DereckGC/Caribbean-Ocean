import styles from "../styles/Hero.module.css";
import type { HeroDotsProps } from "../types/hero.props";

export default function HeroDots({ totalSlides, currentSlide, onSelect }: HeroDotsProps) {
  return (
    <div className={styles.dots}>
      {Array.from({ length: totalSlides }).map((_, index) => (
        <button
          key={index}
          className={`${styles.dot} ${index === currentSlide ? styles.dotActive : ""}`}
          onClick={() => onSelect(index)}
          aria-label={`Go to slide ${index + 1}`}
        />
      ))}
    </div>
  );
}