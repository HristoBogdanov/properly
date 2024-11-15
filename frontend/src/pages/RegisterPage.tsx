import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAuth } from "@/contexts/useAuth";
import CustomButton from "@/components/common/CustomButton";
import Input from "@/components/inputs/Input";
import { registerSchema } from "@/lib/schemas";
import { z } from "zod";

type FormData = z.infer<typeof registerSchema>;

export default function RegisterPage() {
  const { registerUser } = useAuth();
  const methods = useForm<FormData>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = (data: FormData) => {
    registerUser(data.email, data.username, data.password);
  };

  return (
    <div className="min-h-screen container px-6 mx-auto flex flex-col justify-center items-center">
      <FormProvider {...methods}>
        <form
          onSubmit={methods.handleSubmit(onSubmit)}
          className="bg-primary flex flex-col w-full max-w-[500px] gap-6 md:gap-10 p-6 md:p-10 text-md md:text-lg rounded-xl"
        >
          <h2 className="text-3xl md:text-4xl text-white font-black mx-auto">
            Register
          </h2>
          <Input id="username" name="username" isRequired={true} />
          <Input id="email" name="email" type="email" isRequired={true} />
          <Input
            id="password"
            name="password"
            type="password"
            isRequired={true}
          />
          <div className="w-full flex-col md:flex-row flex justify-between gap-5">
            <CustomButton text="Register" type="submit" classes="w-full" />
            <CustomButton
              text="Log in"
              link="/login"
              variant="secondary"
              classes="w-full"
            />
          </div>
        </form>
      </FormProvider>
    </div>
  );
}
