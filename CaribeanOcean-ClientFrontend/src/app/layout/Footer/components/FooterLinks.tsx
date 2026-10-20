import { Link } from "react-router-dom";

export default function FooterLinks() {
  return (
    <div className="footer-section">
      <h3 className="footer-heading">Explore</h3>
      <ul className="footer-links">
        <li><Link to="/offers">Offers</Link></li>
        <li><Link to="/rooms">Rooms & Rates</Link></li>
        <li><Link to="/booking">Online Reservation</Link></li>
        <li><Link to="/#facilities">Facilities</Link></li>
        <li><Link to="/#location">How to get there</Link></li>
        <li><Link to="/#about">About Us</Link></li>
      </ul>
    </div>
  );
}
