import { useState } from "react";
import RoomCard from "./RoomCard";
import styles from "../styles/rooms.module.css";

interface Props {
  rooms: any[];
}

export default function RoomsCarousel({ rooms }: Props) {
  const [activeIndex, setActiveIndex] = useState(0);
  const total = rooms.length;

  const next = () => {
    setActiveIndex((prev) => (prev + 1) % total);
  };

  const prev = () => {
    setActiveIndex((prev) => (prev - 1 + total) % total);
  };

  return (
    <div className={styles.carousel}>
      <button onClick={prev} className={`${styles.arrow} ${styles.leftArrow}`}>‹</button>

      <div className={styles.track}>
        {rooms.map((room, index) => {
          let position = "hidden";
          if (index === activeIndex) position = "center";
          else if (index === (activeIndex - 1 + total) % total) position = "left";
          else if (index === (activeIndex + 1) % total) position = "right";

          return (
            <div
              key={room.id}
              className={`${styles.item} ${styles[position]}`}
            >
              <RoomCard room={room} isActive={index === activeIndex} />
            </div>
          );
        })}
      </div>

      <button onClick={next} className={`${styles.arrow} ${styles.rightArrow}`}>›</button>
    </div>
  );
}