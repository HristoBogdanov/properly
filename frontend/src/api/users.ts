import { handleError } from "@/helpers/ErrorHandler";
import { User } from "@/types/user";
import API from "./API";

export const getUsers = async () => {
  try {
    const data = await API.get<User[]>(
      import.meta.env.VITE_API_URL + "account/users"
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting users");
  }
};

export const getUserById = async (id: string) => {
  try {
    const data = await API.get<User>(
      import.meta.env.VITE_API_URL + `account/users/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting user");
  }
};

export const deleteUser = async (id: string) => {
  try {
    const data = await API.delete<boolean>(
      import.meta.env.VITE_API_URL + `account/users/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error deleting user");
  }
};
