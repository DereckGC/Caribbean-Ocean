import { useEffect, useRef } from "react";
import { useLocation } from "react-router-dom";

export const ScrollToHash = () => {
  const { pathname, hash } = useLocation();
  const retryRef = useRef<number | null>(null);

  useEffect(() => {
    if (retryRef.current !== null) {
      clearInterval(retryRef.current);
      retryRef.current = null;
    }

    if (hash) {
      const id = hash.replace("#", "");
      let attempts = 0;

      const tryScroll = () => {
        const element = document.getElementById(id);
        if (element) {
          if (retryRef.current !== null) {
            clearInterval(retryRef.current);
            retryRef.current = null;
          }
          element.scrollIntoView({ behavior: "smooth" });
          return;
        }
        attempts++;
        if (attempts >= 20) {
          if (retryRef.current !== null) {
            clearInterval(retryRef.current);
            retryRef.current = null;
          }
        }
      };

      requestAnimationFrame(() => {
        requestAnimationFrame(() => {
          const element = document.getElementById(id);
          if (element) {
            element.scrollIntoView({ behavior: "smooth" });
          } else {
            retryRef.current = window.setInterval(tryScroll, 50);
          }
        });
      });
    } else {
      window.scrollTo({ top: 0, behavior: "instant" });
    }

    return () => {
      if (retryRef.current !== null) {
        clearInterval(retryRef.current);
        retryRef.current = null;
      }
    };
  }, [pathname, hash]);

  return null;
};