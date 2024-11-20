import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateImage, Image } from "@/types/image";
import { getImages, addImage, updateImage, removeImage } from "@/api/images";

type ImagesStore = {
  images: Image[];
  getImages: () => Promise<Image[]>;
  addImage: (image: CreateImage) => Promise<boolean>;
  updateImage: (image: CreateImage, id: string) => Promise<boolean>;
  removeImage: (id: string) => Promise<boolean>;
  loading: boolean;
  total: number;
};

export const useImagesStore = create<ImagesStore>((set) => ({
  images: [],
  loading: true,
  total: 0,

  getImages: async () => {
    set({ loading: true });
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
    set({ loading: true });
    try {
      const response = await addImage(image);
      await useImagesStore.getState().getImages();
      if (response?.status === 200) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error adding image");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  updateImage: async (image: CreateImage, id: string) => {
    set({ loading: true });
    try {
      const response = await updateImage(image, id);
      await useImagesStore.getState().getImages();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error updating image");
      return false;
    } finally {
      set({ loading: false });
    }
  },

  removeImage: async (id: string) => {
    set({ loading: true });
    try {
      const response = await removeImage(id);
      await useImagesStore.getState().getImages();
      if (response?.data) {
        return true;
      }
      return false;
    } catch (error) {
      handleError(error, "Error removing image");
      return false;
    } finally {
      set({ loading: false });
    }
  },
}));

// Initialize the store by calling getImages
useImagesStore.getState().getImages();
