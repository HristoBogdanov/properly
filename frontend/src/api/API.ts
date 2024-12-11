import { NavigationFunctionType } from "@/types/navigatioFunctionType";
import axios from "axios";

const API = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

// Add a request interceptor to include the JWT token
API.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Navigation function to be dynamically set for SPA redirects
let navigateFunction: NavigationFunctionType = null;

export const setGlobalNavigate = (navigateFn: NavigationFunctionType) => {
  navigateFunction = navigateFn;
};

// Add a response interceptor for global error handling
API.interceptors.response.use(
  (response) => {
    // Pass through successful responses
    return response;
  },
  (error) => {
    if (axios.isAxiosError(error)) {
      const status = error.response?.status;
      const message = error.message;

      // Handle 500 Internal Server Error
      if (status === 500 || message === "Network Error") {
        if (navigateFunction) {
          navigateFunction("/server-error");
        }
      }
    }

    // Reject the promise to allow local error handling as well
    return Promise.reject(error);
  }
);

export default API;
