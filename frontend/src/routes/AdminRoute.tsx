import { useAuth } from "@/contexts/useAuth";
import NotFoundPage from "@/pages/NotFoundPage";

type Props = { children: React.ReactNode };

export default function AdminRoute({ children }: Props) {
  const { isUserAdmin } = useAuth();
  return isUserAdmin() ? <>{children}</> : <NotFoundPage />;
}
