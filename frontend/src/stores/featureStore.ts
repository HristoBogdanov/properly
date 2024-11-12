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
  getFeatures: () => Promise<void>;
  addFeature: (feature: CreateFeature) => Promise<void>;
  updateFeature: (feature: CreateFeature, id: string) => Promise<void>;
  removeFeature: (id: string) => Promise<void>;
  loading: boolean;
};

export const useFeaturesStore = create<FeaturesStore>((set) => ({
  features: [],
  loading: true,

  getFeatures: async () => {
    try {
      const response = await getFeatures();
      if (response) {
        set({ features: response.data });
      }
    } catch (error) {
      handleError(error, "Error getting features");
    } finally {
      set({ loading: false });
    }
  },

  addFeature: async (feature: CreateFeature) => {
    try {
      const response = await addFeature(feature);
      if (response.data) {
        set((state) => ({
          features: [...state.features, feature],
        }));
      }
    } catch (error) {
      handleError(error, "Error adding feature");
    }
  },

  updateFeature: async (feature: CreateFeature, id: string) => {
    try {
      const response = await updateFeature(feature, id);
      if (response.data) {
        set((state) => ({
          features: state.features.map((f) =>
            f.id === id ? { ...f, ...feature } : f
          ),
        }));
      }
    } catch (error) {
      handleError(error, "Error updating feature");
    }
  },

  removeFeature: async (id: string) => {
    try {
      const response = await removeFeature(id);
      if (response.data) {
        set((state) => ({
          features: state.features.filter((f) => f.id !== id),
        }));
      }
    } catch (error) {
      handleError(error, "Error removing feature");
    }
  },
}));

// Initialize the store by calling getFeatures
useFeaturesStore.getState().getFeatures();
