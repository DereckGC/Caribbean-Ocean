import { useOffers } from "../hooks/useOffers";
import styles from "../styles/Offers.module.css";
import OfferCarrusel from "./OfferCarrusel";


export default function Offers() {

  const {data, loading} = useOffers();

  if (loading) {
    return <div className={styles.container}>Loading special offers...</div>;
  }

  return (
    <div className={styles.container}>
      <h2 className={`text-h2 ${styles.title}`}>Special Offers</h2>
      <OfferCarrusel offers={data || []} />
    </div>
    
  );

};
