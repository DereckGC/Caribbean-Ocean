import styles from '../styles/AboutUs.module.css'
import type { MainImageProps } from '../types/gallery.props';

export default function MainImage({image} : MainImageProps) {

    return(
        <img
            src={image.url}
            alt={image.alt}
            loading='lazy'
            className={styles.galleryImage}
        />
    );

}