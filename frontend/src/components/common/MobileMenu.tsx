import { Link } from "react-router-dom";
import { useState } from "react";
import { navbarItems } from "@/lib/navbarItems";
import { RxHamburgerMenu } from "react-icons/rx";
import { FaHouseUser } from "react-icons/fa";

export default function MobileMenu() {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <>
      <div className="lg:hidden w-full flex justify-between items-center">
        <Link to={"/"}>
          <img src="/images/logo.svg" className="w-[60px] apsect-square"></img>
        </Link>
        <RxHamburgerMenu
          className="text-5xl z-10 cursor-pointer link"
          onClick={() => setIsOpen(!isOpen)}
        />
      </div>
      <div
        className={`${
          isOpen
            ? "translate-x-0 opacity-100 w-[100vw] sm:w-[70vw] max-w-[400px] p-5 gap-5 border-2"
            : "translate-x-[100%] opacity-0 w-0 p-0 gap-0 border-0"
        } h-fit lg:hidden overflow-hidden transition-all duration-300 ease-in-out flex flex-col fixed top-[134px] right-0 bg-primary text-white text-lg uppercase border-primary`}
      >
        {navbarItems.map((item, index) => (
          <Link
            key={index}
            className="border-b-2 border-primary hover:border-b-white transition-all duration-300"
            to={item.url}
          >
            {item.title}
          </Link>
        ))}
        <div className="flex gap-2">
          <Link
            to="/login"
            className="border-b-2 border-primary hover:border-b-white transition-all duration-300"
          >
            Login
          </Link>
          <p>|</p>
          <Link
            to="/register"
            className="border-b-2 border-primary hover:border-b-white transition-all duration-300"
          >
            Register
          </Link>
        </div>
        <Link
          className="flex justify-center items-center gap-2 bg-white text-primary px-5 py-2 rounded-lg hover:border-white hover:bg-primary hover:text-white border-2 border-primary transition-all duration-300 ease-in-out"
          to="/dashboard"
        >
          <FaHouseUser className="text-3xl" />
          Dashboard
        </Link>
      </div>
    </>
  );
}
