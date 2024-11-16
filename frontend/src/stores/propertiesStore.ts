import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import {
  addProperty,
  getProperties,
  updateProperty,
  removeProperty,
  getPropertyById,
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
  getPropertyById: (id: string) => Promise<Property>;
  addProperty: (property: CreateProperty) => Promise<boolean>;
  updateProperty: (property: CreateProperty, id: string) => Promise<boolean>;
  removeProperty: (id: string) => Promise<boolean>;
  loading: boolean;
  pages: PropertyPages;
};

export const usePropertiesStore = create<PropertiesStore>((set) => ({
  properties: [],
  loading: true,
  pages: {
    page: 1,
    perPage: 10,
    totalPages: 1,
  },

  getProperties: async (params?: PropertyQueryParams) => {
    set({ loading: true });
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

  getPropertyById: async (id: string) => {
    set({ loading: true });
    try {
      const response = await getPropertyById(id);
      if (response) {
        return response.data;
      } else {
        return {} as Property;
      }
    } catch (error) {
      handleError(error, "Error getting property");
      return {} as Property;
    } finally {
      set({ loading: false });
    }
  },

  addProperty: async (property: CreateProperty) => {
    set({ loading: true });
    try {
      const response = await addProperty(property);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error adding property");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  updateProperty: async (property: CreateProperty, id: string) => {
    set({ loading: true });
    try {
      const response = await updateProperty(property, id);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error updating property");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  removeProperty: async (id: string) => {
    set({ loading: true });
    try {
      const response = await removeProperty(id);
      if (response?.data) {
        await usePropertiesStore.getState().getProperties();
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error removing property");
      return false;
    } finally {
      set({ loading: false });
    }
  },
}));

// Initialize the store by calling getProperties
usePropertiesStore.getState().getProperties();
