import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { createFeatureSchema } from "@/lib/schemas";
import { useFeaturesStore } from "@/stores/featuresStore";
import { Feature } from "@/types/feature";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createFeatureSchema>;

export default function AddFeaturePage() {
  const { id } = useParams<{ id: string }>();
  const { getFeatureById } = useFeaturesStore();
  const [feature, setFeature] = useState<Feature | undefined>();
  const methods = useForm<FormData>({
    resolver: zodResolver(createFeatureSchema),
  });

  useEffect(() => {
    const fetchFeature = async () => {
      if (id) {
        const fetchedFeature = await getFeatureById(id);
        setFeature(fetchedFeature);
      } else {
        toast.error("That feature does not exist");
      }
    };
    fetchFeature();
  }, [getFeatureById, id]);

  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="View Feature" />
      <FormProvider {...methods}>
        <form>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
            defaultValue={feature?.title}
            disabled
          />
          <div className="flex items-center gap-10">
            <h3 className="text-2xl font-semibold my-10">Feature icon</h3>
            <img
              className="w-10 aspect-square object-cover"
              src={feature?.image.path}
              alt={feature?.image.name}
            />
          </div>
        </form>
      </FormProvider>
    </div>
  );
}
