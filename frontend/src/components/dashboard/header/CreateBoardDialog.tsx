'use client'

import * as React from 'react'
import { Popover, PopoverTrigger, PopoverContent } from '@/components/ui/popover'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Select, SelectTrigger, SelectContent, SelectItem } from '@/components/ui/select'
import { X, Check } from 'lucide-react'

const PHOTOS = [
  'https://images.unsplash.com/photo-1501785888041-af3ef285b470?w=400&q=80',
  'https://images.unsplash.com/photo-1511203466129-824e631920d4?w=400&q=80',
  'https://images.unsplash.com/photo-1504384308090-c894fdcc538d?w=400&q=80',
]
const PALETTE = [
  '#FFF0F5', '#F8BBDE', '#F472B6',
  '#DB2777', '#9333EA', '#7E22CE',
]

export function CreateBoard() {
  const [open, setOpen] = React.useState(false)
  const [bg, setBg] = React.useState<string>(PHOTOS[0])
  const [title, setTitle] = React.useState('')
  const [visibility, setVisibility] = React.useState<'workspace'|'private'|'public'>('workspace')
  const [touched, setTouched] = React.useState(false)

  const isValid = title.trim().length > 0
  const reset = () => {
    setBg(PHOTOS[0])
    setTitle('')
    setVisibility('workspace')
    setTouched(false)
  }

  return (
    <Popover open={open} onOpenChange={val => { setOpen(val); if (!val) reset() }}>
      {/* Триггер */}
      <PopoverTrigger asChild>
        <Button className="px-4 py-2 text-sm font-medium bg-pink-500 hover:bg-pink-600 text-white">
          Create
        </Button>
      </PopoverTrigger>

      {/* PopoverContent */}
      <PopoverContent
        side="bottom"
        align="start"
        className="w-80 p-4 bg-white text-gray-900 border border-gray-200 rounded-lg shadow-md"
      >
        {/* Preview + Close */}
        <div className="relative w-full h-24 mb-4 rounded-lg overflow-hidden bg-gray-100">
          {bg.startsWith('#') ? (
            <div className="w-full h-full" style={{ background: bg }} />
          ) : (
            <img
              src={bg}
              alt=""
              className="w-full h-full object-cover"
            />
          )}
          <span className="absolute top-2 left-2 bg-white/80 rounded-full p-1">
            <Check className="w-4 h-4 text-pink-500" />
          </span>
          <button
            onClick={() => setOpen(false)}
            className="absolute top-1 right-1 p-1 rounded hover:bg-gray-200"
          >
            <X className="w-4 h-4 text-gray-600" />
          </button>
        </div>

        {/* Background */}
        <p className="mb-2 text-sm font-medium">Background</p>
        <div className="flex gap-2 flex-wrap mb-4">
          {PHOTOS.map(src => (
            <div
              key={src}
              onClick={() => setBg(src)}
              className={`w-16 h-10 rounded-md overflow-hidden cursor-pointer border-2 ${
                bg === src ? 'border-pink-500' : 'border-transparent'
              }`}
            >
              <img src={src} alt="" className="w-full h-full object-cover" />
            </div>
          ))}
        </div>
        <div className="flex gap-2 flex-wrap mb-4">
          {PALETTE.map(color => (
            <button
              key={color}
              onClick={() => setBg(color)}
              className={`w-10 h-10 rounded-md border-2 ${
                bg === color ? 'border-pink-500' : 'border-transparent'
              }`}
              style={{ background: color }}
            />
          ))}
          <button
            className="w-10 h-10 rounded-md bg-gray-200 flex items-center justify-center text-lg font-bold text-gray-500"
            onClick={() => alert('Color picker stub')}
          >
            …
          </button>
        </div>

        {/* Title */}
        <label className="block text-sm font-medium mb-1">
          Board title<span className="text-pink-500">*</span>
        </label>
        <Input
          value={title}
          onChange={e => setTitle(e.target.value)}
          onBlur={() => setTouched(true)}
          placeholder="e.g. Marketing Plan"
          className="w-full mb-1 bg-white border border-pink-300 focus:border-pink-500"
        />
        {touched && !isValid && (
          <p className="text-xs text-pink-500 mb-3">Board title is required</p>
        )}

        {/* Visibility */}
        <label className="block text-sm font-medium mb-1">Visibility</label>
        <Select
          value={visibility}
          onValueChange={val => setVisibility(val as any)}
        >
          <SelectTrigger className="w-full mb-4 bg-white border border-pink-300 focus:border-pink-500">
            {visibility.charAt(0).toUpperCase() + visibility.slice(1)}
          </SelectTrigger>
          <SelectContent className="bg-white text-gray-900">
            <SelectItem value="workspace">Workspace</SelectItem>
            <SelectItem value="private">Private</SelectItem>
            <SelectItem value="public">Public</SelectItem>
          </SelectContent>
        </Select>

        {/* Buttons */}
        <Button
          onClick={() => { setOpen(false); reset() }}
          disabled={!isValid}
          className={`w-full mb-2 text-sm font-medium ${
            isValid
              ? 'bg-pink-500 hover:bg-pink-600 text-white'
              : 'bg-gray-300 text-gray-500 cursor-not-allowed'
          }`}
        >
          Create
        </Button>
        <Button
          variant="outline"
          className="w-full text-sm font-medium text-pink-500 border border-pink-500 hover:bg-pink-50"
        >
          Start with a template
        </Button>
      </PopoverContent>
    </Popover>
  )
}
