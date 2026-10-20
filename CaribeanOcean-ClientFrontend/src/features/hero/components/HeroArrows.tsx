import styles from "../styles/Hero.module.css";
import type { HeroArrowProps } from "../types/hero.props";

export default function HeroArrow( {onNext, onPrev} : HeroArrowProps){
    return (
        <>
            <button className={`${styles.arrow} ${styles.arrowLeft}`} onClick={onPrev} aria-label="Previous slide">‹</button>
            <button className={`${styles.arrow} ${styles.arrowRight}`} onClick={onNext} aria-label="Next slide">›</button>
        </>
    );
}