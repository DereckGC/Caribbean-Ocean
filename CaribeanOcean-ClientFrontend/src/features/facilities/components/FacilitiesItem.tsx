import type { Facility } from "../types/facilities.types";
import styles from '../styles/Facilities.module.css'

export default function FacilityItem({ facility, index }: { facility: Facility, index: number }) {
    const hues = [120, 35, 210, 280, 0, 180];

    const isEven = index % 2 === 0;
    const itemClass = `${styles.facilityItem} ${!isEven ? styles.inverted : ''}`;

    return(
        <div className={itemClass}>
            <div className={styles.leftSection}>
                <figure className={styles.imageWrapper}>
                    <img src={facility.image_Url} alt={facility.name} />
                </figure>
                <ul className={styles.facilityList}>
                    {facility.features.map((label, i) => {
                        const hue = hues[i % hues.length];
                        return (
                            <li
                                key={label}
                                className={styles.facilityLabels}
                                style={{
                                    backgroundColor: `hsla(${hue}, 80%, 50%, 0.15)`,
                                    borderColor: `hsla(${hue}, 80%, 45%, 0.8)`,
                                    color: `hsla(${hue}, 80%, 20%, 1)`,
                                }}
                            >
                                {label}
                            </li>
                        );
                    })}
                </ul>
            </div>
            
            <div className={styles.rightSection}>
                <div>
                    <center><h2>{facility.name}</h2></center>
                    <br />
                    <p>{facility.description}</p>
                </div>
            </div>
        </div>
    )
}