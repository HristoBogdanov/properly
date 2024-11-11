import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <RegisterPage /> },
      { path: "/login", element: <LoginPage /> },
    ],
  },
]);
