import { MdOutlineConstruction } from "react-icons/md";

export default function YearOfConstruction({
  yearOfConstruction,
}: {
  yearOfConstruction: number;
}) {
  return (
    <div className="flex items-center gap-2 text-lg lg:text-xl">
      <MdOutlineConstruction className="text-primary" />
      <p>Year of construction: {yearOfConstruction}</p>
    </div>
  );
}
