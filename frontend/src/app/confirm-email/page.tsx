'use client';

import { useEffect, useState } from 'react';
import { useSearchParams, useRouter } from 'next/navigation';

export default function ConfirmEmailPage() {
  const searchParams = useSearchParams();
  const email = searchParams.get('email');
  const token = searchParams.get('token');
  const router = useRouter();
  const [message, setMessage] = useState('Confirming email...');

  useEffect(() => {
    const confirm = async () => {
      if (!email || !token) {
        setMessage('Missing confirmation data.');
        return;
      }

      const res = await fetch('http://localhost:5178/api/account/confirm-email', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, token }),
      });

      if (res.ok) {
        setMessage('Email confirmed successfully!');
        setTimeout(() => router.push('/login'), 2000);
      } else {
        setMessage('Email confirmation failed.');
      }
    };

    confirm();
  }, [email, token, router]);

  return (
    <main className="p-6 text-white">
      <h1 className="text-2xl font-bold">{message}</h1>
    </main>
  );
}
