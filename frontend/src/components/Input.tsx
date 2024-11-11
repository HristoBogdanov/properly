import { InputHTMLAttributes } from "react";
import { useFormContext } from "react-hook-form";

type InputProps = InputHTMLAttributes<HTMLInputElement> & {
  id: string;
  isRequired?: boolean;
};

export default function Input({ id, isRequired = false, ...rest }: InputProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className="flex flex-col md:flex-row gap-2 md:gap-5 justify-between w-full">
      <div className="flex gap-2">
        <label htmlFor={id} className="text-white font-semibold">
          {id.charAt(0).toUpperCase() + id.slice(1)}
        </label>
        {isRequired && <span className="text-white">*</span>}
      </div>
      <div className="flex flex-col gap-2 w-full max-w-[270px]">
        <input className="rounded-md p-1" id={id} {...register(id)} {...rest} />
        {errors[id] && (
          <p className="text-sm text-white font-semibold text-wrap">
            {errors[id]?.message as string}
          </p>
        )}
      </div>
    </div>
  );
}
