import { useFacilities } from "../hooks/useFacilities";
import FacilitiesList from "./FacilitiesList";
import { MdKeyboardArrowDown, MdKeyboardArrowUp } from "react-icons/md";
import styles from '../styles/Facilities.module.css'

export default function Facilities() {
  const { data, loading, loadMore, reset, multiplier } = useFacilities();

  if (loading) return <p>Cargando</p>

  const hasMore = data?.facilities && data.facilities.length >= 3 * multiplier;

  return (
    <section className="sections">
      <h1 className="title">{data?.title}</h1>
      <div className="decorative-line"></div>
      <span className="subtitle">{data?.subtitle}</span>
      <div>
        <FacilitiesList facilities={data?.facilities} />
      </div>
      
      {hasMore ? (
        <button className={`${styles.showButton} description`} onClick={loadMore}>
          Show more <MdKeyboardArrowDown />
        </button>
      ) : (
        <button className={`${styles.showButton} description`} onClick={reset}>
          Show less <MdKeyboardArrowUp />
        </button>
      )}

    </section>
  )
}