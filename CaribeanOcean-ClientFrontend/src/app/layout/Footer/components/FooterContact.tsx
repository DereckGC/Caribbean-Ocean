import { FiPhone, FiMail } from "react-icons/fi";
import { FaWhatsapp } from "react-icons/fa";
import type { FooterContactProps } from "../types/footer.props";

export default function FooterContact({phoneNumbers, emails} : FooterContactProps) {
  
  return (

    <div className="footer-section">
      <h3 className="footer-heading">Contact Us</h3>
      <ul className="footer-contact">
        {
          phoneNumbers.map((phoneNumber) => (
            <li>
              {phoneNumber.type === "calls" ? (
                <FiPhone className="footer-icon" />
              ) : (
                <FaWhatsapp className="footer-icon" />
              )}
              <span>{phoneNumber.contact}</span>
            </li>
          ))
        }

        {
          emails.map((email) => (
            <li>
              <FiMail className="footer-icon" />
              <p>
                {email.contact}
              </p>
            </li>
          ))
        }

      </ul>
    </div>
  );

}
