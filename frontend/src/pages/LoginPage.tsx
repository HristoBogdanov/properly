import CustomButton from "@/components/common/CustomButton";
import Input from "@/components/inputs/Input";
import { useAuth } from "@/contexts/useAuth";
import { loginSchema } from "@/lib/schemas";
import { zodResolver } from "@hookform/resolvers/zod";
import { FormProvider, useForm } from "react-hook-form";
import { z } from "zod";

type FormData = z.infer<typeof loginSchema>;

export default function LoginPage() {
  const { loginUser } = useAuth();
  const methods = useForm<FormData>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = (data: FormData) => {
    loginUser(data.username, data.password);
  };

  return (
    <div className="min-h-screen container px-6 mx-auto flex flex-col justify-center items-center">
      <FormProvider {...methods}>
        <form
          onSubmit={methods.handleSubmit(onSubmit)}
          className="bg-primary flex flex-col w-full max-w-[500px] gap-6 md:gap-10 p-6 md:p-10 text-md md:text-lg rounded-xl"
        >
          <h2 className="text-3xl md:text-4xl text-white font-black mx-auto">
            Log in
          </h2>
          <Input id="username" name="username" isRequired={true} />
          <Input
            id="password"
            name="password"
            type="password"
            isRequired={true}
          />
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-4">
            <CustomButton
              text="Log in"
              type="submit"
              variant="secondary"
              classes="w-full"
            />
            <CustomButton text="Register" link="/register" classes="w-full" />
          </div>
        </form>
      </FormProvider>
    </div>
  );
}
