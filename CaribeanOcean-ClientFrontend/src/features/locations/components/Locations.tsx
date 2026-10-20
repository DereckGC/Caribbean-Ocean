import styles from '../styles/Locations.module.css'
import { useLocations } from "../hooks/useLocations";
import { GrMapLocation } from "react-icons/gr";
import { MdOutlineLocationOn } from "react-icons/md";
import { AiOutlineGoogle } from "react-icons/ai";
import { FaWaze } from "react-icons/fa";
import { FaUber } from "react-icons/fa";

export default function Locations() {
  const { data, loading } = useLocations();

  if (loading) return <p>Cargando</p>

  return (
    <section className="sections">
      <h1 className="title">{data?.title}</h1>
      <span className="subtitle"><GrMapLocation /> {data?.subtitle}</span>
      <div className="decorative-line"></div>
      <p className="description">{data?.description}</p>

      <div className={styles.mapContainer}>
        <div className={styles.map}>
          <iframe 
          src={data?.images[0]?.url} 
          width="100%" 
          height="100%" 
          style={{ border: 0 }} 
          allowFullScreen={true}
          loading="lazy" 
          referrerPolicy="no-referrer-when-downgrade">
          </iframe>
        </div>
        <div className={styles.footerMap}>
          <div>
            <p className=''><MdOutlineLocationOn /> Puerto Viejo, Talamanca, Costa Rica</p>
          </div>
          <div className={styles.iconsGroup}>
            <a
              href="https://maps.app.goo.gl/s26zKKKKTnWVKfFZA"
              target="_blank"
              rel="noopener noreferrer"
              className={styles.iconLink}
            >
              <AiOutlineGoogle />
            </a>
            <a
              href="https://waze.com/ul?ll=9.8962574,-83.6357885&navigate=yes"
              target="_blank"
              rel="noopener noreferrer"
              className={styles.iconLink}
            >
              <FaWaze />

            </a>
            <a
              href="https://m.uber.com/ul/?action=setPickup&pickup=my_location&dropoff[latitude]=9.8962574&dropoff[longitude]=-83.6357885&dropoff[nickname]=Grupo%20Naturalba%20S.A"
              target="_blank"
              rel="noopener noreferrer"
              className={styles.iconLink}
            >
              <FaUber />
            </a>
          </div>
        </div>
      </div>
    </section>
  );
};
