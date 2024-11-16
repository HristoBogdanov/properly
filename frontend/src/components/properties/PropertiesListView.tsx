import Pagination from "@/components/common/Pagination";
import { useEffect, useMemo } from "react";
import { useSearchParams } from "react-router-dom";
import { usePropertiesStore } from "@/stores/propertiesStore";
import PropertyListCard from "./PropertyListCard";
import SkeletonPropertyListCard from "../skeletons/SkeletonPropertyListCard";

export default function PropertiesListView() {
  const { loading, pages, properties, getProperties } = usePropertiesStore();
  const [searchParams] = useSearchParams();

  const params = useMemo(
    () => Object.fromEntries(searchParams.entries()),
    [searchParams]
  );

  // All the filters that are not page and perPage, to be able to continue using them
  const paramsWithoutPagination = useMemo(
    () =>
      Object.fromEntries(
        [...searchParams.entries()].filter(
          ([key]) => key !== "page" && key !== "perPage"
        )
      ),
    [searchParams]
  );

  const baseUrl = useMemo(
    () =>
      `/properties?${new URLSearchParams(paramsWithoutPagination).toString()}`,
    [paramsWithoutPagination]
  );

  useEffect(() => {
    getProperties(params);
  }, [params, getProperties]);

  if (loading) {
    return (
      <div className="w-full flex flex-col justify-center items-center gap-6 lg:gap-10">
        {Array.from({ length: 10 }).map((_, index) => (
          <SkeletonPropertyListCard key={index} />
        ))}
      </div>
    );
  }

  return (
    <div className="w-full flex flex-col justify-center items-center gap-6 lg:gap-10">
      {properties.map((property) => (
        <PropertyListCard key={property.id} property={property} />
      ))}
      <Pagination
        baseUrl={baseUrl}
        currentPage={pages.page}
        perPage={pages.perPage}
        totalPages={pages.totalPages}
      />
    </div>
  );
}
