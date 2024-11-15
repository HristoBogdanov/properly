import { FormProvider, useForm } from "react-hook-form";
import Input from "../inputs/Input";
import Dropdown from "../inputs/Dropdown";
import { z } from "zod";
import { simpleSeacrhPropertySchema } from "@/lib/schemas";
import { zodResolver } from "@hookform/resolvers/zod";
import CustomButton from "./CustomButton";
import { useNavigate } from "react-router-dom";

type FormData = z.infer<typeof simpleSeacrhPropertySchema>;

export default function SimpleFilter() {
  const methods = useForm<FormData>({
    resolver: zodResolver(simpleSeacrhPropertySchema),
  });

  const navigate = useNavigate();

  const sortOptions = [
    { value: "price", label: "Price" },
    { value: "area", label: "Area" },
    { value: "yearOfConstruction", label: "Year of construction" },
  ];

  const descendingOptions = [
    { value: "true", label: "Descending" },
    { value: "false", label: "Ascending" },
  ];

  const onSubmit = (data: FormData) => {
    const filteredData = Object.fromEntries(
      Object.entries(data).filter(([_, value]) => value !== "") // eslint-disable-line
    );
    navigate(
      `/properties?${new URLSearchParams(filteredData as any).toString()}`
    );
  };

  return (
    <div className="w-fit p-3 border rounded-lg mx-6">
      <div className="rounded-lg bg-white p-6">
        <FormProvider {...methods}>
          <form
            className="flex flex-col lg:flex-row justify-center items-center gap-3 w-fit text-lg text-black"
            onSubmit={methods.handleSubmit(onSubmit)}
          >
            <Input
              id="search"
              showError={false}
              classes="border border-[#d5d5d2] rounded-md"
            />
            <Dropdown
              id="sortBy"
              classes="border border-[#d5d5d2] rounded-md"
              options={sortOptions}
              placeholder="Sort by"
            />
            <Dropdown
              id="descending"
              classes="border border-[#d5d5d2] rounded-md"
              options={descendingOptions}
              placeholder="Order"
            />
            <CustomButton text="Search" type="submit" classes="max-lg:w-full" />
          </form>
        </FormProvider>
      </div>
    </div>
  );
}
