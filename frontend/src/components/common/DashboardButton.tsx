import { useAuth } from "@/contexts/useAuth";
import { FaHouseUser } from "react-icons/fa";
import { Link } from "react-router-dom";

export default function DashboardButton({ onClick } = { onClick: () => {} }) {
  const { isUserAdmin } = useAuth();
  return (
    <div onClick={onClick}>
      {isUserAdmin() ? (
        <Link
          className="flex justify-center items-center gap-2 bg-primary text-white px-5 py-2 rounded-lg border-2 border-primary hover:bg-white hover:text-primary transition-all duration-300 ease-in-out"
          to="/dashboard/properties"
        >
          <FaHouseUser className="text-3xl" />
          Dashboard
        </Link>
      ) : null}
    </div>
  );
}
