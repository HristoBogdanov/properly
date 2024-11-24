import { Property } from "@/types/property";
import ForRentTag from "./ForRentTag";
import ForSaleTag from "./ForSaleTag";
import { CiLocationOn } from "react-icons/ci";

export default function PropertyHeader({ property }: { property: Property }) {
  return (
    <div className="w-full flex justify-between">
      <div className="flex flex-col gap-2">
        <div className="flex justify-center gap-4">
          <h2 className="text-3xl lg:text-4xl font-bold">{property.title}</h2>
          <ForSaleTag forSale={property.forSale} />
          <ForRentTag forRent={property.forRent} />
        </div>
        <div className="text-gray-400 flex items-center gap-2 text-xl">
          <CiLocationOn />
          {property.address}
        </div>
      </div>
      <div className="flex flex-col items-center">
        <p className="text-2xl lg:text-3xl text-primary font-semibold">
          ${property.price}
        </p>
        <p className="w-full text-gray-600 text-2xl text-right">
          {property.area} sq ft
        </p>
      </div>
    </div>
  );
}
