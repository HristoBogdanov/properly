import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateImage, Image } from "@/types/image";
import { getImages, addImage, updateImage, removeImage } from "@/api/images";

type ImagesStore = {
  images: Image[];
  getImages: () => Promise<Image[]>;
  addImage: (image: CreateImage) => Promise<void>;
  updateImage: (image: CreateImage, id: string) => Promise<void>;
  removeImage: (id: string) => Promise<void>;
  loading: boolean;
  total: number;
};

export const useImagesStore = create<ImagesStore>((set) => ({
  images: [],
  loading: true,
  total: 0,

  getImages: async () => {
    try {
      const response = await getImages();
      if (response) {
        set({ images: response.data });
        set({ total: response.data.length });
        return response.data;
      } else {
        return [];
      }
    } catch (error) {
      handleError(error, "Error getting images");
      return [];
    } finally {
      set({ loading: false });
    }
  },

  addImage: async (image: CreateImage) => {
    try {
      const response = await addImage(image);
      if (response?.data) {
        await useImagesStore.getState().getImages();
      }
    } catch (error) {
      handleError(error, "Error adding image");
    }
  },

  updateImage: async (image: CreateImage, id: string) => {
    try {
      const response = await updateImage(image, id);
      if (response?.data) {
        await useImagesStore.getState().getImages();
      }
    } catch (error) {
      handleError(error, "Error updating image");
    }
  },

  removeImage: async (id: string) => {
    try {
      const response = await removeImage(id);
      if (response?.data) {
        await useImagesStore.getState().getImages();
      }
    } catch (error) {
      handleError(error, "Error removing image");
    }
  },
}));

// Initialize the store by calling getImages
useImagesStore.getState().getImages();
