'use client'

import * as React from 'react'
import {
  ArrowDownIcon,
  SearchIcon,
  ZapIcon,
  FilterIcon,
  StarIcon,
  UsersIcon,
  ShareIcon,
  MoreHorizontalIcon,
} from 'lucide-react'
import { Button } from '@/components/ui/button'

interface BoardHeaderProps {
  title: string
  boardColor: string
}

// простая утилита потемнить hex-цвет на pct%
function darkenHex(hex: string, pct: number): string {
  let c = hex.replace('#','')
  if(c.length === 3) c = c[0]+c[0]+c[1]+c[1]+c[2]+c[2]
  const num = parseInt(c, 16)
  const amt = Math.round(2.55 * pct)
  let r = (num >> 16) - amt
  let g = ((num >> 8)&0xff) - amt
  let b = (num & 0xff) - amt
  r = Math.max(0,Math.min(255,r))
  g = Math.max(0,Math.min(255,g))
  b = Math.max(0,Math.min(255,b))
  return '#' + ((1<<24)|(r<<16)|(g<<8)|b).toString(16).slice(1)
}

export function BoardHeader({ title, boardColor }: BoardHeaderProps) {
  const bg = darkenHex(boardColor, 15) // сделаем чуть темнее
  const fg = '#fff'

  return (
    <header
      className="
        fixed inset-x-0 top-13    /* ↓3rem = 48px */
        z-40
        flex items-center justify-between
        h-14                      /* тоже 3rem */
        px-6    
        py-4             /* если нужен внутренний отступ контента */
      "
      style={{ background: bg }}
    >
      <div className="flex items-center gap-2">
        <h1 className="text-lg font-semibold" style={{ color: fg }}>
          {title}
        </h1>
        <ArrowDownIcon className="w-5 h-5 cursor-pointer hover:opacity-80" style={{ color: fg }}/>
      </div>

      <div className="flex items-center gap-3">
        {[ SearchIcon, ZapIcon, FilterIcon, StarIcon, UsersIcon ].map((Icon, i) => (
          <Icon
            key={i}
            className="w-5 h-5 cursor-pointer hover:opacity-80"
            style={{ color: fg }}
          />
        ))}

        <Button
          className="flex items-center gap-1 px-3 py-1 text-sm"
          style={{ background: fg, color: bg }}
        >
          <ShareIcon className="w-4 h-4" />
          Share
        </Button>

        <Button variant="ghost" className="p-2 hover:bg-white/20">
          <MoreHorizontalIcon className="w-5 h-5" style={{ color: fg }}/>
        </Button>
      </div>
    </header>
  )
}
