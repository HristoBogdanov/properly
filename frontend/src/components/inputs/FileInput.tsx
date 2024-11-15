import { useFormContext } from "react-hook-form";

type FileInputProps = {
  id: string;
  name: string;
  isRequired?: boolean;
  classes?: string;
  filter?: string;
};

export default function FileInput({
  id,
  name,
  isRequired,
  classes,
  filter,
}: FileInputProps) {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <div className={`flex flex-col gap-2 w-full ${classes}`}>
      <input
        type="file"
        id={id}
        {...register(name, { required: isRequired })}
        className="border rounded-md p-2"
        multiple
        accept={filter}
      />
      {errors[name] && (
        <p className="text-sm text-primary font-semibold text-wrap">
          {errors[name]?.message as string}
        </p>
      )}
    </div>
  );
}
