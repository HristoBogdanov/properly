import { Property } from "@/types/property";
import PropertyListCard from "./PropertyListCard";

type PropertiesListViewProps = {
  properties: Property[];
  loading: boolean;
};

export default function PropertiesListView({
  properties,
}: // loading,
PropertiesListViewProps) {
  return (
    <div className="w-full flex flex-col justify-center items-center gap-6 lg:gap-10">
      {properties.map((property) => (
        <PropertyListCard key={property.id} property={property} />
      ))}
    </div>
  );
}
