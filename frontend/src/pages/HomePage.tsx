import ParralaxContainer from "@/components/ParralaxContainer";
import PropertiesSection from "@/components/PropertiesSection";
import { usePropertiesStore } from "@/stores/propertiesStore";
import { useEffect, useState } from "react";
import { Property } from "@/types/property";

export default function HomePage() {
  const { loading, getProperties } = usePropertiesStore();
  const [latestProperties, setLatestProperties] = useState<Property[]>([]);
  const [budgetProperties, setBudgetProperties] = useState<Property[]>([]);
  const [highendProperties, setHighendProperties] = useState<Property[]>([]);

  useEffect(() => {
    const fetchProperties = async () => {
      const latestResponse = await getProperties({ page: "1", perPage: "6" });
      const budgetResponse = await getProperties({
        maxPrice: "200000",
        page: "1",
        perPage: "6",
      });
      const highendResponse = await getProperties({
        minPrice: "500000",
        page: "1",
        perPage: "6",
      });

      if (latestResponse) setLatestProperties(latestResponse);
      if (budgetResponse) setBudgetProperties(budgetResponse);
      if (highendResponse) setHighendProperties(highendResponse);
    };

    fetchProperties();
  }, [getProperties]);

  return (
    <>
      <ParralaxContainer />
      <div className="container mx-auto flex flex-col py-10 lg:py-20 justify-center items-center gap-10 lg:gap-20">
        <PropertiesSection
          title="Latest Properties"
          properties={latestProperties}
          loading={loading}
        />
        <PropertiesSection
          title="Budget Properties"
          properties={budgetProperties}
          loading={loading}
        />
        <PropertiesSection
          title="High-end Properties"
          properties={highendProperties}
          loading={loading}
        />
      </div>
    </>
  );
}
