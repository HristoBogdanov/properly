import { CreateImage } from "./image";

export type Feature = {
  id: string;
  title: string;
  image: CreateImage;
};

export type CreateFeature = {
  title: string;
  image: CreateImage;
};
