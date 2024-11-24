import { CreateImage } from "@/types/image";

export default function LockedImageGrid({ images }: { images: CreateImage[] }) {
  return (
    <div className="grid grid-cols-4 gap-2">
      {images.map((image: CreateImage) => (
        <img
          key={image.path}
          src={image.path}
          alt={image.name}
          className="w-full aspect-video object-cover"
        ></img>
      ))}
    </div>
  );
}
