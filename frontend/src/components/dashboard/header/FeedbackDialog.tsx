'use client'

import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from '@/components/ui/dialog'
import { Button } from '@/components/ui/button'
import { Checkbox } from '@/components/ui/checkbox'
import { Textarea } from '@/components/ui/textarea'
import { CheckedState } from '@radix-ui/react-checkbox'
import { Megaphone } from 'lucide-react'
import * as React from 'react'

const PINK = '#e91e63'

export function FeedbackDialog() {
  // локальный стейт чекбоксов (для примера)
 

  const [agreeContact,  setAgreeContact]  = React.useState<CheckedState>(false)
  const [agreeResearch, setAgreeResearch] = React.useState<CheckedState>(false)
  
  const [message, setMessage] = React.useState('')

  return (
    <Dialog>
      {/* ───── Триггер ───── */}
      <DialogTrigger asChild>
        <button
          className="h-9 w-9 flex items-center justify-center rounded-md hover:bg-gray-100 transition"
          aria-label="Send feedback"
        >
          <Megaphone className="w-5 h-5 text-pink-600" />
        </button>
      </DialogTrigger>

      {/* ───── Контент ───── */}
      <DialogContent className="max-w-lg bg-white">
        <DialogHeader>
          <DialogTitle className="text-2xl font-bold">Share your thoughts about PaSho</DialogTitle>
          <p className="text-sm text-gray-500 mt-2">
            Required fields are marked with an asterisk&nbsp;
            <span className="text-pink-600">*</span>
          </p>
        </DialogHeader>

        {/* Textarea */}
        <label className="flex flex-col gap-1">
          <span className="text-sm font-medium text-gray-700">
            What’s on your mind?<span className="text-pink-600">*</span>
          </span>
          <Textarea
            rows={6}
            value={message}
            onChange={e => setMessage(e.target.value)}
            className="resize-none"
          />
        </label>

        {/* Checkboxes */}
        <div className="space-y-3">
          <label className="flex items-start gap-2 text-sm text-gray-700">
            <Checkbox checked={agreeContact} onCheckedChange={setAgreeContact} />
            Yes, PaSho team may contact me to improve our products.
          </label>
          <label className="flex items-start gap-2 text-sm text-gray-700">
            <Checkbox checked={agreeResearch} onCheckedChange={setAgreeResearch} />
            I’d like to participate in product research.
          </label>
        </div>

        {/* Footer */}
        <DialogFooter className="mt-4">
          <Button
            variant="ghost"
            className="mr-2"
            onClick={() => (document.activeElement as HTMLElement)?.click()} // закрыть
          >
            Cancel
          </Button>

          <Button
            disabled={!message.trim()}
            style={{ background: PINK }}
            className="text-white"
            onClick={() => {
              // TODO: отправка на API
              console.log({ message, agreeContact, agreeResearch })
            }}
          >
            Send feedback
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  )
}
