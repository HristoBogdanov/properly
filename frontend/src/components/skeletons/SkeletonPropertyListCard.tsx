import { Skeleton } from "@/components/ui/skeleton";

export default function SkeletonPropertyListCard() {
  return (
    <div className="relative w-full col-span-1 aspect-video flex flex-col justify-between bg-cover bg-no-repeat p-3 group overflow-hidden">
      <Skeleton className="absolute inset-0 bg-black opacity-40 z-0" />
      <div className="flex w-full gap-2 z-10">
        <Skeleton className="w-20 h-6 rounded-md" />
        <Skeleton className="w-20 h-6 rounded-md" />
      </div>
      <div className="w-full flex flex-col gap-2 z-10">
        <Skeleton className="h-8 w-3/4 rounded-md" />
        <Skeleton className="h-6 w-1/2 rounded-md" />
        <Skeleton className="h-6 w-1/2 rounded-md" />
      </div>
    </div>
  );
}
