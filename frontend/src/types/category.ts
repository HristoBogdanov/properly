import { SimpleProperty } from "./property";

export type Category = {
  id: string;
  title: string;
  properties: SimpleProperty[];
};

export type SimpleCategory = {
  id: string;
  title: string;
};

export type CreateCategory = {
  title: string;
};
