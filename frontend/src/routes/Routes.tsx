import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import LoginPage from "@/pages/LoginPage";
import RegisterPage from "@/pages/RegisterPage";
import HomePage from "@/pages/HomePage";
import PropertiesPage from "@/pages/PropertiesPage";
import ServerErrorPage from "@/pages/ServerErrorPage";
import DashboardPage from "@/pages/DashboardPage";
import AddPropertyPage from "@/pages/PropertyManager/AddPropertyPage";
import PropertyListPage from "@/pages/PropertyManager/PropertyListPage";
import InfoPropertyPage from "@/pages/PropertyManager/InfoPropertyPage";
import UpdatePropertyPage from "@/pages/PropertyManager/UpdatePropertyPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "/register", element: <RegisterPage /> },
      { path: "/login", element: <LoginPage /> },
      { path: "/properties", element: <PropertiesPage /> },
      { path: "/server-error", element: <ServerErrorPage /> },
      {
        path: "/dashboard",
        element: <DashboardPage />,
        children: [
          { path: "", element: <PropertyListPage /> },
          { path: "/dashboard/properties/add", element: <AddPropertyPage /> },
          {
            path: "/dashboard/properties/information/:id",
            element: <InfoPropertyPage />,
          },
          {
            path: "/dashboard/properties/edit/:id",
            element: <UpdatePropertyPage />,
          },
        ],
      },
    ],
  },
]);
