import { FiMapPin } from "react-icons/fi";
import type { FooterLocationProps } from "../types/footer.props";

export default function FooterLocation({location} : FooterLocationProps) {
  return (
    <div className="footer-section">
      <h3 className="footer-heading">Location</h3>
      <div className="footer-location">
        <a className="footer-location-icon" href={location.url_location} target="_blank">
          <FiMapPin className="footer-icon" />
        </a>
        <p>
            {location.location}
        </p>
      </div>
    </div>
  );
}
