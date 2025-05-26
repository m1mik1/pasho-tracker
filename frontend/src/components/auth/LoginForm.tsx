'use client';

import { useState } from 'react';
import { useRouter } from 'next/navigation';
import AuthCard from './AuthCard';

export default function LoginForm() {
  const router = useRouter();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    const res = await fetch('http://localhost:5178/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, password }),
    });

    if (res.ok) {
      router.push('/tasks');
    } else {
      setError('Login failed. Please check your credentials.');
    }
  };

  return (
    <AuthCard title="Login to your account">
      <form onSubmit={handleLogin} className="space-y-4">
        {error && <div className="text-red-400 text-sm">{error}</div>}
        <input
          type="email"
          placeholder="Email"
          className="w-full p-2 bg-gray-800 border border-gray-600 rounded"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          className="w-full p-2 bg-gray-800 border border-gray-600 rounded"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit" className="w-full bg-blue-600 hover:bg-blue-700 py-2 rounded text-white">
          Login
        </button>
      </form>
    </AuthCard>
  );
}
