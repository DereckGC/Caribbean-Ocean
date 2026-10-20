import styles from "../styles/Hero.module.css";
import type { HeroCarruselProps } from "../types/hero.props";

export default function HeroCarrusel({ images, currentSlide }: HeroCarruselProps) {

    return (
        <div className={styles.slidesWrapper}>
            {images.map((img, index) => (
                <div
                    key={index}
                    className={`${styles.slide} ${index === currentSlide ? styles.slideActive : ""}`}
                >
                    <img
                        src={img.url}
                        alt={`${img.alt} - View ${index + 1}`}
                        className={styles.slideImage}
                        loading={index === 0 ? "eager" : "lazy"}
                    />
                </div>
            ))}
            <div className={styles.overlay} />
        </div>
    );

}