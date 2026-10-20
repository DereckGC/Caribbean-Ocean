import styles from "../styles/Hero.module.css";
import { useHero } from "../hooks/useHero";
import { useCarousel } from "../hooks/useCarousel";
import HeroCarrusel from "./HeroCarrusel";
import HeroContent from "./HeroContent";
import HeroDots from "./HeroDots";
import HeroArrow from "./HeroArrows";

export default function Hero() {

  const { data: hero, loading } = useHero();
  const totalSlides = hero?.images.length ?? 0;

  const { currentSlide, goToSlide, prevSlide, nextSlide } = useCarousel(totalSlides);

  if (loading || !hero) return null;
  
  return (
    <section className={styles.hero} id="hero">
      <HeroCarrusel images={hero.images} currentSlide={currentSlide}/>
      <HeroContent title={hero.title} subtitle={hero.subtitle} description={hero.description}/>
      <HeroArrow onNext={nextSlide} onPrev={prevSlide}/>
      <HeroDots currentSlide={currentSlide} onSelect={goToSlide} totalSlides={totalSlides}/>
    </section>
  );
}