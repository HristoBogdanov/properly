import { navbarItems } from "@/lib/navbarItems";
import { Link, useLocation } from "react-router-dom";
import DashboardButton from "./DashboardButton";
import LoginRegisterButtons from "./LoginRegisterButtons";
import MobileMenu from "./MobileMenu";

export default function Navbar() {
  const location = useLocation();
  const currentLocation = location.pathname;

  return (
    <nav
      className={`${
        currentLocation === "/" ? "bg-transparent" : "bg-[#9A9A94]"
      } w-[100vw] absolute left-1/2 top-0 -translate-x-1/2 text-white h-fit py-10 lg:py-16 px-5 md:px-10 z-40`}
    >
      <div className="absolute inset-0 container my-10 mx-auto flex justify-between items-center text-lg uppercase">
        <div className="w-fit hidden lg:flex gap-10 justify-center items-center">
          <Link to={"/"}>
            <img
              src="/images/logo.svg"
              className="w-[60px] apsect-square"
            ></img>
          </Link>
          {navbarItems.map((item, index) => (
            <Link key={index} to={item.url} className="link">
              {item.title}
            </Link>
          ))}
        </div>
        <div className="w-fit hidden lg:flex gap-10 justify-center items-center">
          <LoginRegisterButtons />
          <DashboardButton />
        </div>
      </div>
      <MobileMenu />
    </nav>
  );
}
