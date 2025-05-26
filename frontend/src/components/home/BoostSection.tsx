import Image from 'next/image'
import { CheckCircle } from 'lucide-react'
import Link from 'next/link'
import { Button } from "@/components/ui/button"

export const BoostSection = () => {
  return (
    <section id="boost" className="py-34 px-6 scroll-mt-24 ">
      <div className="max-w-7xl mx-auto grid md:grid-cols-2 gap-16 items-center">
        {/* Картинка слева */}
        <div className="flex justify-center">
          <Image 
            src="/boost-illustration.svg"
            alt="Boost Productivity"
            width={550}
            height={450}
          />
        </div>

        {/* Текст справа */}
        <div className="space-y-8">
          <h2 className="text-5xl font-extrabold text-darkText leading-tight">
            Take Full Control of Your Tasks with <span className="text-pink-500">PaSho</span>
          </h2>

          <p className="text-xl text-gray-700 italic">
            Stay organized, collaborate effortlessly, and make your workflow insanely productive.
          </p>

          <p className="text-lg font-semibold text-pink-600">
            100% Free. No limits. No credit card needed.
          </p>

          <ul className="space-y-3">
            <li className="flex items-center text-gray-700 text-lg">
              <CheckCircle className="text-pink-500 w-6 h-6 mr-3" />
              Minimal & elegant task boards
            </li>
            <li className="flex items-center text-gray-700 text-lg">
              <CheckCircle className="text-pink-500 w-6 h-6 mr-3" />
              Real-time updates across devices
            </li>
            <li className="flex items-center text-gray-700 text-lg">
              <CheckCircle className="text-pink-500 w-6 h-6 mr-3" />
              Integrated with your favorite tools
            </li>
          </ul>
          <Link href="/register">
    <Button className="
      bg-gradient-to-r from-pink-500 to-pink-400 
      hover:from-pink-600 hover:to-pink-500 
      text-white font-extrabold text-xl 
      px-12 py-6 rounded-full shadow-2xl 
      hover:scale-105 transition-all duration-300 ease-in-out
      flex items-center space-x-3
    ">
      Get Started for Free
      <svg className="w-5 h-5" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
        <path strokeLinecap="round" strokeLinejoin="round" d="M9 5l7 7-7 7" />
      </svg>
    </Button>
  </Link>
        </div>
      </div>
    </section>
  )
}
