'use client';
import Link from 'next/link';
import CommentTable from '@/components/comments/CommentTable';

export default function CommentPage() {
  return (
    <main className="p-6">
      <h1 className="text-3xl font-bold mb-4 text-white">Comments</h1>

      <div className="mb-4">
        <Link
          href="/comments/form"
          className="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded"
        >
          + Add a comment
        </Link>
      </div>

      <CommentTable />
    </main>
  );
}
