import { useImagesStore } from "@/stores/imagesStore";
import Upload from "../inputs/Upload/Upload";

export default function AddIconSection() {
  const { imagesToAddToProperty } = useImagesStore();

  return (
    <div className="flex items-center gap-10">
      <h3 className="text-2xl font-semibold my-10">Feature icon</h3>
      <Upload type="icon" />
      {imagesToAddToProperty.length > 0 && (
        <img
          className="w-10 aspect-square object-cover"
          src={imagesToAddToProperty[imagesToAddToProperty.length - 1].path}
          alt={imagesToAddToProperty[imagesToAddToProperty.length - 1].name}
        />
      )}
    </div>
  );
}
