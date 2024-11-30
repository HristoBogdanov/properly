import { handleError } from "@/helpers/ErrorHandler";
import { CreateUser, LoginUser, UserProfileToken } from "@/types/user";
import API from "./API";

export const loginAPI = async (loginUser: LoginUser) => {
  try {
    const data = await API.post<UserProfileToken>(
      import.meta.env.VITE_API_URL + "account/login",
      loginUser
    );
    return data;
  } catch (error) {
    handleError(error, "Error logging in user");
  }
};

export const registerAPI = async (createUser: CreateUser) => {
  try {
    const data = await API.post<UserProfileToken>(
      import.meta.env.VITE_API_URL + "account/register",
      createUser
    );
    return data;
  } catch (error) {
    handleError(error, "Error registering user");
  }
};
