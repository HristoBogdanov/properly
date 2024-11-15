import { InputHTMLAttributes } from "react";
import { useFormContext } from "react-hook-form";

type InputProps = InputHTMLAttributes<HTMLInputElement> & {
  id: string;
  name: string;
  type?: string;
  placeholder?: string;
  isRequired?: boolean;
  errorColor?: string;
  showError?: boolean;
  classes?: string;
  valueAsNumber?: boolean;
};

export default function Input({
  id,
  name,
  type = "text",
  placeholder,
  isRequired,
  errorColor = "red",
  showError = true,
  classes,
  valueAsNumber,
  ...rest
}: InputProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className={`flex flex-col gap-2 ${classes}`}>
      <input
        id={id}
        type={type}
        placeholder={
          (placeholder || name.charAt(0).toUpperCase() + id.slice(1)) +
          (isRequired ? " *" : "")
        }
        {...register(name, {
          required: isRequired,
          valueAsNumber: valueAsNumber,
        })}
        className="border rounded-md p-2"
        {...rest}
      />
      {showError && errors[name] && (
        <p className={`text-sm text-${errorColor} font-semibold text-wrap`}>
          {errors[name]?.message as string}
        </p>
      )}
    </div>
  );
}
