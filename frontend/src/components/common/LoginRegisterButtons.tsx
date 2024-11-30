import { useAuth } from "@/contexts/useAuth";
import { Link } from "react-router-dom";

export default function LoginRegisterButtons(
  { onClick } = { onClick: () => {} }
) {
  const { isLoggedIn, logout } = useAuth();

  return (
    <div onClick={onClick}>
      {isLoggedIn() ? (
        <p className="link cursor-pointer" onClick={logout}>
          Log out
        </p>
      ) : (
        <div className="flex gap-3 max-lg:border-b-2 max-lg:border-primary max-lg:hover:border-b-white transition-all duration-300">
          <Link className="link max-lg:hover:text-white" to="/login">
            Login
          </Link>
          <p>|</p>
          <Link className="link max-lg:hover:text-white" to="/register">
            Sign up
          </Link>
        </div>
      )}
    </div>
  );
}
