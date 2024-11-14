import { SimpleCategory } from "./category";
import { Feature } from "./feature";
import { CreateImage } from "./image";

export type SimpleProperty = {
  id: string;
  title: string;
  description: string;
  address: string;
  price: number;
  createdAt: string;
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

export type CreateProperty = SimpleProperty & {
  categories: string[];
  features: string[];
  images: CreateImage[];
}

export type PropertyQueryParams = {
  search?: string;
  minPrice?: string;
  maxPrice?: string;
  numberOfBedrooms?: string;
  numberOfBathrooms?: string;
  minArea?: string;
  maxArea?: string;
  minYearOfConstruction?: string;
  maxYearOfConstruction?: string;
  forSale?: "true" | "false";
  forRent?: "true" | "false";
  isFurnished?: "true" | "false";
  sortBy?: "price" | "area" | "yearOfConstruction";
  descending?: "true" | "false";
  page?: string;
  perPage?: string;
};
