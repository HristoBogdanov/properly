import "react-toastify/dist/ReactToastify.css";
import { ToastContainer } from "react-toastify";
import { UserProvider } from "./contexts/useAuth";
import { Outlet } from "react-router-dom";

export default function App() {
  return (
    <UserProvider>
      <Outlet />
      <ToastContainer />
    </UserProvider>
  );
}
