import type { ReactNode } from 'react';
import { useLocation } from 'react-router-dom';
import './AnimatedRoutes.css';

export function AnimatedRoutes({ children }: { children: ReactNode }) {
  const { pathname } = useLocation();

  return (
    <div className="animated-route" key={pathname}>
      {children}
    </div>
  );
}
