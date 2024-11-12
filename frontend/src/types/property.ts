import { SimpleCategory } from "./category";
import { Feature } from "./feature";
import { CreateImage } from "./image";

export type SimpleProperty = {
  id: string;
  title: string;
  description: string;
  address: string;
  price: number;
  forSale: boolean;
  forRent: boolean;
  bedrooms: number;
  bathrooms: number;
  isFurnished: boolean;
  area: number;
  yearOfConstruction: number;
  ownerId: string;
};

export type Property = SimpleProperty & {
  categories: SimpleCategory[];
  features: Feature[];
  images: CreateImage[];
};
