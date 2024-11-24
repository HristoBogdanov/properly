import { Property } from "@/types/property";

export default function PropertyGallery({ property }: { property: Property }) {
  return (
    <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
      <div className="w-full aspect-video overflow-hidden">
        <img
          src={property.images[0].path}
          alt={property.images[1].path}
          className="opacity-90 hover:opacity-100 hover:scale-125 transition-all duration-500 ease-in-out"
        />
      </div>
    </div>
  );
}
