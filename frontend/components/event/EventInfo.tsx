import React from 'react'

const EventInfo = ({
    children,
    title
  }: {
    children: React.ReactNode
    title: string
  }) => {
  return (
    <div className='flex flex-col w-full'>
        <div className='bg-primary w-full flex justify-center items-center flex-none'>
            <h2 className='p-4 text-white font-medium'>{title}</h2>
        </div>
        <div className='h-full w-full flex-1 bg-white'>
            {children}
        </div>
    </div>
  )
}

export default EventInfo