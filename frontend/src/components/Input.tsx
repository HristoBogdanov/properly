import { InputHTMLAttributes } from "react";
import { useFormContext } from "react-hook-form";

type InputProps = InputHTMLAttributes<HTMLInputElement> & {
  id: string;
  isRequired?: boolean;
  classes?: string;
  placeholder?: string;
  showError?: boolean;
};

export default function Input({
  id,
  isRequired = false,
  classes,
  placeholder,
  showError = true,
  ...rest
}: InputProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className="flex flex-col md:flex-row gap-2 md:gap-5 justify-between w-full">
      <div className={`${classes} flex flex-col gap-2 w-full`}>
        <input
          className="rounded-md p-2"
          id={id}
          {...register(id)}
          {...rest}
          placeholder={
            placeholder ||
            id.charAt(0).toUpperCase() + id.slice(1) + (isRequired ? " *" : "")
          }
        />
        {showError && errors[id] && (
          <p className="text-sm text-white font-semibold text-wrap">
            {errors[id]?.message as string}
          </p>
        )}
      </div>
    </div>
  );
}
