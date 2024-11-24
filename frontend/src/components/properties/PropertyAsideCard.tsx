import { Property } from "@/types/property";
import { Link } from "react-router-dom";

export default function PropertyAsideCard({
  property,
}: {
  property: Property;
}) {
  return (
    <div className="w-full flex gap-2 group cursor-pointer relative">
      <div className="w-1/2 aspect-video overflow-hidden">
        <img
          src={property.images[0].path}
          alt={property.images[0].name}
          className="w-full h-full object-cover group-hover:scale-110 transition-all duration-300 ease-out"
        />
      </div>
      <div className="flex flex-col w-full gap-2">
        <h3 className="text-xl md:text-lg line-clamp-1 group-hover:text-primary transition-all duration-300 ease-out">
          {property.title}
        </h3>
        <p className="text-gray-400 text-lg md:text-base">$ {property.price}</p>
      </div>
      <Link
        className="absolute w-full h-full inset-0"
        to={`/properties/${property.id}`}
      />
    </div>
  );
}
