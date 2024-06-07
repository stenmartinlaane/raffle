import React from 'react'

const FooterTextComponent = ({
    children,
    header
  }: {
    children: React.ReactNode
    header: string
  }) => {
  return (
    <>
        <div className='w-full h-full'>
            <div className='h-8 text-text-on-footer-light text-3xl pb-12'><h2>{header}</h2></div>
        
            {children}
        </div>
    </>
  )
}

export default FooterTextComponent