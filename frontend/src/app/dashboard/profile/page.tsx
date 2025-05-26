'use client'

import { useRouter } from 'next/navigation'

export default function UserProfile() {
  const router = useRouter()

  const handleLogout = () => {
    // Логика выхода (чистим токены, состояние и т.д.)
    router.push('/login') // Возврат на логин
  }

  return (
    <div className="max-w-md mx-auto p-6 bg-white rounded-xl shadow-md">
      <h2 className="text-2xl font-bold text-darkText mb-4">Профиль</h2>
      <p className="text-gray-700 mb-2"><strong>Имя:</strong> Иван Иванов</p>
      <p className="text-gray-700 mb-4"><strong>Email:</strong> ivan@example.com</p>

      <button onClick={handleLogout} className="bg-primary text-white px-4 py-2 rounded-xl hover:bg-accent">
        Выйти
      </button>
    </div>
  )
}
