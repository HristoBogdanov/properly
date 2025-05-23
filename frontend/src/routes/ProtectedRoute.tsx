import { useAuth } from "@/contexts/useAuth";
import { Navigate, useLocation } from "react-router-dom";

type Props = { children: React.ReactNode };

export default function ProtectedRoute({ children }: Props) {
  const location = useLocation();
  const { isLoggedIn } = useAuth();
  return isLoggedIn() ? (
    <>{children}</>
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
}
