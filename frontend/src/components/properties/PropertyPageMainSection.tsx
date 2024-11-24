import PropertyHeader from "./PropertyHeader";
import PropertyGallery from "./PropertyGallery";
import PropertyDescription from "./PropertyDescription";
import { Property } from "@/types/property";

export default function PropertyPageMainSection({
  property,
}: {
  property: Property;
}) {
  return (
    <main className="col-span-4 md:col-span-3 flex flex-col justify-center items-center gap-10">
      <PropertyHeader property={property} />
      <PropertyGallery property={property} />
      <PropertyDescription property={property} />
    </main>
  );
}
