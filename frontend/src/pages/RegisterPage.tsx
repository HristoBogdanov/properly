import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useAuth } from "@/contexts/useAuth";
import CustomButton from "@/components/CustomButton";
import Input from "@/components/Input";
import { registerSchema } from "@/lib/schemas";

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
          className="bg-primary flex flex-col w-fit gap-10 p-6 md:p-10 text-lg md:text-xl rounded-xl"
        >
          <Input id="username" isRequired={true} />
          <Input id="email" type="email" isRequired={true} />
          <Input id="password" type="password" isRequired={true} />
          <div className="w-full flex-col md:flex-row flex justify-between gap-5">
            <CustomButton
              text="Register"
              type="submit"
              classes="max-md:w-full"
            />
            <CustomButton
              text="Log in"
              link="login"
              variant="secondary"
              classes="max-md:w-full"
            />
          </div>
        </form>
      </FormProvider>
    </div>
  );
}