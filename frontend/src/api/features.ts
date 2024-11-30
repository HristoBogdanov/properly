import { handleError } from "@/helpers/ErrorHandler";
import { CreateFeature, Feature } from "@/types/feature";
import API from "./API";

export const getFeatures = async () => {
  try {
    const data = await API.get<Feature[]>(
      import.meta.env.VITE_API_URL + "features/all"
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting features");
  }
};

export const getFeatureById = async (id: string) => {
  try {
    const data = await API.get<Feature>(
      import.meta.env.VITE_API_URL + `features/all/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting feature");
  }
};

export const addFeature = async (feature: CreateFeature) => {
  try {
    const data = await API.post<Feature>(
      import.meta.env.VITE_API_URL + "features/create",
      feature
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding feature");
  }
};

export const updateFeature = async (feature: CreateFeature, id: string) => {
  try {
    const data = await API.put<boolean>(
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
    const data = await API.post<boolean>(
      import.meta.env.VITE_API_URL + `features/delete/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error removing feature");
  }
};
