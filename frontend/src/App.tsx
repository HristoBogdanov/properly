import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./contexts/useAuth";
import { Outlet } from "react-router-dom";
import Navbar from "./components/common/Navbar";
import { SidebarProvider } from "./components/ui/sidebar";

export default function App() {
  return (
    <UserProvider>
      <SidebarProvider>
        <Navbar />
        <Outlet />
        <ToastContainer />
      </SidebarProvider>
    </UserProvider>
  );
}
