import { handleError } from "@/helpers/ErrorHandler";
import { CreateImage } from "@/types/image";
import {
  CreateProperty,
  Property,
  PropertyPages,
  PropertyPagesResult,
  PropertyQueryParams,
} from "@/types/property";
import axios from "axios";

export const getProperties = async (params?: PropertyQueryParams) => {
  try {
    const query = constructQuery(params);
    const result = await axios.get<PropertyPagesResult>(
      import.meta.env.VITE_API_URL + `properties/all?${query}`
    );
    if (result) {
      const pages: PropertyPages = result.data.pages;
      const properties: Property[] = result.data.properties;
      return { pages, properties };
    }
  } catch (error) {
    handleError(error, "Error getting properties");
  }
};

export const getPropertyById = async (id: string) => {
  try {
    const data = await axios.get<Property>(
      import.meta.env.VITE_API_URL + `properties/all/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting property");
  }
};

export const addProperty = async (property: CreateProperty) => {
  try {
    const data = await axios.post<Property>(
      import.meta.env.VITE_API_URL + "properties/create",
      property
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding property");
  }
};

export const addImageToProperty = async (id: string, image: CreateImage) => {
  try {
    const data = await axios.post<boolean>(
      import.meta.env.VITE_API_URL + `properties/add-image/${id}`,
      image
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding image to property");
  }
};

export const updateProperty = async (property: CreateProperty, id: string) => {
  try {
    const data = await axios.put<boolean>(
      import.meta.env.VITE_API_URL + `properties/update/${id}`,
      property
    );
    return data;
  } catch (error) {
    handleError(error, "Error updating property");
  }
};

export const removeProperty = async (id: string) => {
  try {
    const data = await axios.post<boolean>(
      import.meta.env.VITE_API_URL + `properties/delete/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error removing property");
  }
};

function constructQuery(params?: PropertyQueryParams) {
  let query = "";
  if (!params) return query;
  for (const [key, value] of Object.entries(params)) {
    query += `${key}=${value}&`;
  }
  return query;
}
