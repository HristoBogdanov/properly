import { useFeaturesStore } from "@/stores/featureStore";

export default function HomePage() {
  const features = useFeaturesStore((state) => state.features);
  const loading = useFeaturesStore((state) => state.loading);
  console.log(features);

  if (loading) {
    return <div>loading...</div>;
  }
  return <div>home</div>;
}
