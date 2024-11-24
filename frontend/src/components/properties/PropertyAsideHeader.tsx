import { formatDate } from "@/helpers/formatDate";
import { Property } from "@/types/property";

export default function PropertyAsideHeader({
  property,
}: {
  property: Property;
}) {
  return (
    <div className="w-full flex flex-col gap-6 border-b border-gray-300 pb-6">
      <div className="flex justify-between items-center gap-2 w-full">
        <h3 className="text-xl font-semibold text-primary">Listed on:</h3>
        <p className="text-lg">{formatDate(property.createdAt)}</p>
      </div>
      <div className="flex justify-between items-center gap-2 w-full">
        <h3 className="text-xl font-semibold text-primary">Owner:</h3>
        <p className="text-lg">{property.ownerName}</p>
      </div>
    </div>
  );
}
