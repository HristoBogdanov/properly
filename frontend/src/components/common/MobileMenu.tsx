import { navbarItems } from "@/lib/navbarItems";
import { useState } from "react";
import { RxHamburgerMenu } from "react-icons/rx";
import { Link } from "react-router-dom";
import DashboardButton from "./DashboardButton";
import LoginRegisterButtons from "./LoginRegisterButtons";

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
          isOpen ? "translate-x-0" : "translate-x-[100%]"
        } h-fit w-[100vw] sm:w-[70vw] max-w-[400px] p-5 gap-5 border-2 lg:hidden overflow-hidden transition-all duration-300 ease-in-out flex flex-col fixed top-[134px] right-0 bg-primary text-white text-lg uppercase border-primary`}
      >
        {navbarItems.map((item, index) => (
          <Link
            key={index}
            className="border-b-2 border-primary hover:border-b-white transition-all duration-300"
            to={item.url}
            onClick={() => setIsOpen(false)}
          >
            {item.title}
          </Link>
        ))}
        <LoginRegisterButtons onClick={() => setIsOpen(!isOpen)} />
        <DashboardButton onClick={() => setIsOpen(!isOpen)} />
      </div>
    </>
  );
}
