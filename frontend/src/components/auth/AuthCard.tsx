'use client';

import { ReactNode } from 'react';

export default function AuthCard({ children, title }: { children: ReactNode; title: string }) {
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-950 text-white px-4">
      <div className="max-w-md w-full p-6 bg-gray-900 rounded-lg shadow-md">
        <h1 className="text-2xl font-bold text-center mb-6">{title}</h1>
        {children}
      </div>
    </div>
  );
}
