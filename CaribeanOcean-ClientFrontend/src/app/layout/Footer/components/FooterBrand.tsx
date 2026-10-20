import logo from "../../../../assets/logo/logo.png";
import type { FooterBrandProps } from "../types/footer.props";
import FooterSocialIcons from "./FooterSocialIcons";

export default function FooterBrand({footerBrand, socialMedia} : FooterBrandProps) {
  return (
    <div className="footer-section footer-brand">
      <img src={logo} alt="Caribbean Ocean Resort & Spa logo" className="logo" />
      <div className="footer-brand-text">
        <span className="footer-brand-name">{footerBrand.title}</span>
        <span className="footer-brand-tagline">{footerBrand.subtitle}</span>
      </div>
      <p className="footer-brand-desc">
        {footerBrand.description}
      </p>
      <FooterSocialIcons socialMedia={socialMedia} />
    </div>
  );
}
