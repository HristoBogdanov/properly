import { loginAPI, registerAPI } from "@/api/auth";
import { handleError } from "@/helpers/ErrorHandler";
import { UserProfile } from "@/types/user";
import axios from "axios";
import { createContext, useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

type UserContextType = {
  user: UserProfile | null;
  token: string | null;
  registerUser: (email: string, username: string, password: string) => void;
  loginUser: (username: string, password: string) => void;
  logout: () => void;
  isLoggedIn: () => boolean;
  isUserAdmin: () => boolean;
};

type Props = { children: React.ReactNode };

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider = ({ children }: Props) => {
  const navigate = useNavigate();
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<UserProfile | null>(null);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    const user = localStorage.getItem("user");
    const token = localStorage.getItem("token");
    if (user && token) {
      setUser(JSON.parse(user));
      setToken(token);
      // this token is not getting added correctly, so I am adding it via an axios interceptor
      axios.defaults.headers.common["Authorization"] = "Bearer " + token;
    }
    setIsReady(true);
  }, []);

  const registerUser = async (
    email: string,
    username: string,
    password: string
  ) => {
    await registerAPI({ email, username, password })
      .then((res) => {
        if (res) {
          localStorage.setItem("token", res?.data.token);
          const userObj: UserProfile = {
            userName: res?.data.userName,
            email: res?.data.email,
            role: res?.data.role,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res.data.token);
          setUser(userObj!);
          toast.success("Register Successfull!");
          navigate("/");
        }
      })
      .catch((error) => {
        handleError(error, "Error registering user");
      });
  };

  const loginUser = async (username: string, password: string) => {
    await loginAPI({ username, password })
      .then((res) => {
        if (res) {
          localStorage.setItem("token", res?.data.token);
          const userObj: UserProfile = {
            userName: res?.data.userName,
            email: res?.data.email,
            role: res?.data.role,
          };
          localStorage.setItem("user", JSON.stringify(userObj));
          setToken(res.data.token);
          setUser(userObj!);
          toast.success("Login Successfull!");
          navigate("/");
        }
      })
      .catch((error) => {
        handleError(error, "Error logging in user");
      });
  };

  const isLoggedIn = () => {
    return !!user;
  };

  const isUserAdmin = () => {
    return user?.role == "Admin";
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
    setToken("");
    navigate("/");
    toast.success("You logged out of your account.");
  };

  return (
    <UserContext.Provider
      value={{
        loginUser,
        user,
        token,
        logout,
        isLoggedIn,
        registerUser,
        isUserAdmin,
      }}
    >
      {isReady ? children : null}
    </UserContext.Provider>
  );
};

export const useAuth = () => useContext(UserContext); // eslint-disable-line
