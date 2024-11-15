import { useFormContext } from "react-hook-form";

type CheckboxProps = {
  id: string;
  name: string;
  label: string;
  value?: string;
  isRequired?: boolean;
  classes?: string;
  showError?:boolean;
};

export default function Checkbox({
  id,
  name,
  label,
  value,
  isRequired,
  classes,
  showError = true
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
        value={value}
        className="border rounded-md"
        {...register(name, { required: isRequired })}
      />
      <label htmlFor={id}>{label}</label>
      {showError && errors[name] && (
        <p className="text-sm text-primary font-semibold text-wrap">
          {errors[name]?.message as string}
        </p>
      )}
    </div>
  );
}
