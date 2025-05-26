'use client';

import { useState, useEffect } from 'react';
import { useRouter } from 'next/navigation';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';
import { Mail, User, Lock, Eye, EyeOff, CheckCircle, XCircle } from 'lucide-react';
import Link from 'next/link';
import ConfirmEmailModal from './ConfirmEmailModal';
import Image from 'next/image';

const carouselData = [
  {
    title: 'Why PaSho?',
    points: ['✨ Elegant task boards', '⚡ Real-time collaboration', '💎 Forever free, no limits'],
  },
  {
    title: 'User Reviews',
    points: ['🌟 "PaSho doubled my team’s output!" - Pavel', '💬 "Smooth and intuitive interface." - Sanya', '🚀 "A must-have for teams." - Kate'],
  },
  {
    title: 'Did You Know?',
    points: ['🧠 87% of users feel more organized.', '📊 Projects finish 40% faster.', '🔒 Secured with encryption.'],
  },
];

export default function RegisterForm() {
  const router = useRouter();
  const [email, setEmail] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [modalOpen, setModalOpen] = useState(false);
  const [carouselIndex, setCarouselIndex] = useState(0);
  const [fade, setFade] = useState(true);

  useEffect(() => {
    const interval = setInterval(() => {
      setFade(false);
      setTimeout(() => {
        setCarouselIndex((prev) => {
          const next = (prev + 1) % carouselData.length;
          console.log(`Switching to block: ${next + 1}`); // debug
          return next;
        });
        setFade(true);
      }, 200); // shorter fade
    }, 5000);
  
    return () => clearInterval(interval);
  }, []);

  const minLength = password.length >= 6;
  const hasUppercase = /[A-Z]/.test(password);
  const hasSpecialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password);
  const passwordValid = minLength && hasUppercase && hasSpecialChar;

  const emailValid = email.includes('@');
  const passwordsMatch = password === confirmPassword;
  const isDisabled = !emailValid || username === '' || !passwordValid || !passwordsMatch;

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setLoading(true);

    try {
      const res = await fetch('http://localhost:5178/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, username, password, confirmPassword }),
      });

      const data = await res.json();

      if (res.ok && data.success) {
        setModalOpen(true);
      } else {
        setError(data.message || 'Registration failed.');
      }
    } catch {
      setError('Something went wrong.');
    } finally {
      setLoading(false);
    }
  };

  const RuleItem = ({ valid, text }: { valid: boolean; text: string }) => (
    <div className="flex items-center space-x-2 text-sm">
      {valid ? <CheckCircle className="w-4 h-4 text-green-500" /> : <XCircle className="w-4 h-4 text-pink-500" />}
      <span className={valid ? 'text-green-600' : 'text-pink-500'}>{text}</span>
    </div>
  );

  return (
    <div className="flex flex-col items-center justify-start min-h-screen bg-gradient-to-br from-[#FFF0F5] to-[#FAF9F6] px-4 py-8 overflow-hidden">
      {/* Header Centered */}
      <div className="text-center mb-8 max-w-xl">
        <h1 className="text-4xl font-extrabold text-[#374151] tracking-tight">
          Welcome to <span className="text-pink-500">PaSho</span>
        </h1>
        <p className="text-[#4B5563] mt-2 text-md">
          Manage tasks beautifully. Real-time updates, elegant boards, and seamless collaboration — join a growing team of productive minds.
        </p>
      </div>

      {/* Form Container */}
      <div className="bg-white rounded-3xl shadow-2xl grid md:grid-cols-2 max-w-4xl w-full overflow-hidden mt-4">
        {/* Carousel Block */}
        <div className="flex flex-col justify-center items-center bg-gradient-to-b from-pink-100 to-pink-50 p-8 transition-opacity duration-500 ease-in-out text-center"
          style={{ opacity: fade ? 1 : 0 }}>
          <h3 className="text-2xl font-bold text-pink-500 mb-4">{carouselData[carouselIndex].title}</h3>
          <ul className="text-gray-700 text-md space-y-3">
            {carouselData[carouselIndex].points.map((point, idx) => (
              <li key={idx}>{point}</li>
            ))}
          </ul>
        </div>

        {/* Form */}
        <div className="p-8 sm:p-10">
          <h2 className="text-2xl font-bold text-[#374151] text-center mb-4 tracking-tight leading-snug">
            Create Your <span className="text-pink-500">Account</span>
          </h2>
          <form onSubmit={handleSubmit} className="space-y-4">
            {error && <div className="text-red-500 text-sm">{error}</div>}

            {/* Username */}
            <div className="flex items-center w-full relative">
              <User className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
              <Input type="text" placeholder="Username" className="pl-10 pr-3 py-2 w-full border border-pink-200 focus:ring-pink-500 rounded-md text-gray-700" value={username} onChange={(e) => setUsername(e.target.value)} />
            </div>

            {/* Email */}
            <div className="flex items-center w-full relative">
              <Mail className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
              <Input type="email" placeholder="Email" className="pl-10 pr-3 py-2 w-full border border-pink-200 focus:ring-pink-500 rounded-md text-gray-700" value={email} onChange={(e) => setEmail(e.target.value)} />
            </div>

            {!emailValid && email.length > 0 && (
              <p className="text-sm text-pink-500">Email must contain "@"</p>
            )}

            {/* Password */}
            <div className="flex items-center w-full relative">
              <Lock className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
              <Input type={showPassword ? 'text' : 'password'} placeholder="Password" className="pl-10 pr-10 py-2 w-full border border-pink-200 focus:ring-pink-500 rounded-md text-gray-700" value={password} onChange={(e) => setPassword(e.target.value)} />
              <button type="button" onClick={() => setShowPassword(!showPassword)} className="absolute right-3 top-1/2 transform -translate-y-1/2 text-pink-500">
                {showPassword ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
              </button>
            </div>

            {/* Password Rules */}
            {password.length > 0 && (
              <div className="space-y-1">
                <RuleItem valid={minLength} text="6+ characters" />
                <RuleItem valid={hasUppercase} text="At least 1 uppercase letter" />
                <RuleItem valid={hasSpecialChar} text="At least 1 special character (!@#$%)" />
              </div>
            )}

            {/* Confirm Password */}
            <div className="flex items-center w-full relative">
              <Lock className="w-4 h-4 text-pink-500 absolute left-3 top-1/2 transform -translate-y-1/2" />
              <Input type={showConfirmPassword ? 'text' : 'password'} placeholder="Confirm Password" className="pl-10 pr-10 py-2 w-full border border-pink-200 focus:ring-pink-500 rounded-md text-gray-700" value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} />
              <button type="button" onClick={() => setShowConfirmPassword(!showConfirmPassword)} className="absolute right-3 top-1/2 transform -translate-y-1/2 text-pink-500">
                {showConfirmPassword ? <EyeOff className="w-4 h-4" /> : <Eye className="w-4 h-4" />}
              </button>
            </div>

            {!passwordsMatch && confirmPassword.length > 0 && (
              <p className="text-sm text-pink-500">Passwords do not match</p>
            )}

            {/* Register Button */}
            <Button type="submit" disabled={isDisabled || loading} className="w-full bg-gradient-to-r from-pink-500 to-pink-600 hover:from-pink-600 hover:to-pink-700 text-white font-bold py-3 transition-all">
              {loading ? 'Creating Account...' : 'Sign Up'}
            </Button>

            {/* Link to Sign In */}
            <p className="text-center text-sm text-gray-600">
              Already have an account?{' '}
              <Link href="/?signin=true" className="text-pink-500 hover:underline font-semibold">
                Sign In
              </Link>
            </p>
          </form>
        </div>
      </div>

      {/* Bottom Illustrations Enlarged */}
      <Image
        src="/login-illustration.svg"
        alt="Login Illustration"
        width={280}
        height={280}
        className="absolute bottom-0 left-0 mb-4 ml-4 hidden md:block"
      />

      <Image
        src="/teamwork-illustration.svg"
        alt="Teamwork Illustration"
        width={280}
        height={280}
        className="absolute bottom-0 right-0 mb-4 mr-4 hidden md:block"
      />

      {/* Success Modal */}
      <ConfirmEmailModal open={modalOpen} setOpen={setModalOpen} />
    </div>
  );
}
