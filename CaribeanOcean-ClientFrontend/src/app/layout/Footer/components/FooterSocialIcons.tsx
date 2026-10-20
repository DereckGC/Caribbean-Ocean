import { FaFacebookF, FaInstagram, FaTiktok } from "react-icons/fa";
import type { FooterSocialIconsProps } from "../types/footer.props";

export default function FooterSocialIcons({ socialMedia }: FooterSocialIconsProps) {
  return (
    <div className="footer-social">
      {socialMedia.map((media) => (
        <a 
          href={media.contact} 
          aria-label={media.type} 
          className="footer-social-icon"
          target="_blank"
        >
          {media.type === "facebook" && <FaFacebookF />}
          {media.type === "instagram" && <FaInstagram />}
          {media.type === "tiktok" && <FaTiktok />}
        </a>
      ))}
    </div>
  );
}