import { SelectHTMLAttributes } from "react";
import { useFormContext } from "react-hook-form";

type DropdownProps = SelectHTMLAttributes<HTMLSelectElement> & {
  id: string;
  classes?: string;
  placeholder?: string;
  options: { value: string; label: string }[];
};

export default function Dropdown({
  id,
  classes,
  placeholder,
  options,
  ...rest
}: DropdownProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className="flex flex-col md:flex-row gap-2 md:gap-5 justify-between w-full">
      <div className={`${classes} flex flex-col gap-2 w-full`}>
        <select className="rounded-md p-2" id={id} {...register(id)} {...rest}>
          {placeholder && (
            <option value="" disabled className="text-[#9CA3AF]">
              {placeholder}
            </option>
          )}
          {options.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
        {errors[id] && (
          <p className="text-sm text-white font-semibold text-wrap">
            {errors[id]?.message as string}
          </p>
        )}
      </div>
    </div>
  );
}
