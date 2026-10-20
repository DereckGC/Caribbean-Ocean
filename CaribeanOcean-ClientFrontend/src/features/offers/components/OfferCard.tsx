import type { Offer } from "../types/offers.type";
import OfferButton from "./OfferButton";
import styles from "../styles/Offers.module.css";

export default function OfferCard({ offer }: { offer: Offer }) {

    return (
        <div className={styles.card}>
            <h3 className="text-h3">{offer.name}</h3>
            <h4 className="text-h4">{offer.roomType}</h4>
            <img className={styles.card} src={offer.urlImage} alt="Room Image" />
            <div className={`text-h4 ${styles.discount}`}>-{offer.discount}%</div>
            <p className={styles.dates}>
                <span>From: {offer.startDate}</span>
                <span> &nbsp;|&nbsp; </span>
                <span>To: {offer.endDate}</span>
            </p>
            <OfferButton Id={offer.id} roomTypeId={offer.roomTypeId} />
        </div>
    )

}