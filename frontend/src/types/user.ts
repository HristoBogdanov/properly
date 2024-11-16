export type UserProfileToken = {
  userName: string;
  email: string;
  token: string;
};

export type UserProfile = {
  userName: string;
  email: string;
};

export type CreateUser = {
  email: string;
  username: string;
  password: string;
};

export type LoginUser = {
  username: string;
  password: string;
};

export type User = {
  id: string;
  username: string;
  email: string;
  role: string;
  numberOfProperties: number;
};
