import { ButtonHTMLAttributes } from "react";
import { Link } from "react-router-dom";

type ButtonProps = ButtonHTMLAttributes<HTMLButtonElement> & {
  text: string;
  link?: string;
  classes?: string;
  variant?: "primary" | "secondary";
};

export default function CustomButton({
  text,
  link,
  classes = "",
  variant = "primary",
  onClick,
  ...rest
}: ButtonProps) {
  return (
    <>
      {link ? (
        <Link to={link}>
          <button
            className={`${classes} ${
              variant == "primary"
                ? "text-white border-white hover:bg-white hover:text-primary hover:border-primary"
                : "text-primary bg-white border-primary hover:bg-primary hover:text-white hover:border-white"
            } py-3 px-8 mx-auto w-fit border-2 text-lg md:text-xl font-bold bg-primary rounded-md transition-all duration-300 ease-in-out`}
            onClick={onClick}
            {...rest}
          >
            {text}
          </button>
        </Link>
      ) : (
        <button
          className={`${classes} ${
            variant == "primary"
              ? "text-white border-white hover:bg-white hover:text-primary hover:border-primary"
              : "text-primary bg-white border-primary hover:bg-primary hover:text-white hover:border-white"
          } py-3 px-8 mx-auto w-fit border-2 text-lg md:text-xl font-bold bg-primary rounded-md transition-all duration-300 ease-in-out`}
          onClick={onClick}
          {...rest}
        >
          {text}
        </button>
      )}
    </>
  );
}
