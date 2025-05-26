'use client'

import { Avatar, AvatarFallback } from '@/components/ui/avatar'
import { AppSwitcher } from '@/components/dashboard/header/AppSwitcher'
import { SearchBox } from './SearchBox'
import { FeedbackDialog } from './FeedbackDialog'
import { CreateBoard } from './CreateBoardDialog'
import { TipPopover } from './Tip'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import { LogOut, User as UserIcon } from 'lucide-react'
import { Button } from '@/components/ui/button'
import Link from 'next/link'

const PINK = '#e91e63'

export const Header = () => (
  <header
    className="
      fixed inset-x-0 top-0 z-50
      flex items-center justify-between
      px-6 py-2
      bg-white
      border-b border-gray-200
    "
  >
    {/* Лого */}
    <div className="flex items-center gap-4">
      <AppSwitcher />
      <span className="font-extrabold text-2xl text-pink-500">PaSho</span>
    </div>

    {/* Центр: поиск + кнопка */}
    <div className="flex items-center gap-4 mx-auto">
      <SearchBox />
      <CreateBoard />
    </div>

    {/* Правая часть */}
    <div className="flex items-center gap-4">
      <FeedbackDialog />
      <TipPopover />

      {/* Профиль */}
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Avatar className="cursor-pointer">
            <AvatarFallback
              className="font-bold"
              style={{ background: '#f8bbd0', color: PINK }}
            >
              PR
            </AvatarFallback>
          </Avatar>
        </DropdownMenuTrigger>
        <DropdownMenuContent
          side="bottom"
          align="end"
          className="w-48 bg-white border border-gray-200 text-gray-700"
        >
          <DropdownMenuItem asChild>
            <Link
              href="/profile"
              className="flex items-center gap-2 py-2 px-3 hover:bg-gray-100"
            >
              <UserIcon className="w-4 h-4" style={{ color: PINK }} />
              Profile
            </Link>
          </DropdownMenuItem>
          <DropdownMenuItem className="flex items-center gap-2 py-2 px-3 hover:bg-gray-100">
            <LogOut className="w-4 h-4" style={{ color: PINK }} />
            Logout
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  </header>
)
