// components/tip-popover.tsx
'use client'

import {
  Popover,
  PopoverTrigger,
  PopoverContent,
} from '@/components/ui/popover'
import { Card, CardContent, CardFooter } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { HelpCircle } from 'lucide-react'
import Image from 'next/image'
import * as React from 'react'

const tips = [
  {
    id: 'tip-boards',
    img: '/tips/board-tip.png',             // 500×200 иллюстрация
    title: 'Make boards more powerful with PaSho Widgets',
    cta: { label: 'Browse widgets', href: '/marketplace' },
  },
  {
    id: 'tip-shortcuts',
    img: '/tips/shortcut-tip.png',
    title: 'Press ⌘K any time to open the global command bar',
    cta: { label: 'View all shortcuts', href: '/shortcuts' },
  },
  {
    id: 'tip-mobile',
    img: '/tips/mobile-tip.png',
    title: 'Stay aligned on the go — install PaSho Mobile',
    cta: { label: 'Get the app', href: '/mobile' },
  },
] as const

export function TipPopover() {
  const [index, setIndex] = React.useState(0)
  const tip = tips[index]

  const nextTip = () => setIndex((index + 1) % tips.length)

  return (
    <Popover>
      <PopoverTrigger asChild>
        <button
          className="h-9 w-9 flex items-center justify-center rounded-md hover:bg-gray-100 transition"
          aria-label="Tips"
        >
          <HelpCircle className="w-5 h-5 text-pink-600" />
        </button>
      </PopoverTrigger>

      <PopoverContent
        side="bottom"
        align="end"
        className="p-0 w-[22rem] bg-white border border-gray-200 shadow-xl"
      >
        {/* карточка */}
        <Card className="border-0">
          <CardContent className="p-0">
            <Image
              src={tip.img}
              alt={tip.title}
              width={704}
              height={280}
              className="w-full rounded-t-md"
            />
            <p className="px-4 py-3 text-center font-medium text-gray-800">
              {tip.title}
            </p>
          </CardContent>

          <CardFooter className="flex flex-col gap-2">
            <Button
              variant="link"
              className="text-sm text-[#2563eb] hover:underline"
              asChild
            >
              <a href={tip.cta.href}>{tip.cta.label}</a>
            </Button>

            {/* Divider */}
            <div className="h-px w-full bg-gray-200 my-2" />

            {/* Footer-links */}
            <div className="flex flex-wrap items-center justify-center gap-x-4 gap-y-1 text-xs text-gray-600">
              <a href="/pricing"  className="hover:text-gray-900">Pricing</a>
              <a href="/api"      className="hover:text-gray-900">API</a>
              <a href="/blog"     className="hover:text-gray-900">Blog</a>
              <a href="/privacy"  className="hover:text-gray-900">Privacy</a>
              <a href="/more"     className="hover:text-gray-900">More…</a>
            </div>
          </CardFooter>
        </Card>

        {/* кнопка «ещё один совет» */}
        <div className="px-4 py-2 border-t border-gray-200 text-center">
          <Button
            variant="ghost"
            size="sm"
            className="text-sm text-[#2563eb] hover:bg-gray-50"
            onClick={nextTip}
          >
            Get another tip
          </Button>
        </div>
      </PopoverContent>
    </Popover>
  )
}
