import styles from "../styles/Hero.module.css";
import type { HeroContentProps } from "../types/hero.props";

export default function HeroContent({ title, subtitle, description }: HeroContentProps) {
    return (
        <div className={styles.content}>
            <span className={styles.tagline}>{subtitle}</span>
            <h1 className={styles.title}>{title}</h1>
            <p className={styles.description}>{description}</p>
            <a href="/rooms" className={styles.ctaButton}>
                Explore Our Rooms
            </a>
        </div>
    );
}