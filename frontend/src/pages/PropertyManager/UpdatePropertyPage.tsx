import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Heading from "@/components/common/Heading";
import Checkbox from "@/components/inputs/Checkbox";
import Input from "@/components/inputs/Input";
import Textarea from "@/components/inputs/Textarea";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { Property } from "@/types/property";
import { FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { createPropertySchema } from "@/lib/schemas";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { useFeaturesStore } from "@/stores/featuresStore";
import CustomButton from "@/components/common/CustomButton";
import { toast } from "react-toastify";
import { handleError } from "@/helpers/ErrorHandler";
import { z } from "zod";

type FormData = z.infer<typeof createPropertySchema>;

export default function UpdatePropertyPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { getPropertyById, loading, updateProperty } = usePropertiesStore();
  const [property, setProperty] = useState<Property | null>(null);

  const { categories, getCategories } = useCategoriesStore();
  const { features, getFeatures } = useFeaturesStore();

  const methods = useForm<FormData>({
    resolver: zodResolver(createPropertySchema),
  });

  useEffect(() => {
    const fetchProperty = async () => {
      if (id) {
        const fetchedProperty = await getPropertyById(id);
        setProperty(fetchedProperty);
      }
    };
    fetchProperty();
  }, [id, getPropertyById]);

  useEffect(() => {
    getCategories();
    getFeatures();
  }, [getCategories, getFeatures]);

  if (!property) {
    return <div>Loading...</div>;
  }

  const onSubmit = async (data: FormData) => {
    if (data.forSale === false && data.forRent === false) {
      toast.error("Mark whether the house is for sale, for rent or both!");
      return;
    }

    if (!id) {
      toast.error("Property ID is required");
      return;
    }

    try {
      const result = await updateProperty(data, id);
      if (result) {
        toast.success("You have successfully updated the property");
        navigate("/dashboard/properties");
      } else {
        toast.error("Error updating property");
      }
    } catch (error: any) {
      handleError(error, "Error updating property");
    }
  };

  return (
    <div className="w-full flex flex-col justify-center items-center gap-10 my-20 px-6">
      <Heading title="Update Property" />
      <FormProvider {...methods}>
        <form
          onSubmit={methods.handleSubmit(onSubmit)}
          className="flex flex-col w-full gap-6 text-md md:text-lg"
        >
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Title</p>
            <Input
              id="title"
              name="title"
              isRequired={true}
              errorColor="primary"
              placeholder="Title"
              defaultValue={property.title}
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Description</p>
            <Textarea
              id="description"
              name="description"
              isRequired={true}
              errorColor="primary"
              placeholder="Description"
              defaultValue={property.description}
              classes="min-h-[200px]"
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Address</p>
            <Input
              id="address"
              name="address"
              isRequired={true}
              errorColor="primary"
              placeholder="Address"
              defaultValue={property.address}
            />
            <div className="flex flex-col gap-2"></div>
            <p className="text-sm font-semibold">Price</p>
            <Input
              id="price"
              name="price"
              type="number"
              isRequired={true}
              errorColor="primary"
              placeholder="Price"
              valueAsNumber
              defaultValue={property.price}
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Bedrooms</p>
            <Input
              id="bedrooms"
              name="bedrooms"
              type="number"
              isRequired={false}
              errorColor="primary"
              placeholder="Bedrooms"
              valueAsNumber
              defaultValue={property.bedrooms}
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Bathrooms</p>
            <Input
              id="bathrooms"
              name="bathrooms"
              type="number"
              isRequired={false}
              errorColor="primary"
              placeholder="Bathrooms"
              valueAsNumber
              defaultValue={property.bathrooms}
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Area (sq ft)</p>
            <Input
              id="area"
              name="area"
              type="number"
              isRequired={true}
              errorColor="primary"
              placeholder="Area (sq ft)"
              valueAsNumber
              defaultValue={property.area}
            />
          </div>
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Year of Construction</p>
            <Input
              id="yearOfConstruction"
              name="yearOfConstruction"
              type="number"
              isRequired={true}
              errorColor="primary"
              placeholder="Year of Construction"
              valueAsNumber
              defaultValue={property.yearOfConstruction}
            />
          </div>
          <Checkbox
            id="forSale"
            name="forSale"
            label="For Sale"
            defaultChecked={property.forSale}
          />
          <Checkbox
            id="forRent"
            name="forRent"
            label="For Rent"
            defaultChecked={property.forRent}
          />
          <Checkbox
            id="isFurnished"
            name="isFurnished"
            label="Property is furnished"
            defaultChecked={property.isFurnished}
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
                defaultChecked={property.categories.some(
                  (c) =>
                    c.id.toLocaleLowerCase() === category.id.toLocaleLowerCase()
                )}
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
                defaultChecked={property.features.some(
                  (f) =>
                    f.id.toLocaleLowerCase() === feature.id.toLocaleLowerCase()
                )}
                showError={false}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Images</h3>
          <div className="w-full flex flex-col gap-4">
            {property.images.map((image, index) => (
              <div key={index} className="flex w-full gap-2">
                <Input
                  id={`images.${index}.name`}
                  name={`images.${index}.name`}
                  isRequired={true}
                  errorColor="primary"
                  placeholder="Image Name"
                  defaultValue={image.name}
                />
                <Input
                  id={`images.${index}.path`}
                  name={`images.${index}.path`}
                  isRequired={true}
                  errorColor="primary"
                  placeholder="Image Path"
                  defaultValue={image.path}
                  classes="w-full"
                />
              </div>
            ))}
          </div>
          <CustomButton
            text={loading ? "Loading..." : "Update Property"}
            disabled={loading}
            type="submit"
            classes={`${
              loading && "opacity-60 hover:opacity-60 cursor-wait"
            } w-full`}
          />
        </form>
      </FormProvider>
    </div>
  );
}
