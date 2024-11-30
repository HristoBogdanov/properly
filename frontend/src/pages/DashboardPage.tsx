import CustomSidebar from "@/components/common/CustomSidebar";
import { SidebarProvider } from "@/components/ui/sidebar";
import AdminRoute from "@/routes/AdminRoute";
import { Outlet } from "react-router-dom";

export default function DashboardPage() {
  return (
    <AdminRoute>
      <SidebarProvider>
        <div className="container mx-auto mt-20 h-fit flex justify-center items-center gap-10">
          <CustomSidebar />
          <div className="flex-1 w-full">
            <Outlet />
          </div>
        </div>
      </SidebarProvider>
    </AdminRoute>
  );
}
