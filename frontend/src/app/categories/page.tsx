'use client';
import Link from 'next/link';
import CategoryTable from '@/components/categories/CategoryTable';

export default function CategoryPage() {
  return (
    <main className="p-6">
      <h1 className="text-3xl font-bold mb-4 text-white">Categories</h1>

      <div className="mb-4">
        <Link
          href="/categories/form"
          className="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded"
        >
          + New category
        </Link>
      </div>

      <CategoryTable />
    </main>
  );
}
