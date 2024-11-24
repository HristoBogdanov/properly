import { useImagesStore } from "@/stores/imagesStore";
import { CreateImage } from "@/types/image";
import { createContext, useEffect, useState } from "react";
import { toast } from "react-toastify";
import { BsCloudUpload } from "react-icons/bs";

// Declare the cloudinary property on the window object to avoid TypeScript errors
declare global {
  interface Window {
    cloudinary: any;
  }
}

type CloudinaryUploadWidgetProps = {
  uwConfig: any;
};

// Create a context to manage the script loading state
const CloudinaryScriptContext = createContext({ loaded: false });

function CloudinaryUploadWidget({ uwConfig }: CloudinaryUploadWidgetProps) {
  const [loaded, setLoaded] = useState(false);

  const { addImage, addImageToAddToProperty } = useImagesStore();

  useEffect(() => {
    // Check if the Cloudinary script is loaded
    if (window.cloudinary) {
      setLoaded(true);
    } else {
      const script = document.createElement("script");
      script.src = "https://upload-widget.cloudinary.com/global/all.js";
      script.async = true;
      script.onload = () => setLoaded(true);
      document.body.appendChild(script);
    }
  }, []);

  const initializeCloudinaryWidget = () => {
    if (loaded) {
      const myWidget = window.cloudinary.createUploadWidget(
        uwConfig,
        (error: any, result: any) => {
          if (!error && result && result.event === "success") {
            if (result.info.url && result.info.display_name) {
              const image: CreateImage = {
                name: result.info.display_name,
                path: result.info.url,
              };
              addImage(image);
              addImageToAddToProperty(image);
              toast.success("Image uploaded successfully");
            } else {
              toast.error("Error uploading image");
            }
          }
        }
      );

      myWidget.open();
    }
  };

  return (
    <CloudinaryScriptContext.Provider value={{ loaded }}>
      <button
        id="upload_widget"
        type="button"
        className="border border-primary w-fit p-3 min-w-[200px] h-fit text-primary rounded-md flex justify-center items-center gap-2 hover:text-white hover:bg-primary transition-all duration-300 ease-in-out"
        onClick={initializeCloudinaryWidget}
        disabled={!loaded}
      >
        <BsCloudUpload />
        {loaded ? "Upload Images" : "Loading..."}
      </button>
    </CloudinaryScriptContext.Provider>
  );
}

export default CloudinaryUploadWidget;
export { CloudinaryScriptContext };
