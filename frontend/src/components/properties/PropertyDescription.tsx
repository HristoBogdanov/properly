import { Property } from "@/types/property";
import BathroomsCount from "./BathroomsCount";
import BedroomsCount from "./BedroomsCount";
import FurnishedTag from "./FurnishedTag";
import YearOfConstruction from "./YearOfConstruction";

export default function PropertyDescription({
  property,
}: {
  property: Property;
}) {
  return (
    <div className="w-full flex flex-col justify-center items-center gap-10">
      <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
        <h3 className="text-2xl font-semibold pb-4 subheading">Description</h3>
        <p className="text-xl">{property.description}</p>
      </div>
      <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
        <h3 className="text-2xl font-semibold pb-4 subheading">Details</h3>
        <div className="w-full grid grid-cols-2 md:grid-cols-3 gap-y-10">
          <YearOfConstruction
            yearOfConstruction={property.yearOfConstruction}
          />
          <BedroomsCount bedrooms={property.bedrooms} />
          <BathroomsCount bathrooms={property.bathrooms} />
          <FurnishedTag isFurnished={property.isFurnished} />
        </div>
      </div>
      <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
        <h3 className="text-2xl font-semibold pb-4 subheading">Categories</h3>
        <p className="text-xl flex flex-wrap gap-6 items-center">
          {property.categories.map((cat) => (
            <p key={cat.id}>{cat.title}</p>
          ))}
        </p>
      </div>
      <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
        <h3 className="text-2xl font-semibold pb-4 subheading">Features</h3>
        <p className="text-xl flex flex-wrap gap-6 items-center">
          {property.features.map((feature) => (
            <div
              key={feature.id}
              className="flex justify-center items-center gap-1"
            >
              <img
                src={feature.image.path}
                alt={feature.image.name}
                className="w-6 h-6 object-cover"
              />
              <p>{feature.title}</p>
            </div>
          ))}
        </p>
      </div>
    </div>
  );
}
