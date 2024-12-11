import { setGlobalNavigate } from "@/api/API";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function NavigatorInitializer() {
  const navigate = useNavigate();

  useEffect(() => {
    setGlobalNavigate(navigate);
  }, [navigate]);

  return null;
}
