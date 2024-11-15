import { useFormContext } from "react-hook-form";

type CheckboxProps = {
  id: string;
  name: string;
  label: string;
  isRequired?: boolean;
  classes?: string;
};

export default function Checkbox({
  id,
  name,
  label,
  isRequired,
  classes,
}: CheckboxProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className={`flex items-center gap-1 ${classes}`}>
      <input
        id={id}
        type="checkbox"
        className="border rounded-md"
        {...register(name, { required: isRequired })}
      />
      <label htmlFor={id}>{label}</label>
      {errors[name] && (
        <p className="text-sm text-primary font-semibold text-wrap">
          {errors[name]?.message as string}
        </p>
      )}
    </div>
  );
}
