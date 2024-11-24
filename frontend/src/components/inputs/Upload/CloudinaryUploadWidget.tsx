import { useImagesStore } from "@/stores/imagesStore";
import { CreateImage } from "@/types/image";
import { createContext, useEffect, useState } from "react";
import { toast } from "react-toastify";

// Declare the cloudinary property on the window object to avoid TypeScript errors
declare global {
  interface Window {
    cloudinary: any;
  }
}

type CloudinaryUploadWidgetProps = {
  uwConfig: any;
  setPublicId: any;
};

// Create a context to manage the script loading state
const CloudinaryScriptContext = createContext({ loaded: false });

function CloudinaryUploadWidget({
  uwConfig,
  setPublicId,
}: CloudinaryUploadWidgetProps) {
  const [loaded, setLoaded] = useState(false);

  const { addImage, imagesToAddToProperty } = useImagesStore();

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
              imagesToAddToProperty.push(image);
              toast.success("Image uploaded successfully");
            } else {
              toast.error("Error uploading image");
            }
            setPublicId(result.info.public_id);
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
        className="cloudinary-button"
        onClick={initializeCloudinaryWidget}
        disabled={!loaded}
      >
        {loaded ? "Upload" : "Loading..."}
      </button>
    </CloudinaryScriptContext.Provider>
  );
}

export default CloudinaryUploadWidget;
export { CloudinaryScriptContext };
