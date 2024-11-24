import { MdOutlineBathroom } from "react-icons/md";

export default function BathroomsCount({ bathrooms }: { bathrooms: number }) {
  if (bathrooms === 0 || !bathrooms) {
    return null;
  }
  return (
    <div className="flex items-center gap-2 text-lg lg:text-xl">
      <MdOutlineBathroom className="text-primary" />
      <p>{bathrooms} Bathrooms</p>
    </div>
  );
}
