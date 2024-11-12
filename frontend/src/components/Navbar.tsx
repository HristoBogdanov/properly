import { Link } from "react-router-dom";
import { FaHouseUser } from "react-icons/fa";
import MobileMenu from "./MobileMenu";

export default function Navbar() {
  return (
    <nav className="w-[100vw] absolute overflow-hidden left-1/2 -translate-x-1/2 text-white h-fit py-10 lg:py-16 px-5 md:px-10 z-40">
      <div className="absolute w-full h-full inset-0 py-10 opacity-30 bg-[#8A8D8E]"></div>
      <div className="absolute inset-0 container my-10 mx-auto flex justify-between items-center text-lg uppercase">
        <div className="w-fit hidden lg:flex gap-10 justify-center items-center">
          <Link to={"/"}>
            <img
              src="/images/logo.svg"
              className="w-[60px] apsect-square"
            ></img>
          </Link>
          <Link className="link" to="/">
            Home
          </Link>
          <Link className="link" to="/properties">
            Properties
          </Link>
          <Link className="link" to="/properties?forRent=true">
            For Rent
          </Link>
          <Link className="link" to="/properties?forSale=true">
            For Sale
          </Link>
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
