import axios from "axios";
import { toast } from "react-toastify";

export const handleError = (error: unknown, message: string) => {
  if (axios.isAxiosError(error)) {
    toast.error(
      `${message}: ${
        error.response?.data || error.response?.data?.message || error.message
      }`
    );
  } else {
    toast.error(message);
  }
  console.error(message, error);
};
