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
import CategoryListPage from "@/pages/CategoryManager/CategoryListPage";
import AddCategoryPage from "@/pages/CategoryManager/AddCategoryPage";
import InfoCategoryPage from "@/pages/CategoryManager/InfoCategoryPage";
import UpdateCategoryPage from "@/pages/CategoryManager/UpdateCategoryPage";
import FeatureListPage from "@/pages/FeatureManager/FeatureListPage";
import AddFeaturePage from "@/pages/FeatureManager/AddFeaturePage";
import InfoFeaturePage from "@/pages/FeatureManager/InfoFeaturePage";
import UpdateFeaturePage from "@/pages/FeatureManager/UpdateFeaturePage";
import PropertyPage from "@/pages/PropertyPage";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "/register", element: <RegisterPage /> },
      { path: "/login", element: <LoginPage /> },
      { path: "/properties", element: <PropertiesPage /> },
      { path: "/properties/:id", element: <PropertyPage /> },
      { path: "/server-error", element: <ServerErrorPage /> },
      {
        path: "/dashboard",
        element: <DashboardPage />,
        children: [
          { path: "/dashboard/properties", element: <PropertyListPage /> },
          { path: "/dashboard/properties/add", element: <AddPropertyPage /> },
          {
            path: "/dashboard/properties/information/:id",
            element: <InfoPropertyPage />,
          },
          {
            path: "/dashboard/properties/edit/:id",
            element: <UpdatePropertyPage />,
          },
          { path: "/dashboard/categories", element: <CategoryListPage /> },
          { path: "/dashboard/categories/add", element: <AddCategoryPage /> },
          {
            path: "/dashboard/categories/information/:id",
            element: <InfoCategoryPage />,
          },
          {
            path: "/dashboard/categories/edit/:id",
            element: <UpdateCategoryPage />,
          },
          { path: "/dashboard/features", element: <FeatureListPage /> },
          { path: "/dashboard/features/add", element: <AddFeaturePage /> },
          {
            path: "/dashboard/features/information/:id",
            element: <InfoFeaturePage />,
          },
          {
            path: "/dashboard/features/edit/:id",
            element: <UpdateFeaturePage />,
          },
        ],
      },
    ],
  },
]);
