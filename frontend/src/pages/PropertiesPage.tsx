import PropertiesListView from "@/components/properties/PropertiesListView";
import Heading from "../components/common/Heading";
import SimpleFilter from "@/components/common/SimpleFilter";

export default function PropertiesPage() {
  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
      <div className="flex flex-col lg:flex-row w-full justify-between items-center">
        <Heading title="Browse Properties" />
        <SimpleFilter />
      </div>
      <PropertiesListView />
    </div>
  );
}
