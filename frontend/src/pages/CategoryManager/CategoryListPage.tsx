import CustomButton from "@/components/common/CustomButton";
import Heading from "@/components/common/Heading";
import { Link } from "react-router-dom";
import { MdModeEdit } from "react-icons/md";
import { IoMdInformationCircleOutline } from "react-icons/io";
import { RiDeleteBin5Line } from "react-icons/ri";
import { useCategoriesStore } from "@/stores/categoriesStore";
import { Category } from "@/types/category";
import { useEffect, useState } from "react";
import Dialog from "@/components/common/Dialog";

export default function CategoryListPage() {
  const { categories, getCategories, removeCategory } = useCategoriesStore();

  const [isDailogOpen, setIsDailogOpen] = useState<boolean>(false);
  const [categoryId, setCategoryId] = useState<string>("");

  const onDeleteClick = (id: string) => {
    setCategoryId(id);
    setIsDailogOpen(true);
  };

  const deleteItem = async (id: string) => {
    await removeCategory(id);
  };

  useEffect(() => {
    getCategories();
  }, [getCategories]);

  return (
    <>
      <div className="w-full flex flex-col justify-center items-center gap-10 my-20">
        <Heading title="Manage Categories" />
        <div className="flex w-full flex-col gap-2 p-4 text-xl">
          {categories.map((category: Category) => (
            <div
              key={category.id}
              className="w-full flex justify-between p-2 rounded-md cursor-pointer items-center hover:bg-[#ffb1c1] transition-all duration-500 ease-in-out"
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
                  onClick={() => onDeleteClick(category.id)}
                  className="text-2xl hover:text-primary transition-all duration-300 ease-in-out cursor-pointer"
                />
              </div>
            </div>
          ))}
        </div>
        <div className="w-full flex justify-between items-center">
          <Link to="/dashboard/categories/add">
            <CustomButton text="Add Category" />
          </Link>
        </div>
      </div>
      <Dialog
        text="Are you sure you want to delete this category?"
        isDailogOpen={isDailogOpen}
        setIsDailogOpen={setIsDailogOpen}
        onConfirmAction={() => deleteItem(categoryId)}
      />
    </>
  );
}
