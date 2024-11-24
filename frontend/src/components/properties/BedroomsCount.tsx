import { MdOutlineBedroomChild } from "react-icons/md";

export default function BedroomsCount({ bedrooms }: { bedrooms: number }) {
  if (bedrooms === 0 || !bedrooms) {
    return null;
  }
  return (
    <div className="flex items-center gap-2 text-lg lg:text-xl">
      <MdOutlineBedroomChild className="text-primary" />
      <p>{bedrooms} Bedrooms</p>
    </div>
  );
}
