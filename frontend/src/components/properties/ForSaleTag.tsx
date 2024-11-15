export default function ForSaleTag({ forSale }: { forSale: boolean }) {
  if (!forSale) return null;

  return (
    <div className="w-fit rounded-lg text-white font-semibold p-2 bg-primary uppercase text-sm">
      For sale
    </div>
  );
}
