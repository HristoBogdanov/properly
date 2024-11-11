import axios from "axios";
import { toast } from "react-toastify";

interface ErrorResponse {
  data?: {
    errors?: { description?: string }[] | Record<string, string[]>;
  };
  status?: number;
}

export const handleError = (error: unknown) => {
  if (axios.isAxiosError(error)) {
    const err = error.response as ErrorResponse;
    if (Array.isArray(err?.data?.errors)) {
      for (const val of err.data.errors) {
        toast.warning(val.description);
      }
    } else if (typeof err?.data?.errors === "object") {
      for (const e in err.data.errors) {
        toast.warning(err.data.errors[e][0]);
      }
    } else if (err?.data) {
      toast.warning(err.data.errors);
    } else if (err?.status === 401) {
      toast.warning("Please login");
      window.history.pushState({}, "LoginPage", "/login");
    } else if (err?.status === 400) {
      toast.warning("Bad request");
      window.history.pushState({}, "BadRequestPage", "/bad-request");
    } else if (err?.status === 500) {
      toast.warning("Server error occured");
      window.history.pushState({}, "ServerErrorPage", "/server-error");
    } else if (err) {
      toast.warning(err.data?.errors);
    }
  }
};
