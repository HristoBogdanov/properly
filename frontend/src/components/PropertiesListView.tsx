import { Property } from "@/types/property";
import PropertyListCard from "./PropertyListCard";
import Pagination from "@/components/Pagination";

type PropertiesListViewProps = {
  properties: Property[];
  loading: boolean;
  baseUrl: string;
  page: number;
  perPage: number;
  total: number;
};

export default function PropertiesListView({
  properties,
  baseUrl,
  page,
  perPage,
  total,
}: // loading,
PropertiesListViewProps) {
  return (
    <div className="w-full flex flex-col justify-center items-center gap-6 lg:gap-10">
      {properties.map((property) => (
        <PropertyListCard key={property.id} property={property} />
      ))}
      <Pagination
        baseUrl={baseUrl}
        currentPage={page}
        perPage={perPage}
        total={total}
      />
    </div>
  );
}
