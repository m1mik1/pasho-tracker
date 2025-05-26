'use client'

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { LayoutGrid, Home, Square } from 'lucide-react'
import Link from 'next/link'

const PINK = '#e91e63'

export const AppSwitcher = () => (
  <DropdownMenu>
    <DropdownMenuTrigger asChild>
      <button
        className="h-9 w-9 flex items-center justify-center rounded-md hover:bg-gray-100 transition"
        aria-label="Apps"
      >
        <LayoutGrid className="w-5 h-5 text-pink-600" />
      </button>
    </DropdownMenuTrigger>

    <DropdownMenuContent side="bottom" align="start" className="w-64 bg-white shadow-lg border border-gray-200">
      {/* Заголовок */}
      <p className="px-3 pt-3 pb-2 text-xs font-semibold text-gray-500">
        More from PaSho
      </p>

      {/* ——— Блок “Home” ——— */}
      <DropdownMenuItem asChild className="gap-3 py-3 px-3 hover:bg-gray-50">
        <Link href="/">
          <Home className="w-5 h-5 shrink-0 text-pink-600" />
          <span className="text-gray-800">Home</span>
        </Link>
      </DropdownMenuItem>

      {/* ——— Блок “PaSho” ——— */}
      <DropdownMenuItem asChild className="gap-3 py-3 px-3 hover:bg-gray-50">
        <Link href="/dashboard">
          {/* используем тот же розовый, чтобы подчеркнуть текущее приложение */}
          <Square className="w-5 h-5 shrink-0" style={{ color: PINK }} />
          <span className="text-gray-800 font-medium">PaSho</span>
        </Link>
      </DropdownMenuItem>

      <div className="my-2 border-t border-gray-200" />

      {/* ——— Discover ——— */}
      <p className="px-3 pt-2 pb-1 text-xs font-semibold text-gray-500">
        Discover more
      </p>

      <DropdownMenuItem asChild className="gap-3 py-3 px-3 hover:bg-gray-50">
        <Link href="/apps">
          <LayoutGrid className="w-5 h-5 shrink-0 text-pink-600" />
          <span className="text-gray-800">All PaSho apps</span>
        </Link>
      </DropdownMenuItem>
    </DropdownMenuContent>
  </DropdownMenu>
)
