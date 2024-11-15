import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import Input from "@/components/inputs/Input";
import CustomButton from "@/components/common/CustomButton";
import { createPropertySchema } from "@/lib/schemas";
import Heading from "@/components/common/Heading";
import Checkbox from "@/components/inputs/Checkbox";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { useFeaturesStore } from "@/stores/featuresStore";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { handleError } from "@/helpers/ErrorHandler";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";

type FormData = z.infer<typeof createPropertySchema>;

export default function AddListingPage() {
  const methods = useForm<FormData>({
    resolver: zodResolver(createPropertySchema),
  });

  const navigate = useNavigate();

  const { categories } = useCategoriesStore();
  const { features } = useFeaturesStore();
  const { addProperty } = usePropertiesStore();

  const onSubmit = async (data: FormData) => {
    if (data.forSale === false && data.forRent === false) {
      toast.error("Mark whether the house is for sale, for rent or both!");
      return;
    }

    try {
      await addProperty(data);
      toast.success("You have successfully created a new property");
      navigate("/properties");
    } catch (error: any) {
      handleError(error, "Error adding property");
    }
  };

  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
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
            defaultValue="test title no collections"
          />
          <Input
            id="description"
            name="description"
            isRequired={true}
            errorColor="primary"
            placeholder="Description"
            defaultValue="testtesttesttesttesttesttesttest"
          />
          <Input
            id="address"
            name="address"
            isRequired={true}
            errorColor="primary"
            placeholder="Address"
            defaultValue="jelio manolov"
          />
          <Input
            id="ownerId"
            name="ownerId"
            isRequired={true}
            errorColor="primary"
            placeholder="Owner ID"
            defaultValue="A943FA8B-7525-4DC6-84EE-50BEBF957EE4"
          />
          <Input
            id="price"
            name="price"
            type="number"
            isRequired={true}
            errorColor="primary"
            placeholder="Price"
            valueAsNumber
            defaultValue={300000}
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
            defaultValue={150}
          />
          <Input
            id="yearOfConstruction"
            name="yearOfConstruction"
            type="number"
            isRequired={true}
            errorColor="primary"
            placeholder="Year of Construction"
            valueAsNumber
            defaultValue={2002}
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
                value={category.title}
                name="categories"
                label={category.title}
                showError={false}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Features</h3>
          <div className="w-full flex flex-wrap gap-6">
            {features.map((feature) => (
              <Checkbox
                key={feature.id}
                id={feature.id}
                value={feature.title}
                name="features"
                label={feature.title}
                showError={false}
              />
            ))}
          </div>
          <Input
            id="images.name"
            name="images.0.name"
            isRequired={true}
            errorColor="primary"
            placeholder="Image Name"
            defaultValue="test"
          />
          <Input
            id="images.path"
            name="images.0.path"
            isRequired={true}
            errorColor="primary"
            placeholder="Image Path"
            defaultValue="https://thumbor.forbes.com/thumbor/fit-in/900x510/https://www.forbes.com/home-improvement/wp-content/uploads/2022/07/Paris_Exterior_4-Edit-e1714649473120.png"
          />
          <CustomButton text="Add Property" type="submit" classes="w-full" />
        </form>
      </FormProvider>
    </div>
  );
}
