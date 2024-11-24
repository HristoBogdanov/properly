import AddIconSection from "@/components/common/AddIconSection";
import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import Input from "@/components/inputs/Input";
import { handleError } from "@/helpers/ErrorHandler";
import { createFeatureSchema } from "@/lib/schemas";
import { useFeaturesStore } from "@/stores/featuresStore";
import { useImagesStore } from "@/stores/imagesStore";
import { zodResolver } from "@hookform/resolvers/zod";
import { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { z } from "zod";

type FormData = z.infer<typeof createFeatureSchema>;

export default function AddFeaturePage() {
  const methods = useForm<FormData>({
    resolver: zodResolver(createFeatureSchema),
  });

  const navigate = useNavigate();

  const { loading, addFeature } = useFeaturesStore();
  const { imagesToAddToProperty, clearImagesToAddToProperty } =
    useImagesStore();

  useEffect(() => {
    clearImagesToAddToProperty();
  }, [clearImagesToAddToProperty]);

  const onSubmit = async (data: FormData) => {
    if (imagesToAddToProperty.length === 0) {
      toast.error("Please select an icon for the feature");
    }
    try {
      data.image = {
        name: imagesToAddToProperty[imagesToAddToProperty.length - 1].name,
        path: imagesToAddToProperty[imagesToAddToProperty.length - 1].path,
      };
      const result = await addFeature(data);
      if (result) {
        toast.success("You have succesfully created a new feature");
        navigate("/dashboard/features");
      } else {
        toast.error("Error adding feature");
      }
    } catch (error: any) {
      handleError(error, "Error adding feature");
    }
  };
  return (
    <div className="w-full flex flex-col gap-10 my-20">
      <Heading title="Add Feature" />
      <FormProvider {...methods}>
        <form onSubmit={methods.handleSubmit(onSubmit)}>
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
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
