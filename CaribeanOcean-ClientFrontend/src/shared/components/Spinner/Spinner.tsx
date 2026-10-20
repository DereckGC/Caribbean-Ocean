import styles from './Spinner.module.css';
interface SpinnerProps {
  message?: string; // Por si quieres decir "Buscando habitaciones..."
}
export const Spinner = ({ message }: SpinnerProps) => {
  return (
    <div className={styles.spinnerContainer}>
      <div className={styles.loader}></div>
      {message && <p className={styles.spinnerText}>{message}</p>}
    </div>
  );
};