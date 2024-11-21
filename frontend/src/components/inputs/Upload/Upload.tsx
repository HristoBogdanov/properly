import { useState } from "react";
import CloudinaryUploadWidget from "./CloudinaryUploadWidget";
import { Cloudinary } from "@cloudinary/url-gen";
import { AdvancedImage } from "@cloudinary/react";

export default function Upload() {
  const [publicId, setPublicId] = useState("");

  const cloudName = import.meta.env.VITE_CLOUDINARY_CLOUD_NAME;
  const uploadPreset = import.meta.env.VITE_CLOUDINARY_PRESET;
  const folder = import.meta.env.VITE_CLOUDINARY_FOLDER;

  const uwConfig = {
    cloudName,
    uploadPreset,
    folder,
  };

  // Create a Cloudinary instance and set your cloud name.
  const cld = new Cloudinary({
    cloud: {
      cloudName,
    },
  });

  const myImage = cld.image(publicId);

  return (
    <div className="App">
      <CloudinaryUploadWidget uwConfig={uwConfig} setPublicId={setPublicId} />
      {publicId && <AdvancedImage cldImg={myImage} />}
    </div>
  );
}
