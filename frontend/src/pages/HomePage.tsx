import CustomButton from "@/components/common/CustomButton";
import ParralaxContainer from "@/components/common/ParralaxContainer";
import PropertiesSection from "@/components/properties/PropertiesSection";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { useEffect } from "react";

export default function HomePage() {
  const { loading, properties, getProperties } = usePropertiesStore();

  useEffect(() => {
    getProperties({ page: "1", perPage: "12" });
  }, [getProperties]);

  return (
    <>
      <ParralaxContainer />
      <div className="container mx-auto flex flex-col py-10 lg:py-20 justify-center items-end gap-10 px-6">
        <PropertiesSection
          title="Latest Properties"
          properties={properties}
          loading={loading}
        />
        <CustomButton text="Show more" link="/properties" />
      </div>
    </>
  );
}
