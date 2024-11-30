import { handleError } from "@/helpers/ErrorHandler";
import { CreateImage, Image } from "@/types/image";
import API from "./API";

export const getImages = async () => {
  try {
    const data = await API.get<Image[]>(
      import.meta.env.VITE_API_URL + "images/all"
    );
    return data;
  } catch (error) {
    handleError(error, "Error getting images");
  }
};

export const addImage = async (image: CreateImage) => {
  try {
    const data = await API.post<Image>(
      import.meta.env.VITE_API_URL + "images/create",
      image
    );
    return data;
  } catch (error) {
    handleError(error, "Error adding image");
  }
};

export const updateImage = async (image: CreateImage, id: string) => {
  try {
    const data = await API.put<boolean>(
      import.meta.env.VITE_API_URL + `images/update/${id}`,
      image
    );
    return data;
  } catch (error) {
    handleError(error, "Error updating image");
  }
};

export const removeImage = async (id: string) => {
  try {
    const data = await API.delete<boolean>(
      import.meta.env.VITE_API_URL + `images/delete/${id}`
    );
    return data;
  } catch (error) {
    handleError(error, "Error removing image");
  }
};
