import { Property } from "@/types/property";
import PropertyCard from "./PropertyCard";
import Heading from "./Heading";
import SkeletonPropertyCard from "./SkeletonPropertyCard";

type PropertiesSectionProps = {
  title: string;
  properties: Property[];
  loading: boolean;
};

export default function PropertiesSection({
  title,
  properties,
  loading,
}: PropertiesSectionProps) {
  return (
    <div className="flex flex-col w-full gap-6 lg:gap-10">
      <Heading title={title} />
      <div className="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 gap-y-6">
        {loading
          ? Array.from({ length: 6 }).map((_, index) => (
              <SkeletonPropertyCard key={index} />
            ))
          : properties.map((property) => (
              <PropertyCard key={property.id} property={property} />
            ))}
      </div>
    </div>
  );
}
