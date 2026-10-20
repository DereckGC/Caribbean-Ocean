import type { AboutUsProps } from "../types/aboutUs.props";
import styles from '../styles/AboutUs.module.css'

export default function AboutUsText({title, subtitle, description} : AboutUsProps){
  return(
    <div className={styles.textColumn}>
      <h1 className="title">{title}</h1>
      <div className="decorative-line"></div>
      <span className="subtitle">{subtitle}</span>
      <p className={styles.description}>{description}</p>
    </div>
  );
}