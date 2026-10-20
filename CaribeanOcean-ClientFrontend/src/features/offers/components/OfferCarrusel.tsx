import type { OfferCarruselProps } from "../types/offers.props";
import styles from "../styles/Offers.module.css";
import OfferCard from "./OfferCard";
import { useCarousel } from "../hooks/useCarousel";

export default function OfferCarrusel({ offers }: OfferCarruselProps) {

    if (offers.length == 0) return null;
    const { currentIndex, next, prev, itemsToShow } = useCarousel({ offers });

    const slidePercent = (currentIndex / offers.length) * 100;

    return (
        <div className={styles.carouselWrapper}>
            <button className={styles.arrow} onClick={prev} aria-label="Previous offers">
                &#10094;
            </button>

            <div className={styles.trackViewport}>
                <div
                    className={styles.track}
                    style={{
                        width: `${(offers.length / itemsToShow) * 100}%`,
                        transform: `translateX(-${slidePercent}%)`,
                    }}
                >
                    {offers.map((offer, index) => (
                        <div
                            key={index}
                            className={styles.trackItem}
                            style={{ width: `${100 / offers.length}%` }}
                        >
                            <OfferCard offer={offer} />
                        </div>
                    ))}
                </div>
            </div>

            <button className={styles.arrow} onClick={next} aria-label="Next offers">
                &#10095;
            </button>
        </div>
    );
}