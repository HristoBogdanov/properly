import { useAuth } from "@/contexts/useAuth";
import { Navigate, useLocation } from "react-router-dom";

type Props = { children: React.ReactNode };

export default function AdminRoute({ children }: Props) {
  const location = useLocation();
  const { isUserAdmin } = useAuth();
  return isUserAdmin() ? (
    <>{children}</>
  ) : (
    <Navigate to="/not-found" state={{ from: location }} replace />
  );
}
