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

export default function InfoPropertyPage() {
  const { id } = useParams<{ id: string }>();
  const { getPropertyById } = usePropertiesStore();
  const [property, setProperty] = useState<Property | null>(null);

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
              defaultValue={property.title}
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
              defaultValue={property.description}
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
              defaultValue={property.address}
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
              defaultValue={property.price}
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
              defaultValue={property.bedrooms}
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
              defaultValue={property.bathrooms}
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
              defaultValue={property.area}
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
              defaultValue={property.yearOfConstruction}
              disabled
            />
          </div>
          <Checkbox
            id="forSale"
            name="forSale"
            label="For Sale"
            checked={property.forSale}
            disabled
          />
          <Checkbox
            id="forRent"
            name="forRent"
            label="For Rent"
            checked={property.forRent}
            disabled
          />
          <Checkbox
            id="isFurnished"
            name="isFurnished"
            label="Property is furnished"
            checked={property.isFurnished}
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
                checked
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
                  disabled
                />
                <Input
                  id={`images.${index}.path`}
                  name={`images.${index}.path`}
                  isRequired={true}
                  errorColor="primary"
                  placeholder="Image Path"
                  defaultValue={image.path}
                  disabled
                  classes="w-full"
                />
              </div>
            ))}
          </div>
        </form>
      </FormProvider>
    </div>
  );
}
