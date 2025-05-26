'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

type Category = {
  id: number;
  categoryName: string;
};

export default function CategoryTable() {
  const [categories, setCategories] = useState<Category[]>([]);
  const router = useRouter();

  useEffect(() => {
    fetch('http://localhost:5178/api/Category')
      .then((res) => res.json())
      .then((data) => setCategories(data))
      .catch((err) => console.error('Fetch error:', err));
  }, []);

  const handleDelete = async (id: number) => {
    const confirmed = confirm('Are you sure you want to delete this category?');
    if (!confirmed) return;

    await fetch(`http://localhost:5178/api/Category/${id}`, {
      method: 'DELETE',
    });

    setCategories((prev) => prev.filter((cat) => cat.id !== id));
  };

  return (
    <table className="min-w-full divide-y divide-gray-700 border border-gray-600">
      <thead className="bg-gray-800 text-white">
        <tr>
          <th className="px-6 py-3 text-left">ID</th>
          <th className="px-6 py-3 text-left">Category Name</th>
          <th className="px-6 py-3 text-left">Actions</th>
        </tr>
      </thead>
      <tbody className="bg-gray-900 divide-y divide-gray-700 text-white">
        {categories.map((cat) => (
          <tr key={cat.id}>
            <td className="px-6 py-4">{cat.id}</td>
            <td className="px-6 py-4">{cat.categoryName}</td>
            <td className="px-6 py-4 space-x-2">
              <button
                onClick={() => router.push(`/categories/form?id=${cat.id}`)}
                className="bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded"
              >
                Edit
              </button>
              <button
                onClick={() => handleDelete(cat.id)}
                className="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded"
              >
                Delete
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
