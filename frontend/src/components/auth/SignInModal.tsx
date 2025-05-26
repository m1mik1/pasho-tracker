'use client'

import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { Mail, Lock, Eye, EyeOff } from 'lucide-react'
import { useState } from 'react'
import Link from 'next/link'
import { useRouter } from 'next/navigation'

export const SignInModal = ({ open, setOpen }: { open: boolean; setOpen: (open: boolean) => void }) => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [showPassword, setShowPassword] = useState(false)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState('')
  const router = useRouter()

  const isDisabled = email === '' || password === '' || !email.includes('@')
  const showEmailHint = email.length > 0 && !email.includes('@')

  const handleLogin = async () => {
    setLoading(true)
    setError('')
    try {
      const res = await fetch('http://localhost:5178/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password }),
      })

      if (!res.ok) {
        throw new Error('Invalid credentials')
      }

      const data = await res.json()
      console.log('Login Success:', data)
      setOpen(false)

      // Переход в дашборд
      router.push('/dashboard')

    } catch (err) {
      setError('Login failed. Please check your email and password.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="backdrop-blur-sm bg-white/80 border-pink-200">
        <DialogHeader className="text-center">
          <DialogTitle className="text-darkText text-3xl font-extrabold tracking-tight mb-2">
            Sign In & Take <span className="text-pink-500">Control</span>
          </DialogTitle>
          <p className="text-sm text-gray-600 italic">
            Enter your details and get back to crushing tasks 🚀
          </p>
        </DialogHeader>

        <div className="space-y-4">
          {/* Email */}
          <div className="flex items-center w-full relative">
            <Mail className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
            <Input
              type="email"
              placeholder="Email"
              className="pl-10 pr-3 py-2 w-full bg-white border border-pink-300 focus:ring-pink-500 rounded-md text-gray-700"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          {/* Email Hint */}
          {showEmailHint && (
            <p className="text-sm text-pink-500">Email must contain "@"</p>
          )}

          {/* Password */}
          <div className="flex items-center w-full relative">
            <Lock className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
            <Input
              type={showPassword ? 'text' : 'password'}
              placeholder="Password"
              className="pl-10 pr-10 py-2 w-full bg-white border border-pink-300 focus:ring-pink-500 rounded-md text-gray-700"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <button
              type="button"
              onClick={() => setShowPassword(!showPassword)}
              className="absolute right-3 top-1/2 transform -translate-y-1/2 text-pink-500"
            >
              {showPassword ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
            </button>
          </div>

          {/* Error */}
          {error && <p className="text-sm text-red-500">{error}</p>}

          {/* Sign In */}
          <Button
            disabled={isDisabled || loading}
            className="w-full bg-pink-500 hover:bg-pink-600"
            onClick={handleLogin}
          >
            {loading ? 'Signing In...' : 'Sign In'}
          </Button>

          <p className="text-center text-sm text-gray-600">
            Don’t have an account?{' '}
            <Link href="/register" className="text-pink-500 hover:underline font-semibold">
              Sign Up
            </Link>
          </p>
        </div>
      </DialogContent>
    </Dialog>
  )
}
