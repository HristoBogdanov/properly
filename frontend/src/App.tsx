import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./contexts/useAuth";
import { Outlet } from "react-router-dom";
import Navbar from "./components/common/Navbar";
import NavigatorInitializer from "./helpers/NavigatorInitializer";

export default function App() {
  return (
    <UserProvider>
      <Navbar />
      <NavigatorInitializer />
      <Outlet />
      <ToastContainer />
    </UserProvider>
  );
}
