import CloudinaryUploadWidget from "./CloudinaryUploadWidget";

export default function Upload() {
  const cloudName = import.meta.env.VITE_CLOUDINARY_CLOUD_NAME;
  const uploadPreset = import.meta.env.VITE_CLOUDINARY_PRESET;
  const folder = import.meta.env.VITE_CLOUDINARY_FOLDER;

  const uwConfig = {
    cloudName,
    uploadPreset,
    folder,
  };

  return <CloudinaryUploadWidget uwConfig={uwConfig} />;
}
