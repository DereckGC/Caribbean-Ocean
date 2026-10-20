import { useState, useEffect } from "react";
import "./MainNavBar.css";
import { Link } from "react-router-dom";
import { FiMenu, FiX } from "react-icons/fi";
import logo from "../../../assets/logo/logo.png";

export default function Navbar() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [scrolled, setScrolled] = useState(false);

  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);
  const closeMenu = () => setIsMenuOpen(false);

  useEffect(() => {
    const handleScroll = () => setScrolled(window.scrollY > 60);
    window.addEventListener("scroll", handleScroll);
    return () => window.removeEventListener("scroll", handleScroll);
  }, []);

  return (
    <nav className={`navbar${scrolled ? " navbar--scrolled" : ""}`}>
      <div className="navbar-container">

        {/* Logo */}
        <Link to="/" className="navbar-logo" onClick={closeMenu}>
          <span className="navbar-logo-icon">✦</span>
          <img src={logo} alt="Caribbean Ocean Resort & Spa logo" className="logo" />
          <div className="navbar-logo-text">
            <span className="navbar-logo-main">Caribbean Ocean</span>
            <span className="navbar-logo-sub">Resort & Spa</span>
          </div>
        </Link>

        {/* Hamburger */}
        <button className="menu-button" onClick={toggleMenu} aria-label="Toggle menu">
          {isMenuOpen ? <FiX size={26} /> : <FiMenu size={26} />}
        </button>

        {/* Links Desktop */}
        <div className="navbar-links">
          <Link to="/" className="nav-link">Home</Link>
          <Link to="/rooms" className="nav-link">Rooms</Link>
          <Link to="/#facilities" className="nav-link">Facilities</Link>
          <Link to="/#location" className="nav-link">How to get there</Link>
          <Link to="/#about" className="nav-link">About Us</Link>
          <Link to="/#footer" className="nav-link">Contact</Link>

          {/* Offers — special glowing badge */}
          <Link to="/offers" className="nav-link nav-link--offers">
            <span className="star star-1">✦</span>
            <span className="star star-2">✦</span>
            <span className="star star-3">✦</span>
            <span className="offers-label">Offers</span>
          </Link>

          {/* Booking CTA */}
          <Link to="/booking" className="nav-link nav-link--booking">
            <span className="booking-wave" />
            Book Now
          </Link>
        </div>

        {/* Mobile Menu */}
        <div className={`mobile-menu${isMenuOpen ? " open" : ""}`}>
          <div className="mobile-menu-inner">
            <Link to="/" className="nav-link" onClick={closeMenu}>Home</Link>
            <Link to="/rooms" className="nav-link" onClick={closeMenu}>Rooms</Link>
            <Link to="/#facilities" className="nav-link" onClick={closeMenu}>Facilities</Link>
            <Link to="/#location" className="nav-link" onClick={closeMenu}>How to get there</Link>
            <Link to="/#about" className="nav-link" onClick={closeMenu}>About Us</Link>
            <Link to="/#footer" className="nav-link" onClick={closeMenu}>Contact</Link>

            <Link to="/offers" className="nav-link nav-link--offers" onClick={closeMenu}>
              <span className="star star-1">✦</span>
              <span className="star star-2">✦</span>
              <span className="star star-3">✦</span>
              <span className="offers-label">Offers</span>
            </Link>

            <Link to="/booking" className="nav-link nav-link--booking" onClick={closeMenu}>
              <span className="booking-wave" />
              Book Now
            </Link>
          </div>
        </div>

        {/* Overlay */}
        {isMenuOpen && <div className="mobile-overlay" onClick={closeMenu} />}
      </div>
    </nav>
  );
}
