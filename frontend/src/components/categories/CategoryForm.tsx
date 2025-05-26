'use client';

import { useRouter, useSearchParams } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function CategoryForm() {
  const router = useRouter();
  const params = useSearchParams();
  const id = params.get('id');

  const [categoryName, setCategoryName] = useState('');

  useEffect(() => {
    if (id) {
      fetch(`http://localhost:5178/api/Category/${id}`)
        .then((res) => res.json())
        .then((data) => setCategoryName(data.categoryName));
    }
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const category = { id: id ? Number(id) : 0, categoryName };

    const res = await fetch(`http://localhost:5178/api/Category${id ? `/${id}` : ''}`, {
      method: id ? 'PUT' : 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(category),
    });

    if (res.ok) {
      router.push('/categories');
    } else {
      console.error('Failed to save category');
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-gray-900 text-white rounded shadow">
      <h1 className="text-2xl font-bold mb-6">
        {id ? 'Edit Category' : 'Create Category'}
      </h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          placeholder="Category name"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          required
          className="w-full p-2 rounded bg-gray-800 border border-gray-600"
        />
        <button
          type="submit"
          className="bg-green-600 hover:bg-green-700 px-4 py-2 rounded text-white"
        >
          Save
        </button>
      </form>
    </div>
  );
}
