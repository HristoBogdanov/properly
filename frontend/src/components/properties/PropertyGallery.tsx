import { Property } from "@/types/property";

export default function PropertyGallery({ property }: { property: Property }) {
  return (
    <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
      <div className="flex aspect-video overflow-hidden">
        {property.images.map((image) => (
          <img
            key={image.path}
            src={image.path}
            alt={image.name}
            className="object-cover opacity-90 hover:opacity-100 hover:scale-110 h-full w-full transition-all duration-500 ease-in-out"
          />
        ))}
      </div>
    </div>
  );
}
