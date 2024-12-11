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
import { useImagesStore } from "@/stores/imagesStore";
import UploadSection from "@/components/inputs/UploadSection";

type FormData = z.infer<typeof createPropertySchema>;

export default function UpdatePropertyPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { getPropertyById, loading, updateProperty } = usePropertiesStore();
  const [property, setProperty] = useState<Property | null>(null);

  const [isForSale, setIsForSale] = useState(false);
  const [isForRent, setIsForRent] = useState(false);

  const { categories } = useCategoriesStore();
  const { features } = useFeaturesStore();
  const {
    imagesToAddToProperty,
    setImagesToAddToProperty,
    clearImagesToAddToProperty,
  } = useImagesStore();

  const checkManualValidationFields = () => {
    if (!isForSale && !isForRent) {
      toast.error("Mark whether the house is for sale, for rent or both!");
      return;
    }

    if (imagesToAddToProperty.length === 0) {
      toast.error("Please upload at least one image");
      return;
    }
  };

  // Set default data to the form, before fetching the property
  const methods = useForm<FormData>({
    resolver: zodResolver(createPropertySchema),
    defaultValues: {
      title: "",
      description: "",
      address: "",
      price: 0,
      bedrooms: 0,
      bathrooms: 0,
      area: 0,
      yearOfConstruction: 0,
      forSale: false,
      forRent: false,
      isFurnished: false,
      categories: [],
      features: [],
      images: [],
    },
  });

  // After the property is fetched, set the property data to the form
  useEffect(() => {
    const fetchProperty = async () => {
      if (id) {
        // Fetch the property data by ID
        const fetchedProperty = await getPropertyById(id);
        // Set the fetched property data to the state
        setProperty(fetchedProperty);
        // Reset the form values with the fetched property data
        methods.reset({
          ...fetchedProperty,
          // Map the categories to an array of category IDs
          categories: fetchedProperty.categories.map((c) => c.id),
          // Map the features to an array of feature IDs
          features: fetchedProperty.features.map((f) => f.id),
        });
        setImagesToAddToProperty(fetchedProperty.images);
      }
    };
    fetchProperty();
  }, [id, getPropertyById, methods, setImagesToAddToProperty]);

  if (!property) {
    return <div>Loading...</div>;
  }
  const onSubmit = async (data: FormData) => {
    // another check here just in case the onClick validation of the button is bipassed
    if ((!isForSale && !isForRent) || imagesToAddToProperty.length === 0) {
      return;
    }

    if (!id) {
      toast.error("There is no such property");
      return;
    }

    try {
      data.images = imagesToAddToProperty.map((image) => ({
        name: image.name,
        path: image.path,
      }));
      const result = await updateProperty(data, id);
      if (result) {
        toast.success("You have successfully updated the property");
        clearImagesToAddToProperty();
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
            />
          </div>
          <Checkbox
            id="forSale"
            name="forSale"
            label="For Sale"
            onChange={(e) => setIsForSale(e.target.checked)}
          />
          <Checkbox
            id="forRent"
            name="forRent"
            label="For Rent"
            onChange={(e) => setIsForRent(e.target.checked)}
          />
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
                defaultChecked={property.categories.some(
                  (c) => c.id.toLowerCase() === category.id.toLowerCase()
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
                  (f) => f.id === feature.id
                )}
                showError={false}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Images</h3>
          <UploadSection />
          <CustomButton
            text={loading ? "Loading..." : "Update Property"}
            disabled={loading}
            type="submit"
            classes={`${
              loading && "opacity-60 hover:opacity-60 cursor-wait"
            } w-full`}
            onClick={checkManualValidationFields}
          />
        </form>
      </FormProvider>
    </div>
  );
}
