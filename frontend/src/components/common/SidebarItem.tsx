import { ReactNode } from "react";
import { SidebarMenuButton, SidebarMenuItem } from "../ui/sidebar";
import { Link, useLocation } from "react-router-dom";

export default function SidebarItem({
  link,
  children,
}: {
  link: string;
  children: ReactNode;
}) {
  const { pathname } = useLocation();

  const isTabActive = () => {
    return pathname.includes(link);
  };

  return (
    <Link
      className={`${
        isTabActive() ? "text-primary" : ""
      } hover:text-primary hover:bg-gray-700 rounded-md transition-all duration-300 ease-in-out flex w-full gap-2`}
      to={link}
    >
      <SidebarMenuItem>
        <SidebarMenuButton className="text-lg lg:text-xl">
          {children}
        </SidebarMenuButton>
      </SidebarMenuItem>
    </Link>
  );
}
