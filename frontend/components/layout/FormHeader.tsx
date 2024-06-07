import React from 'react'

const FormHeader = () => {
  return (
    <>
    <div className='w-full overflow-hidden flex flex-none'>
        <div className='w-1/5 bg-primary flex items-center pb-4'>
            <h1 className='p-2 pb-6 pl-4 text-white text-4xl font-thin text-nowrap'>Osavõtja Info</h1>
        </div>
        <div className="bg-cover bg-center w-4/5 h-auto" style={{ backgroundImage: "url('/images/libled.jpg')", backgroundRepeat: "no-repeat", backgroundSize: "cover" }}></div>
    </div>
    </>
  )
}

export default FormHeader