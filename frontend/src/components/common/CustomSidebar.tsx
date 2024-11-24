import {
  Sidebar,
  SidebarContent,
  SidebarHeader,
  SidebarMenu,
  SidebarTrigger,
} from "@/components/ui/sidebar";
import { FaHome, FaWifi, FaImage } from "react-icons/fa";
import { TbCategoryPlus } from "react-icons/tb";
import SidebarItem from "./SidebarItem";

const items = [
  {
    content: (
      <>
        <FaHome />
        Properties
      </>
    ),
    link: "/dashboard",
  },
  {
    content: (
      <>
        <TbCategoryPlus />
        Categories
      </>
    ),
    link: "/dashboard/categories",
  },
  {
    content: (
      <>
        <FaWifi />
        Features
      </>
    ),
    link: "/dashboard/features",
  },
  {
    content: (
      <>
        <FaImage />
        Images
      </>
    ),
    link: "/dashboard/images",
  },
];

export default function CustomSidebar() {
  return (
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
          {items.map((item) => (
            <SidebarItem link={item.link}>{item.content}</SidebarItem>
          ))}
        </SidebarMenu>
      </SidebarContent>
    </Sidebar>
  );
}
