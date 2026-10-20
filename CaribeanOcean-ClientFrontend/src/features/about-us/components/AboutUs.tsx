import styles from "../styles/AboutUs.module.css";
import { useAboutUs } from "../hooks/useAboutUs";
import AboutUsText from "./AboutUsText";

export default function AboutUs() {
  const { data, loading, error } = useAboutUs();

  if (loading) return <div className={styles.loading}>Loading...</div>;
  if (error) return <div className={styles.error}>Something went wrong. Please try again.</div>;
  if (!data) return null;

  return (
    <section className="sections">
      <AboutUsText title={data.title} subtitle={data.subtitle} description={data.description} />
    </section>
  );
};