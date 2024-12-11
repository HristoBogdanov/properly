import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import { Link } from "react-router-dom";
import { MdModeEdit } from "react-icons/md";
import { IoMdInformationCircleOutline } from "react-icons/io";
import { RiDeleteBin5Line } from "react-icons/ri";
import { useFeaturesStore } from "@/stores/featuresStore";
import { Feature } from "@/types/feature";
import { useEffect } from "react";

export default function FeatureListPage() {
  const { features, removeFeature, getFeatures } = useFeaturesStore();

  const deleteItem = async (id: string) => {
    await removeFeature(id);
  };

  useEffect(() => {
    getFeatures();
  }, [getFeatures]);

  return (
    <div className="w-full flex flex-col justify-center items-center gap-10 my-20">
      <Heading title="Manage Features" />
      <div className="flex w-full flex-col gap-2 p-4 text-xl">
        {features.map((feature: Feature) => (
          <div
            key={feature.id}
            className="w-full flex justify-between p-2 rounded-md cursor-pointer items-center hover:bg-[#ffb1c1] transition-all duration-500 ease-in-out"
          >
            {feature.title}
            <div className="flex gap-2">
              <Link
                to={`/dashboard/features/information/${feature.id}`}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
              >
                <IoMdInformationCircleOutline />
              </Link>
              <Link
                to={`/dashboard/features/edit/${feature.id}`}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
              >
                <MdModeEdit />
              </Link>
              <RiDeleteBin5Line
                onClick={() => deleteItem(feature.id)}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out cursor-pointer"
              />
            </div>
          </div>
        ))}
      </div>
      <div className="w-full flex justify-between items-center">
        <Link to="/dashboard/features/add">
          <CustomButton text="Add Feature" />
        </Link>
      </div>
    </div>
  );
}
