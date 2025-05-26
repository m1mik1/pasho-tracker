'use client'

import Image from 'next/image'
import { Sparkles } from 'lucide-react'

export const AboutProject = () => {
  return (
    <section id="about" className="relative py-24 px-6 bg-gradient-to-br from-[#FFF0F5] to-[#FAF9F6] overflow-hidden">
      {/* Декор */}
      <div className="absolute top-0 right-0 w-64 h-64 bg-pink-100 rounded-full opacity-20 blur-3xl -z-10"></div>

      <div className="max-w-6xl mx-auto grid md:grid-cols-2 gap-12 items-center">
        {/* Текстовая часть */}
        <div className="space-y-6 border-l-4 border-pink-200 pl-6">
          <h2 className="text-4xl font-extrabold text-darkText">
            About <span className="text-pink-500">PaSho</span> Project
          </h2>

          <div className="flex items-center text-pink-500 font-semibold italic">
            <Sparkles className="w-5 h-5 mr-2" />
            A tool designed to spark your team’s flow.
          </div>

          <p className="text-lg text-gray-700 italic">
            A simple yet powerful platform to make your team’s productivity bloom beautifully.
          </p>

          <p className="text-md text-gray-700 leading-relaxed">
            <span className="font-bold text-pink-500">PaSho</span> is not just a task manager — it’s a creative space for collaboration.
            Inspired by minimal design and powered by a focus-enhancing pink aesthetic.
          </p>

          <p className="text-sm text-gray-600">
            Crafted by <span className="font-bold text-darkText">Pastushenko Rostyslav</span>, 
            student at <span className="font-bold text-darkText">Kyiv Polytechnic Institute (KPI)</span>.
          </p>

          <p className="text-sm text-gray-600">
            Backend: <span className="font-bold">ASP.NET Core</span> with clean architecture, REST APIs, and real-time power.
          </p>

          <p className="text-sm text-gray-600">
            Fully <span className="font-bold text-pink-500">open-source</span>. Dive into the code:
          </p>

          <a href="https://github.com/m1mik1/pasho-tracker" target="_blank" className="text-pink-500 font-bold hover:underline block">
            github.com/m1mik1/pasho-tracker
          </a>

          <p className="text-sm text-gray-500">
            Contact: <a href="mailto:lord3d91@gmail.com" className="text-pink-500 hover:underline">lord3d91@gmail.com</a>
          </p>
        </div>

        {/* Картинка */}
        <div className="flex justify-center relative">
          <div className="absolute -top-6 -left-6 w-24 h-24 bg-pink-100 rounded-full blur-2xl opacity-30 -z-10"></div>
          <Image 
            src="/about-illustration.svg"
            alt="PaSho Illustration"
            width={480}
            height={380}
          />
        </div>
      </div>
    </section>
  )
}
