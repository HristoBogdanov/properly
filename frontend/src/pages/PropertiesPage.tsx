import SimpleFilter from "@/components/common/SimpleFilter";
import PropertiesListView from "@/components/properties/PropertiesListView";
import ProtectedRoute from "@/routes/ProtectedRoute";
import Heading from "../components/common/Heading";

export default function PropertiesPage() {
  return (
    <ProtectedRoute>
      <div className="mt-[200px] container mx-auto flex flex-col justify-center items-center gap-10 lg:gap-20 mb-10 lg:mb-20 px-6">
        <div className="flex flex-col xl:flex-row w-full justify-between items-center gap-10">
          <Heading title="Browse Properties" />
          <SimpleFilter />
        </div>
        <PropertiesListView />
      </div>
    </ProtectedRoute>
  );
}
