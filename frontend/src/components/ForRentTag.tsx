export default function ForRentTag({ forRent }: { forRent: boolean }) {
  if (!forRent) return null;

  return (
    <div className="w-fit rounded-lg text-white font-semibold p-2 bg-[#6449E7] uppercase text-sm">
      For rent
    </div>
  );
}
