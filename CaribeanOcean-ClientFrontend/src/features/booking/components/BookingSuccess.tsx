
import styles from './Booking.module.css';


interface Props {
  guestName: string;
  guestLastName: string;
  roomName: string;
  reservationCode: string;
  onClose: () => void;
}

export const BookingSuccess = ({ guestName, guestLastName, roomName, reservationCode, onClose }: Props) => {
  return (
    <div className={styles.checkoutHero}>
      <div className={styles.successContainer}>
        <button className={styles.closeSuccessBtn} onClick={onClose}>✕</button>
        <h2 className={styles.successTitle}>Reservation made successfully</h2>
        <div className={styles.successIcon}>✓</div>
        <p className={styles.successMessage}>
          Thank you <strong>{guestName} {guestLastName}</strong>, your booking for the <strong>{roomName}</strong> was successfully completed.
        </p>
        <div className={styles.reserveCode}>Reserve code: {reservationCode}</div>
        <p className={styles.successFooter}>Thank you for choosing us</p>
      </div>
    </div>
  );
};

