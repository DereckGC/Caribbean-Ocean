import styles from "../styles/add.module.css";
import { usePublicity } from "../hooks/useAdd";

export default function AddModal() {
  const { currentAdd } = usePublicity(5000);

  if (!currentAdd) return null;

  return (
    <div className={styles["pub-modal"]}>
      <a 
        href={currentAdd.targetURL} 
        target="_blank" 
        rel="noopener noreferrer"
        className={styles["pub-link"]}
      >
        <img
          src={currentAdd.imageURL}
          alt="Publicidad"
          className={styles["pub-image"]}
        />
      </a>
    </div>
  );
}