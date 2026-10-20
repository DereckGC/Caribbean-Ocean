import { useNavigate } from "react-router-dom";
import styles from "../styles/rooms.module.css";
import { Badge } from "./Badge";

interface Props {
  room: any;
  isActive: boolean;
}

export default function RoomCard({ room, isActive }: Props) {
  const navigate = useNavigate();

  const handleBook = () => {
    if (isActive) {
      navigate("/booking"); 
    }
  };

  return (
    <div className={`${styles.card} ${isActive ? styles.active : ""}`}>
     
      <img src={room.imageUrl} alt={room.name} />
      <h3>{room.name}</h3>
      <p>${room.dailyRate.toFixed(2)}</p>

      <button
        className={styles.button}
        onClick={handleBook}
        disabled={!isActive} 
        style={{
          cursor: isActive ? "pointer" : "not-allowed",
          opacity: isActive ? 1 : 0.5
        }}
      >
        Book
      </button>

      <div className={styles.features}>
        {room.features?.map((f: string, i: number) => (
          <Badge key={i} text={f} />
        ))}
      </div>
    </div>
  );
}