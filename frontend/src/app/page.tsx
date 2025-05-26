'use client'

import { useSearchParams } from 'next/navigation'
import { useState, useEffect } from 'react'
import { SignInModal } from '@/components/auth/SignInModal'
import { MainHeader } from '@/components/home/MainHeader'
import { BoostSection } from '@/components/home/BoostSection'
import { HowItWorks } from '@/components/home/HowItWorks'
import { AboutProject } from '@/components/home/AboutProject'

export default function HomePage() {
  const searchParams = useSearchParams()
  const showSignIn = searchParams.get('signin') === 'true'

  const [signInOpen, setSignInOpen] = useState(false)

  useEffect(() => {
    if (showSignIn) {
      setSignInOpen(true)
    }
  }, [showSignIn])

  return (
    <>
      <MainHeader onSignInClick={() => setSignInOpen(true)} />
      <SignInModal open={signInOpen} setOpen={setSignInOpen} />
      <BoostSection />
      <HowItWorks />
      <AboutProject />
    </>
  )
}
