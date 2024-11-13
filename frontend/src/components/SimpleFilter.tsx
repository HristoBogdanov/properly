import { FormProvider, useForm } from "react-hook-form";
import Input from "./Input";
import { z } from "zod";
import { simpleSeacrhPropertySchema } from "@/lib/schemas";
import { zodResolver } from "@hookform/resolvers/zod";
import CustomButton from "./CustomButton";

type FormData = z.infer<typeof simpleSeacrhPropertySchema>;

export default function SimpleFilter() {
  const methods = useForm<FormData>({
    resolver: zodResolver(simpleSeacrhPropertySchema),
  });

  return (
    <div className="w-fit p-3 border rounded-lg mx-6">
      <div className="rounded-lg bg-white flex justify-center items-center p-6">
        <FormProvider {...methods}>
          <form className="flex justify-center items-center gap-3 w-fit text-lg text-black">
            <Input id="search" classes="border border-[#d5d5d2] rounded-md" />
            <Input id="sortBy" classes="border border-[#d5d5d2] rounded-md" />
            <Input
              id="desending"
              classes="border border-[#d5d5d2] rounded-md"
            />
            <CustomButton text="Search" type="submit" />
          </form>
        </FormProvider>
      </div>
    </div>
  );
}
