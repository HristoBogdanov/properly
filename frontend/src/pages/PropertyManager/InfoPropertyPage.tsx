import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Heading from "@/components/common/Heading";
import Checkbox from "@/components/inputs/Checkbox";
import Input from "@/components/inputs/Input";
import Textarea from "@/components/inputs/Textarea";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { Property } from "@/types/property";
import { FormProvider, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { createPropertySchema } from "@/lib/schemas";
import { z } from "zod";
import LockedImageGrid from "@/components/properties/LockedImageGrid";

type FormData = z.infer<typeof createPropertySchema>;

export default function InfoPropertyPage() {
  const { id } = useParams<{ id: string }>();
  const { getPropertyById } = usePropertiesStore();
  const [property, setProperty] = useState<Property | null>(null);

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
      ownerId: "",
      categories: [],
      features: [],
      images: [{ name: "", path: "" }],
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
      }
    };
    fetchProperty();
  }, [id, getPropertyById, methods]);

  if (!property) {
    return <div>Loading...</div>;
  }

  return (
    <div className="w-full flex flex-col justify-center items-center gap-10 my-20 px-6">
      <Heading title="View Property" />
      <FormProvider {...methods}>
        <form className="flex flex-col w-full gap-6 text-md md:text-lg">
          <div className="flex flex-col gap-2">
            <p className="text-sm font-semibold">Title</p>
            <Input
              id="title"
              name="title"
              isRequired={true}
              errorColor="primary"
              placeholder="Title"
              disabled
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
              disabled
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
              disabled
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
              disabled
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
              disabled
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
              disabled
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
              disabled
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
              disabled
            />
          </div>
          <Checkbox id="forSale" name="forSale" label="For Sale" disabled />
          <Checkbox id="forRent" name="forRent" label="For Rent" disabled />
          <Checkbox
            id="isFurnished"
            name="isFurnished"
            label="Property is furnished"
            disabled
          />

          <h3 className="text-2xl font-semibold">Categories</h3>
          <div className="w-full flex flex-wrap gap-6">
            {property.categories.map((category) => (
              <Checkbox
                key={category.id}
                id={category.id}
                value={category.title}
                name="categories"
                label={category.title}
                defaultChecked
                disabled
                showError={false}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Features</h3>
          <div className="w-full flex flex-wrap gap-6">
            {property.features.map((feature) => (
              <Checkbox
                key={feature.id}
                id={feature.id}
                value={feature.title}
                name="features"
                label={feature.title}
                defaultChecked
                disabled
                showError={false}
              />
            ))}
          </div>
          <h3 className="text-2xl font-semibold">Images</h3>
          <LockedImageGrid images={property.images} />
        </form>
      </FormProvider>
    </div>
  );
}
