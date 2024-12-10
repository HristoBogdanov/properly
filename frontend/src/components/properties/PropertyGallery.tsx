import { Property } from "@/types/property";

export default function PropertyGallery({ property }: { property: Property }) {
  const coverImage = property.images[0];

  return (
    <div className="p-10 w-full bg-[#F5F7FB] rounded-lg flex flex-col">
      <div className="flex aspect-video overflow-hidden">
        <img
          src={coverImage.path}
          alt={coverImage.name}
          className="object-cover opacity-90 hover:opacity-100 hover:scale-110 h-full w-full transition-all duration-500 ease-in-out"
        />
      </div>
      <div className="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3">
        {property.images.slice(1).map((image) => (
          <img
            key={image.name}
            src={image.path}
            alt={image.name}
            className="aspect-video w-full object-cover opacity-90 hover:opacity-100 transition-all duration-500 ease-in-out cursor-pointer"
          ></img>
        ))}
      </div>
    </div>
  );
}
