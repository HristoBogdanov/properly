import { usePropertiesStore } from "@/stores/propertiesStore";
import { Property } from "@/types/property";
import Pagination from "../../components/common/Pagination";
import { useEffect, useMemo, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { MdModeEdit } from "react-icons/md";
import { IoMdInformationCircleOutline } from "react-icons/io";
import { RiDeleteBin5Line } from "react-icons/ri";
import Heading from "../../components/common/Heading";
import CustomButton from "../../components/common/CustomButton";
import Dialog from "@/components/common/Dialog";

export default function PropertyListPage() {
  const { pages, properties, getProperties, removeProperty } =
    usePropertiesStore();

  const [searchParams] = useSearchParams();

  const [isDailogOpen, setIsDailogOpen] = useState<boolean>(false);
  const [propertyId, setPropertyId] = useState<string>("");

  const params = useMemo(
    () => Object.fromEntries(searchParams.entries()),
    [searchParams]
  );

  const onDeleteClick = (id: string) => {
    setPropertyId(id);
    setIsDailogOpen(true);
  };

  const deleteItem = async (id: string) => {
    await removeProperty(id);
  };

  useEffect(() => {
    getProperties(params);
  }, [params, getProperties]);

  return (
    <>
      <div className="w-full flex flex-col justify-center items-center gap-10 my-20">
        <Heading title="Manage Properties" />
        <div className="flex w-full flex-col gap-2 p-4 text-xl">
          {properties.map((property: Property) => (
            <div
              key={property.id}
              className="w-full flex justify-between p-2 rounded-md cursor-pointer items-center hover:bg-[#ffb1c1] transition-all duration-500 ease-in-out"
            >
              {property.title}
              <div className="flex gap-2">
                <Link
                  to={`/dashboard/properties/information/${property.id}`}
                  className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
                >
                  <IoMdInformationCircleOutline />
                </Link>
                <Link
                  to={`/dashboard/properties/edit/${property.id}`}
                  className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
                >
                  <MdModeEdit />
                </Link>
                <RiDeleteBin5Line
                  onClick={() => onDeleteClick(property.id)}
                  className="text-2xl hover:text-primary transition-all duration-300 ease-in-out cursor-pointer"
                />
              </div>
            </div>
          ))}
        </div>
        <div className="w-full flex justify-between items-center">
          <Pagination
            baseUrl="/dashboard"
            currentPage={pages.page}
            perPage={pages.perPage}
            totalPages={pages.totalPages}
          />
          <Link to="/dashboard/properties/add">
            <CustomButton text="Add Property" />
          </Link>
        </div>
      </div>
      <Dialog
        text="Are you sure you want to delete this property?"
        isDailogOpen={isDailogOpen}
        setIsDailogOpen={setIsDailogOpen}
        onConfirmAction={() => deleteItem(propertyId)}
      />
    </>
  );
}
