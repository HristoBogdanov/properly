import { useImagesStore } from "@/stores/imagesStore";

export default function HomePage() {
  const images = useImagesStore((state) => state.images);
  const loading = useImagesStore((state) => state.loading);
  console.log(images);

  if (loading) {
    return <div>loading...</div>;
  }
  return <div>home</div>;
}
