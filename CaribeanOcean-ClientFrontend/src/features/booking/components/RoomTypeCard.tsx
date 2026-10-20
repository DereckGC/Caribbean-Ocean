
import styles from './Booking.module.css';
import type { RoomType } from '../types/booking.types';

            
interface RoomTypeCardProps {
    room:RoomType;
    onSelect: (roomId: number) => void;
}

export const RoomTypeCard = ({room, onSelect}: RoomTypeCardProps) => {
    return (
    <div className={styles.roomCard}>
      <div className={styles.roomImagePlaceholder}>
        {room.imageUrl ? (
          <img src={room.imageUrl} alt={room.name} className={styles.roomImage} />
        ) : (
          <span>Image Placeholder</span>
        )}
      </div>
      
      <div className={styles.roomContent}>
        <div>
          <div className={styles.roomHeader}>
            <div>
              <h3>{room.name}</h3>
              <span className={styles.roomType}>{room.type}</span>
            </div>
            <div className={styles.roomPrice}>
             From ${room.dailyRate} <span>/ night</span>
            </div>
          </div>
          <p className={styles.roomDescription}>{room.description}</p>
        </div>
        <button className={styles.bookButton} onClick={() => onSelect(room.id)}>
          Select Room
        </button>
      </div>
    </div>
  );
};

