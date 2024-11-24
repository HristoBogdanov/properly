import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { createCategorySchema } from "@/lib/schemas";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { SimpleCategory } from "@/types/category";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createCategorySchema>;

export default function InfoCategoryPage() {
  const { id } = useParams<{ id: string }>();
  const { getCategoryById } = useCategoriesStore();
  const [category, setCategory] = useState<SimpleCategory | undefined>();
  const methods = useForm<FormData>({
    resolver: zodResolver(createCategorySchema),
  });

  useEffect(() => {
    const fetchCategory = async () => {
      if (id) {
        const fetchedCategory = await getCategoryById(id);
        setCategory(fetchedCategory);
      } else {
        toast.error("That category does not exist");
      }
    };
    fetchCategory();
  }, [getCategoryById, id]);

  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="View Category" />
      <FormProvider {...methods}>
        <form>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
            defaultValue={category?.title}
            disabled
          />
        </form>
      </FormProvider>
    </div>
  );
}
