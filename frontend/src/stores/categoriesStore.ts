import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateCategory, Category } from "@/types/category";
import {
  getCategories,
  addCategory,
  updateCategory,
  removeCategory,
} from "@/api/categories";

type CategoryStore = {
  categories: Category[];
  getCategories: () => Promise<Category[]>;
  addCategory: (category: CreateCategory) => Promise<void>;
  updateCategory: (category: CreateCategory, id: string) => Promise<void>;
  removeCategory: (id: string) => Promise<void>;
  loading: boolean;
};

export const useCategoryStore = create<CategoryStore>((set) => ({
  categories: [],
  loading: true,

  getCategories: async () => {
    try {
      const response = await getCategories();
      if (response) {
        set({ categories: response.data });
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
    try {
      const response = await addCategory(category);
      if (response?.data) {
        await useCategoryStore.getState().getCategories();
      }
    } catch (error) {
      handleError(error, "Error adding category");
    }
  },

  updateCategory: async (category: CreateCategory, id: string) => {
    try {
      const response = await updateCategory(category, id);
      if (response?.data) {
        await useCategoryStore.getState().getCategories();
      }
    } catch (error) {
      handleError(error, "Error updating category");
    }
  },

  removeCategory: async (id: string) => {
    try {
      const response = await removeCategory(id);
      if (response?.data) {
        await useCategoryStore.getState().getCategories();
      }
    } catch (error) {
      handleError(error, "Error removing category");
    }
  },
}));

// Initialize the store by calling getCategories
useCategoryStore.getState().getCategories();
