import { handleError } from "@/helpers/ErrorHandler";
import { Category, CreateCategory } from "@/types/category";
import axios from "axios";

export const getCategories = async () => {
  try {
    const data = await axios.get<Category[]>(
      import.meta.env.VITE_API_URL + "categories/all"
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting categories");
  }
};

export const addCategory = async (category: CreateCategory) => {
  try {
    const data = await axios.post<Category>(
      import.meta.env.VITE_API_URL + "categories/create",
      category
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding category");
  }
};

export const updateCategory = async (category: CreateCategory, id: string) => {
  try {
    const data = await axios.put<boolean>(
      import.meta.env.VITE_API_URL + `categories/update/${id}`,
      category
    );
    return data;
  } catch (error) {
    handleError(error, "Error updating category");
  }
};

export const removeCategory = async (id: string) => {
  try {
    const data = await axios.post<boolean>(
      import.meta.env.VITE_API_URL + `categories/delete/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error removing category");
  }
};
