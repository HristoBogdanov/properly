import AddCategoryPage from "@/pages/CategoryManager/AddCategoryPage";
import CategoryListPage from "@/pages/CategoryManager/CategoryListPage";
import InfoCategoryPage from "@/pages/CategoryManager/InfoCategoryPage";
import UpdateCategoryPage from "@/pages/CategoryManager/UpdateCategoryPage";
import DashboardPage from "@/pages/DashboardPage";
import AddFeaturePage from "@/pages/FeatureManager/AddFeaturePage";
import FeatureListPage from "@/pages/FeatureManager/FeatureListPage";
import InfoFeaturePage from "@/pages/FeatureManager/InfoFeaturePage";
import UpdateFeaturePage from "@/pages/FeatureManager/UpdateFeaturePage";
import HomePage from "@/pages/HomePage";
import LoginPage from "@/pages/LoginPage";
import NotFoundPage from "@/pages/NotFoundPage";
import PropertiesPage from "@/pages/PropertiesPage";
import AddPropertyPage from "@/pages/PropertyManager/AddPropertyPage";
import InfoPropertyPage from "@/pages/PropertyManager/InfoPropertyPage";
import PropertyListPage from "@/pages/PropertyManager/PropertyListPage";
import UpdatePropertyPage from "@/pages/PropertyManager/UpdatePropertyPage";
import PropertyPage from "@/pages/PropertyPage";
import RegisterPage from "@/pages/RegisterPage";
import ServerErrorPage from "@/pages/ServerErrorPage";
import { createBrowserRouter } from "react-router-dom";
import App from "../App";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "*", element: <NotFoundPage /> },
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
          { path: "", element: <PropertyListPage /> },
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
