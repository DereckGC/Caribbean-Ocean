import "../styles/Footer.css";
import FooterBrand from "./FooterBrand";
import FooterLinks from "./FooterLinks";
import FooterLocation from "./FooterLocation";
import FooterContact from "./FooterContact";
import FooterBottom from "./FooterBottom";
import { useFooter } from "../hooks/useFooter";

export default function Footer() {
  
  const {footerBrand, socialMedia, emails, phoneNumbers, location, error, isLoading} = useFooter();
  if (error) return <div className="error">Something went wrong. Please try again.</div>;
  if (isLoading || !footerBrand || !socialMedia || !emails || !phoneNumbers || !location) return null;

  return (
    <footer className="footer" id="footer">

      <div className="footer-accent" />

      <div className="footer-container">
        <FooterBrand footerBrand={footerBrand} socialMedia={socialMedia}/>
        <FooterLinks />
        <FooterLocation location={location}/>
        <FooterContact emails={emails} phoneNumbers={phoneNumbers} />
      </div>

      <FooterBottom />

    </footer>
  );
}