import { z } from "zod";
import { passwordRegex } from "./regexes";

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
  sortBy: z.enum(["price", "area", "yearOfConstruction"]).optional().or(z.literal("")),
  descending: z.enum(["true", "false"]).optional().or(z.literal("")),
});
