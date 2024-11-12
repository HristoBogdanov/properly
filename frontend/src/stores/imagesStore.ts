import { create } from "zustand";
import { handleError } from "@/helpers/ErrorHandler";
import { CreateImage, Image } from "@/types/image";
import { getImages, addImage, updateImage, removeImage } from "@/api/images";

type ImagesStore = {
  images: Image[];
  getImages: () => Promise<void>;
  addImage: (image: CreateImage) => Promise<void>;
  updateImage: (image: CreateImage, id: string) => Promise<void>;
  removeImage: (id: string) => Promise<void>;
  loading: boolean;
};

export const useImagesStore = create<ImagesStore>((set) => ({
  images: [],
  loading: true,

  getImages: async () => {
    try {
      const response = await getImages();
      if (response) {
        set({ images: response.data });
      }
    } catch (error) {
      handleError(error, "Error getting images");
    } finally {
      set({ loading: false });
    }
  },

  addImage: async (image: CreateImage) => {
    try {
      const response = await addImage(image);
      const newImage: Image = { ...image, id: response.data.id };
      if (response?.data) {
        set((state) => ({
          images: [...state.images, [...newImage]],
        }));
      }
    } catch (error) {
      handleError(error, "Error adding image");
    }
  },

  updateImage: async (image: CreateImage, id: string) => {
    try {
      const response = await updateImage(image, id);
      if (response?.data) {
        set((state) => ({
          images: state.images.map((i) =>
            i.id === id ? { ...i, ...image } : i
          ),
        }));
      }
    } catch (error) {
      handleError(error, "Error updating image");
    }
  },

  removeImage: async (id: string) => {
    try {
      const response = await removeImage(id);
      if (response?.data) {
        set((state) => ({
          images: state.images.filter((i) => i.id !== id),
        }));
      }
    } catch (error) {
      handleError(error, "Error removing image");
    }
  },
}));

// Initialize the store by calling getImages
useImagesStore.getState().getImages();
