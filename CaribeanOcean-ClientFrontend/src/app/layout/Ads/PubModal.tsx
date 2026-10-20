import "./Publicity.css"
import { useEffect, useState } from "react";

const pubs = [
  {
    id: 1,
    image: "src/assets/rooms/JuniorRoom.jpg",
  },
  {
    id: 2,
    image: "src/assets/rooms/PremiunRoom.jpg",
  },
  {
    id: 3,
    image: "src/assets/rooms/StandardRoom.jpg",
  },
];

export default function PubModal() {
  const [currentPub, setCurrentPub] = useState(0);
 

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentPub((prev) => (prev + 1) % pubs.length);
    }, 5000); // cambia cada 5 segundos

    return () => clearInterval(interval);
  }, []);

 

  return (
    <div className="pub-modal">

      <img
        src={pubs[currentPub].image}
        alt="Publicidad"
        className="pub-image"
      />
    </div>
  );
}







