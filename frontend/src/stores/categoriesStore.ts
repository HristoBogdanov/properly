import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { Category, CreateCategory } from "@/types/feature";
import {
  getCategories,
  addCategory,
  updateCategory,
  removeCategory,
} from "@/api/categories";

type CategoryStore = {
  categories: Category[];
  getCategories: () => Promise<void>;
  addCategory: (category: CreateCategory) => Promise<void>;
  updateCategory: (category: CreateCategory, id: string) => Promise<void>;
  removeCategory: (id: string) => Promise<void>;
};

export const useCategoriesStore = create<CategoryStore>((set) => ({
  categories: [],
  loading: true,

  getCategories: async () => {
    try {
      const response = await getCategories();
      if (response) {
        set({ categories: response.data });
      }
    } catch (error) {
      handleError(error, "Error getting categories");
    } finally {
      set({ loading: false });
    }
  },

  addCategory: async (category: CreateCategory) => {
    try {
      const response = await addCategory(category);
      const newCategory: Category = { ...category, id: response.data.id };
      if (response?.data) {
        set((state) => ({
          categories: [...state.categories, [...newCategory]],
        }));
      }
    } catch (error) {
      handleError(error, "Error adding category");
    }
  },

  updateCategory: async (category: CreateCategory, id: string) => {
    try {
      const response = await updateCategory(category, id);
      if (response?.data) {
        set((state) => ({
          categories: state.categories.map((c) =>
            c.id === id ? { ...c, ...category } : c
          ),
        }));
      }
    } catch (error) {
      handleError(error, "Error updating category");
    }
  },

  removeCategory: async (id: string) => {
    try {
      await removeCategory(id);
      set((state) => ({
        categories: state.categories.filter((c) => c.id !== id),
      }));
    } catch (error) {
      handleError(error, "Error removing category");
    }
  },
}));

// Initialize the store by calling getCategories
useCategoriesStore.getState().getCategories();
