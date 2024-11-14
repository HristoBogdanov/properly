import { useEffect, useMemo } from "react";
import { useSearchParams } from "react-router-dom";
import PropertiesListView from "@/components/PropertiesListView";
import Heading from "../components/Heading";
import { usePropertiesStore } from "@/stores/propertiesStore";

export default function PropertiesPage() {
  const { loading, properties, total, getProperties } = usePropertiesStore();
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

  useEffect(() => {
    getProperties(params);
  }, [params, getProperties]);

  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
      <Heading title="Browse Properties" />
      <PropertiesListView
        properties={properties}
        loading={loading}
        baseUrl={`/properties?${new URLSearchParams(
          paramsWithoutPagination
        ).toString()}`}
        page={Number(params.page) || 1}
        perPage={Number(params.perPage) || 10}
        total={total}
      />
    </div>
  );
}
