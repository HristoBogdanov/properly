import SimpleFilter from "./SimpleFilter";

export default function ParralaxContainer() {
  return (
    <div className="w-100vw h-[90vh] relative overflow-hidden z-10">
      <div
        className="absolute inset-0 z-0 overflow-hidden bg-cover bg-no-repeat  md:bg-fixed"
        style={{
          backgroundImage: `url("/images/hero.jpg")`,
          backgroundPosition: "70% 0%",
        }}
      />
      <div className="absolute inset-0 bg-black opacity-60"></div>
      <div className="absolute inset-0 mx-auto flex flex-col items-center justify-center gap-6 p-4 text-center font-black text-white lg:p-0 text-4xl md:text-5xl">
        <h1>Find Your Dream House</h1>
        <p className="text-lg md:text-xl">
          We have over a million properties for you.
        </p>
        <div className="mx-6">
          <SimpleFilter />
        </div>
      </div>
    </div>
  );
}
