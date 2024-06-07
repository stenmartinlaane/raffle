import FormHeader from "./layout/FormHeader";

export default function FormPage({
    children,
  }: {
    children: React.ReactNode
  }) {
    return(
        <>
           <div className="w-full h-full bg-white-background ">
                <FormHeader></FormHeader>
                <div className="flex-1 flex w-full">
                    <div className="w-1/5"></div>
                    {children}
                </div>
            </div>  
        </>
    )
}