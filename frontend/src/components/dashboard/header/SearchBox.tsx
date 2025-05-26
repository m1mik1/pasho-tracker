'use client'

import * as React from 'react'
import { Command, CommandGroup, CommandInput, CommandItem } from '@/components/ui/command'
import {
  Popover,
  PopoverTrigger,
  PopoverContent,
} from '@/components/ui/popover'
import { Input } from '@/components/ui/input'
import { Badge } from '@/components/ui/badge'
import { Search } from 'lucide-react'

const PINK = '#ea4aaa'

interface Board {
  id: string
  title: string
  workspace: string
  icon: React.JSX.Element
}

// demo data
const recentBoards: Board[] = [
  { id: 'b1', title: 'Basic Board', workspace: 'Team Alpha', icon: <Badge className="bg-blue-500" /> },
  { id: 'b2', title: 'Marketing',   workspace: 'Private',    icon: <Badge className="bg-purple-500" /> },
  { id: 'b3', title: 'My Kanban',   workspace: 'Workspace',  icon: <Badge className="bg-gradient-to-r from-pink-500 to-purple-500" /> },
]

export function SearchBox() {
  const [open, setOpen]   = React.useState(false)
  const [query, setQuery] = React.useState('')
  const inputRef          = React.useRef<HTMLInputElement>(null)

  // when popover opens, focus the real <input>
  React.useEffect(() => {
    if (open) {
      inputRef.current?.focus()
    }
  }, [open])

  const filtered = recentBoards.filter(b =>
    b.title.toLowerCase().includes(query.toLowerCase())
  )

  return (
    <Popover open={open} onOpenChange={setOpen}>
      {/* ─── trigger ─── */}
      <PopoverTrigger asChild>
        <div
          className="relative w-[28rem] cursor-text"
          onClick={() => setOpen(true)}   /* open on click, too */
        >
          <Search className="absolute left-3 top-1/2 -translate-y-1/2 w-4 h-4 text-gray-400" />
          <Input
            ref={inputRef}
            placeholder="Search boards…"
            value={query}
            onChange={e => setQuery(e.target.value)}
            className="pl-9 pr-3 py-2 rounded-md bg-white text-sm text-gray-800 placeholder:text-gray-400
                       border border-gray-300 focus:outline-none focus:ring-2 focus:ring-pink-300"
          />
        </div>
      </PopoverTrigger>

      {/* ─── popover content ─── */}
      <PopoverContent
        side="bottom"
        align="start"
        className="w-[28rem] p-0 bg-white border border-gray-200 rounded-md shadow-lg"
      >
        <Command>
          {/* hide the built-in input, but let Cmd handle ↑↓↵ */}
          <CommandInput className="hidden" value={query} onValueChange={setQuery} />

          <CommandGroup
            heading="RECENT BOARDS"
            className="px-3 py-2 text-xs font-semibold text-gray-500"
          >
            {filtered.length > 0 ? (
              filtered.map(board => (
                <CommandItem
                  key={board.id}
                  className="flex items-center gap-3 px-3 py-2 rounded hover:bg-gray-100"
                  onSelect={() => {
                    console.log('go to board', board.id)
                    setOpen(false)
                  }}
                >
                  {board.icon}
                  <div className="flex flex-col">
                    <span className="text-gray-900">{board.title}</span>
                    <span className="text-xs text-gray-500">{board.workspace}</span>
                  </div>
                </CommandItem>
              ))
            ) : (
              <div className="px-3 py-6 text-center text-sm text-gray-500">No results</div>
            )}
          </CommandGroup>

          <div className="flex items-center justify-between px-4 py-2 border-t border-gray-200 text-sm">
            <button
              className="flex items-center gap-2 text-pink-500 hover:text-pink-600"
              onClick={() => {
                console.log('advanced search:', query)
                setOpen(false)
              }}
            >
              <Search className="w-4 h-4" />
              Advanced search
            </button>
            <kbd className="text-xs text-gray-400">↵</kbd>
          </div>
        </Command>
      </PopoverContent>
    </Popover>
  )
}
