import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { handleError } from "@/helpers/ErrorHandler";
import { createCategorySchema } from "@/lib/schemas";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { zodResolver } from "@hookform/resolvers/zod";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createCategorySchema>;

export default function AddCategoryPage() {
  const methods = useForm<FormData>({
    resolver: zodResolver(createCategorySchema),
  });

  const navigate = useNavigate();

  const { loading, addCategory } = useCategoriesStore();

  const onSubmit = async (data: FormData) => {
    try {
      const result = await addCategory(data);
      if (result) {
        toast.success("You have succesfully created a new category");
        navigate("/dashboard/categories");
      } else {
        toast.error("Error adding category");
      }
    } catch (error: any) {
      handleError(error, "Error adding category");
    }
  };
  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="Add Category" />
      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
          />
          <CustomButton
            text={loading ? "Loading..." : "Add Category"}
            disabled={loading}
            type="submit"
            classes={`${
              loading && "opacity-60 hover:opacity-60 cursor-wait"
            } w-fit h-fit mt-10`}
          />
        </form>
      </FormProvider>
    </div>
  );
}
