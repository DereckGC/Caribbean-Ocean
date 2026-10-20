import styles from "../styles/rooms.module.css";

export const Badge = ({ text }: { text: string }) => {
  const getBadgeClass = (feature: string) => {
    const t = feature.toLowerCase();

    if (t.includes("ocean")) return styles.ocean;
    if (t.includes("wifi")) return styles.wifi;
    if (t.includes("king")) return styles.bed;
    if (t.includes("bed")) return styles.beds;
    if (t.includes("pool")) return styles.pool;
    if (t.includes("balcony")) return styles.balcony;

    return styles.balcony;
  };

  return (
    <span className={`${styles.badge} ${getBadgeClass(text)}`}>
      {text}
    </span>
  );
};