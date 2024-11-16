import { deleteUser, getUserById, getUsers } from "@/api/users";
import { handleError } from "@/helpers/ErrorHandler";
import { User } from "@/types/user";
import { create } from "zustand";

type UsersStore = {
  users: User[];
  getUsers: () => Promise<User[]>;
  getUserById: (id: string) => Promise<User>;
  removeUser: (id: string) => Promise<boolean>;
  loading: boolean;
  total: number;
};

export const useUsersStore = create<UsersStore>((set) => ({
  users: [],
  loading: true,
  total: 0,

  getUsers: async () => {
    set({ loading: true });
    try {
      const response = await getUsers();
      if (response) {
        set({ users: response.data });
        set({ total: response.data.length });
        return response.data;
      } else {
        return [];
      }
    } catch (error) {
      handleError(error, "Error getting users");
      return [];
    } finally {
      set({ loading: false });
    }
  },

  getUserById: async (id: string) => {
    set({ loading: true });
    try {
      const response = await getUserById(id);
      if (response) {
        return response.data;
      } else {
        return {} as User;
      }
    } catch (error) {
      handleError(error, "Error getting user");
      return {} as User;
    } finally {
      set({ loading: false });
    }
  },

  removeUser: async (id: string) => {
    set({ loading: true });
    try {
      const response = await deleteUser(id);
      if (response?.data) {
        await useUsersStore.getState().getUsers();
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error removing user");
      return false;
    } finally {
      set({ loading: false });
    }
  },
}));
