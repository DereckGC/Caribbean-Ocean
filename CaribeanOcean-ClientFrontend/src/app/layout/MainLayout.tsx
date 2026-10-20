import Footer from "./Footer/components/Footer";
import MainNavBar from "./Navbar/MainNavBar";
import AddModal from "./Adds/components/add";
export default function Layout({ children }: { children: React.ReactNode }) {
  return (
    <>
      <main>{children}</main>
      <Footer />
      <MainNavBar/>
      <AddModal/>
    </>
  );
}