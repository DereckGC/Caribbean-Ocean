import styles from "../styles/AboutUs.module.css";
import { useGallery } from "../hooks/useGallery";

export default function Gallery() {

    // if (gallery.images.length === 0) return null;
    const {imgs, mainImage, setSelectedImageId} = useGallery();

    return (
        <div className={styles.aboutUsGallery}>
            <img
                src={mainImage?.url}
                alt={mainImage?.alt}
                loading='lazy'
                className={styles.galleryImage}
            />
            <>
                {imgs?.map(img => 
                    <img 
                        src={img.url}
                        alt={img.alt}
                        loading='lazy'
                        className={`${styles.galleryImage} ${styles.galleryThumb}`}
                        onClick={() => setSelectedImageId(img.id)}
                    />
                )}
            </>
        </div>
    );
}