import { Link, useLocation } from "react-router-dom";
import { FaHouseUser } from "react-icons/fa";
import MobileMenu from "./MobileMenu";
import { navbarItems } from "@/lib/navbarItems";

export default function Navbar() {
  const location = useLocation();
  const currentLocation = location.pathname;

  return (
    <nav
      className={`${
        currentLocation === "/" ? "bg-transparent" : "bg-[#9A9A94]"
      } w-[100vw] absolute left-1/2 -translate-x-1/2 text-white h-fit py-10 lg:py-16 px-5 md:px-10 z-40`}
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
          <div className="flex gap-3">
            <Link className="link" to="/login">
              Login
            </Link>
            <p>|</p>
            <Link className="link" to="/register">
              Sign up
            </Link>
          </div>
          <Link
            className="flex justify-center items-center gap-2 bg-primary text-white px-5 py-2 rounded-lg border-2 border-primary hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
            to="/add-listing"
          >
            <FaHouseUser className="text-3xl" />
            Add listing
          </Link>
        </div>
      </div>
      <MobileMenu />
    </nav>
  );
}
