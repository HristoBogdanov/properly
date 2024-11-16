export default function Heading({ title }: { title: string }) {
  const [firstPart, secondPart] = title.split(" ");

  return (
    <div className="w-full flex gap-3 h-[84px]">
      <div className="w-4 h-full rounded-md bg-primary"></div>
      <div className="w-full flex flex-col gap-2">
        <h2 className="text-lg font-semibold uppercase">{firstPart}</h2>
        <h2 className="text-4xl lg:text-5xl font-black text-primary uppercase">
          {secondPart}
        </h2>
      </div>
    </div>
  );
}
