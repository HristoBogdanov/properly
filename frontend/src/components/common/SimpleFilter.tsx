import { simpleSeacrhPropertySchema } from "@/lib/schemas";
import { zodResolver } from "@hookform/resolvers/zod";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate, useSearchParams } from "react-router-dom";
import { z } from "zod";
import { useEffect } from "react";
import Dropdown from "../inputs/Dropdown";
import Input from "../inputs/Input";
import CustomButton from "./CustomButton";

type FormData = z.infer<typeof simpleSeacrhPropertySchema>;

export default function SimpleFilter() {
  const methods = useForm<FormData>({
    resolver: zodResolver(simpleSeacrhPropertySchema),
  });

  const [params] = useSearchParams();
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

  useEffect(() => {
    const defaultValues: Partial<FormData> = {};
    params.forEach((value, key) => {
      defaultValues[key as keyof FormData] = value as any;
    });
    methods.reset(defaultValues);
  }, [params, methods]);

  const onSubmit = (data: FormData) => {
    const filteredData = Object.fromEntries(
      Object.entries(data).filter(([_, value]) => value !== "") // eslint-disable-line
    );
    navigate(
      `/properties?${new URLSearchParams(filteredData as any).toString()}`
    );
  };

  return (
    <div className="w-fit p-3 border rounded-lg max-lg:w-full">
      <div className="rounded-lg bg-white p-6">
        <FormProvider {...methods}>
          <form
            className="flex flex-col lg:flex-row justify-center items-center gap-3 w-full lg:w-fit text-lg text-black"
            onSubmit={methods.handleSubmit(onSubmit)}
          >
            <Input
              id="search"
              name="search"
              showError={false}
              classes="border border-[#d5d5d2] rounded-md max-lg:w-full"
            />
            <Dropdown
              id="sortBy"
              classes="border border-[#d5d5d2] rounded-md"
              options={sortOptions}
              placeholder="Order by"
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
