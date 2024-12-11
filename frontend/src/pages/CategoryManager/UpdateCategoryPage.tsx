import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { handleError } from "@/helpers/ErrorHandler";
import { createCategorySchema } from "@/lib/schemas";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { SimpleCategory } from "@/types/category";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createCategorySchema>;

export default function UpdateCategoryPage() {
  const { id } = useParams<{ id: string }>();
  const { loading, getCategoryById, updateCategory } = useCategoriesStore();
  const [category, setCategory] = useState<SimpleCategory | undefined>();
  const methods = useForm<FormData>({
    resolver: zodResolver(createCategorySchema),
  });

  const navigate = useNavigate();

  useEffect(() => {
    const fetchCategory = async () => {
      if (id) {
        const fetchedCategory = await getCategoryById(id);
        setCategory(fetchedCategory);
        methods.setValue("title", fetchedCategory.title);
      } else {
        toast.error("That category does not exist");
      }
    };
    fetchCategory();
  }, [getCategoryById, id, methods]);

  const onSubmit = async (data: FormData) => {
    if (!id) {
      toast.error("There is no such property");
      return;
    }
    try {
      const result = await updateCategory(data, id);
      if (result) {
        toast.success("You have succesfully updated the category");
        navigate("/dashboard/categories");
      } else {
        toast.error("Error updating category");
      }
    } catch (error: any) {
      handleError(error, "Error updating category");
    }
  };

  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="Update Category" />
      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
            defaultValue={category?.title}
          />
          <CustomButton
            text={loading ? "Loading..." : "Update Category"}
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
