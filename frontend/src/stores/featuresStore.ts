import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateFeature, Feature } from "@/types/feature";
import {
  getFeatures,
  addFeature,
  updateFeature,
  removeFeature,
} from "@/api/features";

type FeaturesStore = {
  features: Feature[];
  getFeatures: () => Promise<Feature[]>;
  addFeature: (feature: CreateFeature) => Promise<boolean>;
  updateFeature: (feature: CreateFeature, id: string) => Promise<boolean>;
  removeFeature: (id: string) => Promise<boolean>;
  loading: boolean;
  total: number;
};

export const useFeaturesStore = create<FeaturesStore>((set) => ({
  features: [],
  loading: true,
  total: 0,

  getFeatures: async () => {
    set({ loading: true });
    try {
      const response = await getFeatures();
      if (response) {
        set({ features: response.data });
        set({ total: response.data.length });
        return response.data;
      } else {
        return [];
      }
    } catch (error) {
      handleError(error, "Error getting features");
      return [];
    } finally {
      set({ loading: false });
    }
  },

  addFeature: async (feature: CreateFeature) => {
    set({ loading: true });
    try {
      const response = await addFeature(feature);
      await useFeaturesStore.getState().getFeatures();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error adding feature");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  updateFeature: async (feature: CreateFeature, id: string) => {
    set({ loading: true });
    try {
      const response = await updateFeature(feature, id);
      await useFeaturesStore.getState().getFeatures();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error updating feature");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  removeFeature: async (id: string) => {
    set({ loading: true });
    try {
      const response = await removeFeature(id);
      await useFeaturesStore.getState().getFeatures();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error removing feature");
      return false;
    } finally {
      set({ loading: false });
    }
  },
}));

// Initialize the store by calling getFeatures
useFeaturesStore.getState().getFeatures();
