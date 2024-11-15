import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import {
  addProperty,
  getProperties,
  updateProperty,
  removeProperty,
} from "@/api/properties";
import {
  CreateProperty,
  Property,
  PropertyPages,
  PropertyQueryParams,
} from "@/types/property";

type PropertiesStore = {
  properties: Property[];
  getProperties: (params?: PropertyQueryParams) => Promise<Property[]>;
  addProperty: (property: CreateProperty) => Promise<void>;
  updateProperty: (property: CreateProperty, id: string) => Promise<void>;
  removeProperty: (id: string) => Promise<void>;
  loading: boolean;
  pages: PropertyPages;
};

export const usePropertiesStore = create<PropertiesStore>((set) => ({
  properties: [],
  loading: true,
  total: 0,
  pages: {
    page: 1,
    perPage: 10,
    totalPages: 1,
  },

  getProperties: async (params?: PropertyQueryParams) => {
    try {
      const response = await getProperties(params);
      if (response) {
        set({ properties: response.properties });
        set({ pages: response.pages });
        return response.properties;
      } else {
        return [];
      }
    } catch (error) {
      handleError(error, "Error getting properties");
      return [];
    } finally {
      set({ loading: false });
    }
  },

  addProperty: async (property: CreateProperty) => {
    try {
      const response = await addProperty(property);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
      }
    } catch (error) {
      handleError(error, "Error adding property");
    }
  },

  updateProperty: async (property: CreateProperty, id: string) => {
    try {
      const response = await updateProperty(property, id);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
      }
    } catch (error) {
      handleError(error, "Error updating property");
    }
  },

  removeProperty: async (id: string) => {
    try {
      const response = await removeProperty(id);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
      }
    } catch (error) {
      handleError(error, "Error removing property");
    }
  },
}));

// Initialize the store by calling getProperties
usePropertiesStore.getState().getProperties();
