import { z } from "zod";
import { passwordRegex } from "./regexes";

const numberErrorMessage = {
  required_error: "This field is required",
  invalid_type_error: "Enter a number!",
};

export const registerSchema = z.object({
  username: z.string().min(3, "Username should be at least 3 characters"),
  email: z.string().email("Invalid email address"),
  password: z
    .string()
    .regex(
      passwordRegex,
      "Password must be at least 12 characters, contain a number and a lowercase letter"
    ),
});

export const loginSchema = z.object({
  username: z.string().min(3, "Username should be at least 3 characters"),
  password: z
    .string()
    .regex(
      passwordRegex,
      "Password must be at least 12 characters, contain a number and a lowercase letter"
    ),
});

export const simpleSeacrhPropertySchema = z.object({
  search: z.string().min(3, "Search should be at least 3 characters"),
  sortBy: z
    .enum(["price", "area", "yearOfConstruction"])
    .optional()
    .or(z.literal("")),
  descending: z.enum(["true", "false"]).optional().or(z.literal("")),
});

export const createImageSchema = z.object({
  name: z
    .string()
    .min(3, "Image name must be at least 3 characters")
    .max(100, "Image name must be at most 100 characters"),
  path: z
    .string()
    .min(3, "Image path must be at least 3 characters")
    .max(100, "Image path must be at most 100 characters"),
});

export const createPropertySchema = z.object({
  title: z
    .string()
    .min(3, "Title must be at least 3 characters")
    .max(200, "Title must be at most 200 characters"),
  description: z
    .string()
    .min(20, "Description must be at least 20 characters")
    .max(5000, "Description must be at most 5000 characters"),
  address: z
    .string()
    .min(3, "Address must be at least 3 characters")
    .max(500, "Address must be at most 500 characters"),
  price: z
    .number(numberErrorMessage)
    .min(0, "Price must be at least 0")
    .max(100000000, "Price must be at most 100000000"),
  area: z
    .number(numberErrorMessage)
    .min(0, "Area must be at least 0")
    .max(10000, "Area must be at most 10000"),
  yearOfConstruction: z
    .number(numberErrorMessage)
    .int()
    .min(1800, "Year of construction must be at least 1800")
    .max(2100, "Year of construction must be at most 2100"),
  // Check if its Nan and map it to undefined
  bedrooms: z.preprocess(
    (val: any) => (isNaN(val) ? undefined : val),
    z
      .number(numberErrorMessage)
      .int()
      .min(0, "Bedrooms must be at least 0")
      .max(100, "Bedrooms must be at most 100")
      .optional()
  ),
  // Check if its Nan and map it to undefined
  bathrooms: z.preprocess(
    (val: any) => (isNaN(val) ? undefined : val),
    z
      .number(numberErrorMessage)
      .int()
      .min(0, "Bathrooms must be at least 0")
      .max(100, "Bathrooms must be at most 100")
      .optional()
  ),
  forSale: z.boolean(),
  forRent: z.boolean(),
  isFurnished: z.boolean(),
  ownerId: z.string(),
  categories: z.optional(z.array(z.string())),
  features: z.optional(z.array(z.string())),
  images: z.array(
    z.object({
      name: z.string().min(1, "Image name is required"),
      path: z.string().min(1, "Image path is required"),
    })
  ),
});
