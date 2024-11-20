import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateCategory, Category } from "@/types/category";
import {
  getCategories,
  addCategory,
  updateCategory,
  removeCategory,
} from "@/api/categories";

type CategoriesStore = {
  categories: Category[];
  getCategories: () => Promise<Category[]>;
  addCategory: (category: CreateCategory) => Promise<boolean>;
  updateCategory: (category: CreateCategory, id: string) => Promise<boolean>;
  removeCategory: (id: string) => Promise<boolean>;
  loading: boolean;
  total: number;
};

export const useCategoriesStore = create<CategoriesStore>((set) => ({
  categories: [],
  loading: true,
  total: 0,

  getCategories: async () => {
    set({ loading: true });
    try {
      const response = await getCategories();
      if (response) {
        set({ categories: response.data });
        set({ total: response.data.length });
        return response.data;
      } else {
        return [];
      }
    } catch (error) {
      handleError(error, "Error getting categories");
      return [];
    } finally {
      set({ loading: false });
    }
  },

  addCategory: async (category: CreateCategory) => {
    set({ loading: true });
    try {
      const response = await addCategory(category);
      await useCategoriesStore.getState().getCategories();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error adding category");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  updateCategory: async (category: CreateCategory, id: string) => {
    set({ loading: true });
    try {
      const response = await updateCategory(category, id);
      await useCategoriesStore.getState().getCategories();
      if (response?.status === 200) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error updating category");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  removeCategory: async (id: string) => {
    set({ loading: true });
    try {
      const response = await removeCategory(id);
      await useCategoriesStore.getState().getCategories();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error removing category");
      return false;
    } finally {
      set({ loading: false });
    }
  },
}));

// Initialize the store by calling getCategories
useCategoriesStore.getState().getCategories();
