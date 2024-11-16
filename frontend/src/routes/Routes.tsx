import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
import HomePage from "@/pages/HomePage";
import PropertiesPage from "@/pages/PropertiesPage";
import ServerErrorPage from "@/pages/ServerErrorPage";
import DashboardPage from "@/pages/DashboardPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "/register", element: <RegisterPage /> },
      { path: "/login", element: <LoginPage /> },
      { path: "/properties", element: <PropertiesPage /> },
      { path: "/dashboard", element: <DashboardPage /> },
      { path: "/server-error", element: <ServerErrorPage /> },
    ],
  },
]);
