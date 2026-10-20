import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './app/layout/MainLayout';
import { HomePage } from './features/home/HomePage';
import { Booking } from './features/booking/components/Booking';
import AboutUs from './features/about-us/components/AboutUs';
import Offers from './features/offers/components/Offers';
import { RoomsPage } from './features/rooms/components/RoomsPage';
import { ScrollToHash } from './shared/components/ScrollToHash';
import { AnimatedRoutes } from './shared/components/AnimatedRoutes/AnimatedRoutes';

export default function App() {
  return (
    <BrowserRouter>
      <ScrollToHash />

      <Layout>
        <AnimatedRoutes>
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/about" element={<AboutUs />} />
            <Route path="/booking" element={<Booking />} />
            <Route path="/offers" element={<Offers />} />
            <Route path="/rooms" element={<RoomsPage />} />
            <Route path="*" element={<div>Página no encontrada</div>} />
          </Routes>
        </AnimatedRoutes>
      </Layout>
    </BrowserRouter>
  );
}
