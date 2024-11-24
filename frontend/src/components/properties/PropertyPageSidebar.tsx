import { Property } from "@/types/property";
import PropertyAsideHeader from "./PropertyAsideHeader";
import RecentProperties from "./RecentProperties";

export default function PropertyPageSidebar({
  property,
}: {
  property: Property;
}) {
  return (
    <aside className="col-span-4 md:col-span-1 flex flex-col gap-10">
      <PropertyAsideHeader property={property} />
      <RecentProperties />
    </aside>
  );
}
