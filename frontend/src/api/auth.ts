import axios from "axios";
import { UserProfileToken } from "@/types/user";
import { handleError } from "@/helpers/ErrorHandler";

export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(
      import.meta.env.VITE_API_URL + "account/login",
      {
        username: username,
        password: password,
      }
    );
    return data;
  } catch (error) {
    handleError(error, "Error logging in user");
  }
};

export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(
      import.meta.env.VITE_API_URL + "account/register",
      {
        email: email,
        username: username,
        password: password,
      }
    );
    return data;
  } catch (error) {
    handleError(error, "Error registering user");
  }
};
