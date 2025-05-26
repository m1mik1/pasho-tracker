// MainHeader.tsx
'use client'

import Link from 'next/link'
import {
  NavigationMenu,
  NavigationMenuList,
  NavigationMenuItem,
  NavigationMenuLink,
} from "@/components/ui/navigation-menu"
import { Button } from "@/components/ui/button"

export const MainHeader = ({ onSignInClick }: { onSignInClick: () => void }) => {
  return (
    <header className="fixed top-0 left-0 w-full bg-white shadow-md rounded-b-2xl flex justify-between items-center px-8 py-4 z-50 ">
      <div className="flex items-center space-x-6">
        <Link href="/" className="text-pink-500 font-extrabold text-2xl tracking-tight">PaSho</Link>

        <NavigationMenu>
          <NavigationMenuList className="space-x-4 text-gray-700 font-bold text-[16px]">
            <NavigationMenuItem>
              <NavigationMenuLink href="#boost" className="hover:text-pink-500 transition">Why PaSho?</NavigationMenuLink>
            </NavigationMenuItem>
            <NavigationMenuItem>
              <NavigationMenuLink href="#how-it-works" className="hover:text-pink-500 transition">How It Works</NavigationMenuLink>
            </NavigationMenuItem>
            <NavigationMenuItem>
              <NavigationMenuLink href="#about" className="hover:text-pink-500 transition">About</NavigationMenuLink>
            </NavigationMenuItem>
          </NavigationMenuList>
        </NavigationMenu>
      </div>

      <div className="flex items-center space-x-4">
        <Button variant="ghost" onClick={onSignInClick} className="hover:text-pink-500 font-medium">
          Sign In
        </Button>
        <Link href="/register">
          <Button className="bg-pink-500 hover:bg-pink-600 text-white font-bold px-6 py-2 rounded-full shadow-md hover:shadow-lg transition-all text-sm">
            Sign Up
          </Button>
        </Link>
      </div>
    </header>
  )
}
