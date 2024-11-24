import PreviewImageGrid from "../properties/PreviewImageGrid";
import Upload from "./Upload/Upload";

export default function UploadSection() {
  return (
    <div className="flex flex-col lg:flex-row w-full justify-between gap-10">
      <Upload />
      <PreviewImageGrid />
    </div>
  );
}
