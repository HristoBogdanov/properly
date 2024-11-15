import PropertiesListView from "@/components/properties/PropertiesListView";
import Heading from "../components/common/Heading";

export default function PropertiesPage() {
  return (
    <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
      <Heading title="Browse Properties" />
      <PropertiesListView />
    </div>
  );
}
