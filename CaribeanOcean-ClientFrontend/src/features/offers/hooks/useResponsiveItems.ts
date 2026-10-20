import { useState, useEffect } from 'react';

export function useResponsiveItems(defaultItems = 3) {
  const [itemsToShow, setItemsToShow] = useState(defaultItems);

  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth < 768) {
        setItemsToShow(1);
      } else if (window.innerWidth < 1024) {
        setItemsToShow(2);
      } else {
        setItemsToShow(3);
      }
    };
    
    handleResize();
    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  return itemsToShow;
}
