import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import Input from "@/components/inputs/Input";
import CustomButton from "@/components/common/CustomButton";
import { createPropertySchema } from "@/lib/schemas";
import Heading from "@/components/common/Heading";
import { useFeaturesStore } from "@/stores/featuresStore";
import { useCategoriesStore } from "@/stores/categoriesStore";
import Checkbox from "@/components/inputs/Checkbox";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { handleError } from "@/helpers/ErrorHandler";

type FormData = z.infer<typeof createPropertySchema>;

export default function AddListingPage() {
  const methods = useForm<FormData>({
    resolver: zodResolver(createPropertySchema),
  });

  const { categories } = useCategoriesStore();
  const { features } = useFeaturesStore();
  const { addProperty } = usePropertiesStore();

  const onSubmit = async (data: FormData) => {
    console.log(data);
    try {
      await addProperty(data);
      console.log(data);
      // Handle success
    } catch (error: any) {
      handleError(error, "Error adding property");
    }
  };

  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap20 mb-10 lg:mb-20 px-6">
      <Heading title="Add Property" />
      <FormProvider {...methods}>
        <form
          onSubmit={methods.handleSubmit(onSubmit)}
          className="flex flex-col w-full gap-6 text-md md:text-lg"
        >
          <Input
            id="title"
            name="title"
            isRequired={true}
            errorColor="primary"
            placeholder="Title"
          />
          <Input
            id="description"
            name="description"
            isRequired={true}
            errorColor="primary"
            placeholder="Description"
          />
          <Input
            id="address"
            name="address"
            isRequired={true}
            errorColor="primary"
            placeholder="Address"
          />
          <Input
            id="price"
            name="price"
            type="number"
            isRequired={true}
            errorColor="primary"
            placeholder="Price"
            valueAsNumber
          />
          <Input
            id="bedrooms"
            name="bedrooms"
            type="number"
            isRequired={false}
            errorColor="primary"
            placeholder="Bedrooms"
            valueAsNumber
          />
          <Input
            id="bathrooms"
            name="bathrooms"
            type="number"
            isRequired={false}
            errorColor="primary"
            placeholder="Bathrooms"
            valueAsNumber
          />
          <Input
            id="area"
            name="area"
            type="number"
            isRequired={true}
            errorColor="primary"
            placeholder="Area (sq ft)"
            valueAsNumber
          />
          <Input
            id="yearOfConstruction"
            name="yearOfConstruction"
            type="number"
            isRequired={true}
            errorColor="primary"
            placeholder="Year of Construction"
            valueAsNumber
          />
          <Checkbox id="forSale" name="forSale" label="For Sale" />
          <Checkbox id="forRent" name="forRent" label="For Rent" />
          <Checkbox
            id="isFurnished"
            name="isFurnished"
            label="Property is furnished"
          />
          <h3 className="text-2xl font-semibold">Categories</h3>
          <div className="w-full flex flex-wrap gap-6">
            {categories.map((category) => (
              <Checkbox
                key={category.id}
                id={category.id}
                name={`categories.${category.id}`}
                label={category.title}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Features</h3>
          <div className="w-full flex flex-wrap gap-6">
            {features.map((feature) => (
              <Checkbox
                key={feature.id}
                id={feature.id}
                name={`features.${feature.id}`}
                label={feature.title}
              />
            ))}
          </div>
          <Input
            id="images.name"
            name="images.0.name"
            isRequired={true}
            errorColor="primary"
            placeholder="Image Name"
          />
          <Input
            id="images.path"
            name="images.0.path"
            isRequired={true}
            errorColor="primary"
            placeholder="Image Path"
          />
          <CustomButton text="Add Property" type="submit" classes="w-full" />
        </form>
      </FormProvider>
    </div>
  );
}
