'use client'

import { Mail, ClipboardList, Users } from 'lucide-react'

export const HowItWorks = () => {
  return (
    <section id="how-it-works" className="relative py-28 px-4 bg-[#F9F7F6] overflow-hidden">
      {/* Decorative blur */}
      <div className="absolute top-0 left-0 w-32 h-32 bg-pink-100 rounded-full opacity-30 blur-3xl -z-10"></div>

      {/* Header */}
      <div className="max-w-3xl mx-auto text-center mb-16">
        <h2 className="text-4xl font-extrabold text-darkText mb-4">
          How It Works with <span className="text-pink-500">PaSho</span>
        </h2>
        <p className="text-lg text-gray-700 italic">
          Your productivity in 3 simple steps — <span className="text-pink-500 font-semibold">Sign Up. Create. Collaborate.</span>
        </p>
      </div>

      {/* Timeline */}
      <div className="relative max-w-3xl mx-auto before:absolute before:left-1/2 before:top-0 before:bottom-0 before:w-1 before:bg-pink-200 before:-translate-x-1/2">
        {[
          { id: 1, icon: <Mail className="w-6 h-6 text-pink-500" />, title: 'Sign Up', desc: 'Create your free account in seconds and get started with PaSho.' },
          { id: 2, icon: <ClipboardList className="w-6 h-6 text-pink-500" />, title: 'Create Board', desc: 'Set up your first board, organize tasks, and invite your team.' },
          { id: 3, icon: <Users className="w-6 h-6 text-pink-500" />, title: 'Collaborate', desc: 'Assign tasks, communicate, and track progress together.' },
        ].map((step, index) => (
          <div key={step.id} className={`relative mb-12 flex ${index % 2 === 0 ? 'justify-start' : 'justify-end'}`}>
            <div className="w-1/2 px-4">
              <div className="bg-white border border-pink-100 rounded-xl shadow-md p-5 transition hover:-translate-y-1 hover:shadow-lg">
                <div className="flex items-center mb-2 space-x-2">
                  {step.icon}
                  <h3 className="text-lg font-bold text-darkText">{step.title}</h3>
                </div>
                <p className="text-[#5A5A5A] text-sm">{step.desc}</p>
              </div>
            </div>
            <div className="absolute top-1 left-1/2 -translate-x-1/2 w-4 h-4 bg-pink-500 rounded-full border-4 border-white shadow-md"></div>
          </div>
        ))}
      </div>
    </section>
  )
}
