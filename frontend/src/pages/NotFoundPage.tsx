import CustomButton from "@/components/common/CustomButton";

export default function NotFoundPage() {
  return (
    <div className="pt-[150px] h-screen container mx-auto flex flex-col items-center justify-evenly px-6">
      <div className="flex flex-col justify-center items-center">
        <img
          src="/images/error.png"
          className="w-full max-w-[300px] h-auto mb-10"
        ></img>
        <p className="text-6xl lg:text-7xl font-black">404</p>
        <p className="text-2xl lg:text-3xl font-semibold">NOT FOUND</p>
      </div>
      <CustomButton link="/" text="Home" />
    </div>
  );
}
