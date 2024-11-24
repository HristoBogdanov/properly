import CloudinaryUploadWidget from "./CloudinaryUploadWidget";

export default function Upload({ type }: { type: "regular" | "icon" }) {
  const cloudName = import.meta.env.VITE_CLOUDINARY_CLOUD_NAME;
  const uploadPreset = import.meta.env.VITE_CLOUDINARY_PRESET;
  const folder =
    type === "regular"
      ? import.meta.env.VITE_CLOUDINARY_FOLDER
      : import.meta.env.VITE_CLOUDINARY_ICON_FOLDER;

  const uwConfig = {
    cloudName,
    uploadPreset,
    folder,
  };

  return <CloudinaryUploadWidget uwConfig={uwConfig} />;
}
