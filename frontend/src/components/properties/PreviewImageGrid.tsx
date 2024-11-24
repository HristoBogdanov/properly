import { useImagesStore } from "@/stores/imagesStore";
import { CreateImage } from "@/types/image";
import { IoCloseCircle } from "react-icons/io5";

export default function PreviewImageGrid() {
  const { imagesToAddToProperty, removeImageFromImagesToAddToProperty } =
    useImagesStore();

  const handleClick = (name: string, path: string) => {
    removeImageFromImagesToAddToProperty(name, path);
  };

  if (imagesToAddToProperty.length === 0) {
    return null;
  }
  return (
    <div className="grid grid-cols-4 gap-2">
      {imagesToAddToProperty.map((image: CreateImage) => (
        <div key={image.path} className="relative w-full aspect-video">
          <img
            src={image.path}
            alt={image.name}
            className="w-full h-full object-cover"
          ></img>
          <button
            type="button"
            onClick={() => handleClick(image.name, image.path)}
            className="absolute top-2 right-2"
          >
            <IoCloseCircle className="text-red-500 scale-150 hover:text-red-700" />
          </button>
        </div>
      ))}
    </div>
  );
}
