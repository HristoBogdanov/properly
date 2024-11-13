import { Property } from "@/types/property";
import ForSaleTag from "./ForSaleTag";
import ForRentTag from "./ForRentTag";
import { Link } from "react-router-dom";

export default function PropertyCard({ property }: { property: Property }) {
  return (
    <>
      <div
        className="relative w-full col-span-1 aspect-video flex flex-col justify-between bg-cover bg-no-repeat p-3 group overflow-hidden"
        style={{ backgroundImage: `url(${property.images[0].path})` }}
      >
        <div className="absolute inset-0 bg-black opacity-40 z-0 group-hover:-translate-y-[100%] transition-all duration-500 ease-in-out"></div>
        <div className="flex w-full gap-2 z-10">
          <ForSaleTag forSale={property.forSale} />
          <ForRentTag forRent={property.forRent} />
        </div>
        <div className="w-full flex flex-col gap-2 z-10 group-hover:-translate-y-[20%] transition-all duration-500 ease-in-out">
          <h3 className="text-white text-2xl font-bold">{property.title}</h3>
          <p className="text-white font-bold">${property.price}</p>
          <p className="text-white font-bold">{property.address}</p>
        </div>
        <Link
          to={`/properties/${property.id}`}
          className="absolute inset-0 z-10"
        ></Link>
      </div>
    </>
  );
}
