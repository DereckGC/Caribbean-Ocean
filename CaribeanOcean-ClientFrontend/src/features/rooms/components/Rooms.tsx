import RoomsCarousel from "./RoomsCarousel";
import styles from "../styles/rooms.module.css";

interface Props {
  data?: any;
  loading?: boolean;
}

export const Rooms = ({ data, loading }: Props) => {
  return (
    <div className={styles.container}>
      <h2 className="title">{data?.title}</h2>
      <h3 className="subtitle">{data?.subtitle}</h3>

      {loading && <p>Loading rooms...</p>}

      {!loading && data?.rooms?.length > 0 && (
        <RoomsCarousel rooms={data.rooms} />
      )}
    </div>
  );
};