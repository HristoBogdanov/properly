import { useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import PropertiesListView from "@/components/PropertiesListView";
import Heading from "../components/Heading";
import { usePropertiesStore } from "@/stores/propertiesStore";

export default function PropertiesPage() {
  const { loading, properties, total, getProperties } = usePropertiesStore();
  const [searchParams] = useSearchParams();

  useEffect(() => {
    const params = Object.fromEntries(searchParams.entries());
    getProperties(params);
  }, [searchParams, getProperties]);

  console.log(total);

  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
      <Heading title="Browse Properties" />
      <PropertiesListView
        properties={properties}
        loading={loading}
        total={total}
      />
    </div>
  );
}
