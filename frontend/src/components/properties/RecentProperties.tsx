import { usePropertiesStore } from "@/stores/propertiesStore";
import PropertyAsideCard from "./PropertyAsideCard";

export default function RecentProperties() {
  const { properties } = usePropertiesStore();
  return (
    <div className="w-full flex flex-col justify-center items-center gap-6">
      <h2 className="subheading w-full text-2xl">Recent Properties</h2>
      {properties.map((property) => (
        <PropertyAsideCard key={property.id} property={property} />
      ))}
    </div>
  );
}
