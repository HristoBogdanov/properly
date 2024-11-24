import { Property } from "@/types/property";
import { MdOutlinePlace } from "react-icons/md";
import { TbMeterSquare } from "react-icons/tb";
import { FaHouseUser } from "react-icons/fa";
import { formatDate } from "@/helpers/formatDate";
import { Link } from "react-router-dom";
import ForSaleTag from "./ForSaleTag";
import ForRentTag from "./ForRentTag";
import BedroomsCount from "./BedroomsCount";
import BathroomsCount from "./BathroomsCount";
import YearOfConstruction from "./YearOfConstruction";

export default function PropertyListCard({ property }: { property: Property }) {
  return (
    <div className="relative w-full grid grid-cols-1 lg:grid-cols-3 shadow-md border rounded-md overflow-hidden group">
      <div className="w-full aspect-[16/9.7] relative overflow-hidden">
        <div className="absolute top-0 w-full flex items-center justify-between p-4">
          <ForSaleTag forSale={property.forSale} />
          <ForRentTag forRent={property.forRent} />
        </div>
        <img
          src={property.images[0].path}
          alt={property.title}
          className="w-full h-full object-cover group-hover:scale-110 transition-all duration-500 ease-out"
        />
      </div>
      <div className="col-span-2 flex flex-col p-6 gap-2 lg:gap-4">
        <div className="w-full flex items-center justify-between">
          <h3 className="text-2xl font-semibold group-hover:text-primary transition-all duration-300 ease-in-out">
            {property.title}
          </h3>
          <p className="text-xl text-primary">${property.price}</p>
        </div>
        <div className="flex items-center gap-2 text-lg lg:text-xl">
          <MdOutlinePlace className="text-primary" />
          <p>{property.address}</p>
        </div>
        <div className="w-full grid grid-cols-1 lg:grid-cols-2 gap-y-2">
          <BedroomsCount bedrooms={property.bedrooms} />
          <BathroomsCount bathrooms={property.bathrooms} />
        </div>
        <div className="w-full grid grid-cols-1 lg:grid-cols-2 gap-y-2">
          <div className="flex items-center gap-2 text-lg lg:text-xl">
            <TbMeterSquare className="text-primary" />
            <p>{property.area} Square metres</p>
          </div>
          <YearOfConstruction
            yearOfConstruction={property.yearOfConstruction}
          />
        </div>
        <div className="w-full border-t mt-4 pt-4 flex items-center justify-between text-lg lg:text-xl">
          <div className="flex items-center gap-2">
            <FaHouseUser className="text-primary" />
            <p>{property.ownerName}</p>
          </div>
          <div className="flex items-center gap-2">
            <p>{formatDate(property.createdAt)}</p>
          </div>
        </div>
      </div>
      <Link
        to={`/properties/${property.id}`}
        className="absolute inset-0 w-full h-full"
      ></Link>
    </div>
  );
}
