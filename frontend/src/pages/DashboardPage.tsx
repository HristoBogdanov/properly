import PropertyList from "@/components/dashboard/PropertyList";
import {
  Sidebar,
  SidebarContent,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuItem,
  SidebarMenuButton,
  SidebarTrigger,
} from "@/components/ui/sidebar";
import { FaHome, FaUser, FaWifi, FaImage } from "react-icons/fa";
import { TbCategoryPlus } from "react-icons/tb";

export default function DashboardPage() {
  return (
    <div className="container mx-auto mt-20 h-fit flex justify-center items-center gap-10">
      <Sidebar
        side="left"
        className="w-64 bg-gray-800 text-white z-50 border-none"
      >
        <SidebarHeader className="relative p-4 max-sm:bg-gray-700 max-sm:text-white">
          <h1 className="text-2xl font-bold">Dashboard</h1>
          <SidebarTrigger className="absolute top-4 -right-7 bg-gray-800 scale-150 hover:text-primary hover:bg-gray-700" />
        </SidebarHeader>
        <SidebarContent className="flex-1 p-4 max-sm:bg-gray-700 max-sm:text-white">
          <SidebarMenu className="gap-4">
            <SidebarMenuItem className="hover:text-primary transition-all duration-300 ease-in-out">
              <SidebarMenuButton className="text-lg lg:text-xl">
                <FaHome className="mr-2" />
                Properties
              </SidebarMenuButton>
            </SidebarMenuItem>
            <SidebarMenuItem className="hover:text-primary transition-all duration-300 ease-in-out">
              <SidebarMenuButton className="text-lg lg:text-xl">
                <TbCategoryPlus className="mr-2" />
                Categories
              </SidebarMenuButton>
            </SidebarMenuItem>
            <SidebarMenuItem className="hover:text-primary transition-all duration-300 ease-in-out">
              <SidebarMenuButton className="text-lg lg:text-xl">
                <FaWifi className="mr-2" />
                Features
              </SidebarMenuButton>
            </SidebarMenuItem>
            <SidebarMenuItem className="hover:text-primary transition-all duration-300 ease-in-out">
              <SidebarMenuButton className="text-lg lg:text-xl">
                <FaImage className="mr-2" />
                Images
              </SidebarMenuButton>
            </SidebarMenuItem>
            <SidebarMenuItem className="hover:text-primary transition-all duration-300 ease-in-out">
              <SidebarMenuButton className="text-lg lg:text-xl">
                <FaUser className="mr-2" />
                Users
              </SidebarMenuButton>
            </SidebarMenuItem>
          </SidebarMenu>
        </SidebarContent>
      </Sidebar>
      <div className="flex-1 w-full">
        <PropertyList />
      </div>
    </div>
  );
}
