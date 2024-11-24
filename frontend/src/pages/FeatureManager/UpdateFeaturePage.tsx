import AddIconSection from "@/components/common/AddIconSection";
import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { handleError } from "@/helpers/ErrorHandler";
import { createFeatureSchema } from "@/lib/schemas";
import { useFeaturesStore } from "@/stores/featuresStore";
import { useImagesStore } from "@/stores/imagesStore";
import { Feature } from "@/types/feature";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createFeatureSchema>;

export default function UpdateFeaturePage() {
  const { id } = useParams<{ id: string }>();
  const { loading, getFeatureById, updateFeature } = useFeaturesStore();
  const [feature, setFeature] = useState<Feature | undefined>();
  const {
    imagesToAddToProperty,
    clearImagesToAddToProperty,
    setImagesToAddToProperty,
  } = useImagesStore();
  const methods = useForm<FormData>({
    resolver: zodResolver(createFeatureSchema),
  });

  const navigate = useNavigate();

  useEffect(() => {
    const fetchFeature = async () => {
      if (id) {
        const fetchedFeature = await getFeatureById(id);
        setFeature(fetchedFeature);
        setImagesToAddToProperty([fetchedFeature.image]);
      } else {
        toast.error("That feature does not exist");
      }
      clearImagesToAddToProperty();
    };
    fetchFeature();
  }, [
    getFeatureById,
    clearImagesToAddToProperty,
    setImagesToAddToProperty,
    id,
  ]);

  const onSubmit = async (data: FormData) => {
    if (imagesToAddToProperty.length === 0) {
      toast.error("Please select an icon for the feature");
    }

    if (!id) {
      toast.error("There is no such property");
      return;
    }

    try {
      data.image = {
        name: imagesToAddToProperty[imagesToAddToProperty.length - 1].name,
        path: imagesToAddToProperty[imagesToAddToProperty.length - 1].path,
      };
      const result = await updateFeature(data, id);
      if (result) {
        toast.success("You have succesfully updated the feature");
        clearImagesToAddToProperty();
        navigate("/dashboard/features");
      } else {
        toast.error("Error updating feature");
      }
    } catch (error: any) {
      handleError(error, "Error updating feature");
    }
  };
  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="Update Feature" />
      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
            defaultValue={feature?.title}
          />
          <AddIconSection />
          <CustomButton
            text={loading ? "Loading..." : "Add Feature"}
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
