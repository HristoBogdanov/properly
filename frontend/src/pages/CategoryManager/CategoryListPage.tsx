import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import { Link } from "react-router-dom";
import { MdModeEdit } from "react-icons/md";
import { IoMdInformationCircleOutline } from "react-icons/io";
import { RiDeleteBin5Line } from "react-icons/ri";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { Category } from "@/types/category";
import { useEffect } from "react";

export default function CategoryListPage() {
  const { categories, getCategories, removeCategory } = useCategoriesStore();

  const deleteItem = async (id: string) => {
    await removeCategory(id);
  };

  useEffect(() => {
    getCategories();
  }, [getCategories]);

  return (
    <div className="w-full flex flex-col justify-center items-center gap-10 my-20">
      <Heading title="Manage Categories" />
      <div className="flex w-full flex-col gap-4 p-4 text-xl">
        {categories.map((category: Category) => (
          <div
            key={category.id}
            className="w-full flex justify-between items-center"
          >
            {category.title}
            <div className="flex gap-2">
              <Link
                to={`/dashboard/categories/information/${category.id}`}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
              >
                <IoMdInformationCircleOutline />
              </Link>
              <Link
                to={`/dashboard/categories/edit/${category.id}`}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out"
              >
                <MdModeEdit />
              </Link>
              <RiDeleteBin5Line
                onClick={() => deleteItem(category.id)}
                className="text-2xl hover:text-primary transition-all duration-300 ease-in-out cursor-pointer"
              />
            </div>
          </div>
        ))}
      </div>
      <div className="w-full flex justify-between items-center">
        <Link to="/dashboard/categories/add">
          <CustomButton text="Add Property" />
        </Link>
      </div>
    </div>
  );
}
