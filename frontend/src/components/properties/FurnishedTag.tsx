import { GiSofa } from "react-icons/gi";

export default function FurnishedTag({
  isFurnished,
}: {
  isFurnished: boolean;
}) {
  if (!isFurnished) {
    return null;
  }
  return (
    <div className="flex items-center gap-2 text-lg lg:text-xl">
      <GiSofa className="text-primary" />
      <p>Property is furnished</p>
    </div>
  );
}
