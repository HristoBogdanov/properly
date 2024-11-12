import { handleError } from "@/helpers/ErrorHandler";
import { CreateFeature, Feature } from "@/types/feature";
import axios from "axios";

export const getFeatures = async () => {
  try {
    const data = await axios.get<Feature[]>(
      import.meta.env.VITE_API_URL + "features/all"
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting features");
  }
};

export const addFeature = async (feature: CreateFeature) => {
  try {
    const data = await axios.post<boolean>(
      import.meta.env.VITE_API_URL + "features/add",
      feature
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding feature");
  }
};

export const updateFeature = async (feature: CreateFeature, id: string) => {
  try {
    const data = await axios.put<boolean>(
      import.meta.env.VITE_API_URL + `features/update/${id}`,
      feature
    );
    return data;
  } catch (error) {
    handleError(error, "Error updating feature");
  }
};

export const removeFeature = async (id: string) => {
  try {
    const data = await axios.delete<boolean>(
      import.meta.env.VITE_API_URL + `features/delete/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error removing feature");
  }
};
