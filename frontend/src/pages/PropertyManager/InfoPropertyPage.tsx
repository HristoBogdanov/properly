import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import Heading from "@/components/common/Heading";
import Checkbox from "@/components/inputs/Checkbox";
import Input from "@/components/inputs/Input";
import Textarea from "@/components/inputs/Textarea";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { Property } from "@/types/property";

export default function InfoPropertyPage() {
  const [searchParams] = useSearchParams();
  const propertyId = searchParams.get("id");
  const { getPropertyById } = usePropertiesStore();
  const [property, setProperty] = useState<Property | null>(null);

  useEffect(() => {
    const fetchProperty = async () => {
      if (propertyId) {
        const fetchedProperty = await getPropertyById(propertyId);
        setProperty(fetchedProperty);
      }
    };
    fetchProperty();
  }, [propertyId, getPropertyById]);

  if (!property) {
    return <div>Loading...</div>;
  }

  return (
    <div className="w-full flex flex-col justify-center items-center gap-10 my-20">
      <Heading title="View Property" />
      <form className="flex flex-col w-full gap-6 text-md md:text-lg">
        <Input
          id="title"
          name="title"
          isRequired={true}
          errorColor="primary"
          placeholder="Title"
          defaultValue={property.title}
          disabled
        />
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
        <Input
          id="address"
          name="address"
          isRequired={true}
          errorColor="primary"
          placeholder="Address"
          defaultValue={property.address}
          disabled
        />
        <Input
          id="ownerId"
          name="ownerId"
          isRequired={true}
          errorColor="primary"
          placeholder="Owner ID"
          defaultValue={property.ownerId}
          disabled
        />
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
        <Checkbox
          id="forSale"
          name="forSale"
          label="For Sale"
          defaultChecked={property.forSale}
          disabled
        />
        <Checkbox
          id="forRent"
          name="forRent"
          label="For Rent"
          defaultChecked={property.forRent}
          disabled
        />
        <Checkbox
          id="isFurnished"
          name="isFurnished"
          label="Property is furnished"
          defaultChecked={property.isFurnished}
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
        <Input
          id="images.name"
          name="images.0.name"
          isRequired={true}
          errorColor="primary"
          placeholder="Image Name"
          defaultValue={property.images[0]?.name}
          disabled
        />
        <Input
          id="images.path"
          name="images.0.path"
          isRequired={true}
          errorColor="primary"
          placeholder="Image Path"
          defaultValue={property.images[0]?.path}
          disabled
        />
      </form>
    </div>
  );
}
