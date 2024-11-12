import { Image } from "./image";

export type Feature = {
  id: string;
  title: string;
  image: Image;
};

export type CreateFeature = {
  title: string;
  image: Image;
};
