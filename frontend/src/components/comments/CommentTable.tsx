'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

type Comment = {
  id: number;
  commentText: string;
  author: string;
  relatedTaskId: number;
};

export default function CommentTable() {
  const [comments, setComments] = useState<Comment[]>([]);
  const router = useRouter();

  useEffect(() => {
    fetch('http://localhost:5178/api/Comment')
      .then((res) => res.json())
      .then((data) => setComments(data))
      .catch((err) => console.error('Fetch error:', err));
  }, []);

  const handleDelete = async (id: number) => {
    const confirmed = confirm('Are you sure you want to delete this comment?');
    if (!confirmed) return;

    await fetch(`http://localhost:5178/api/Comment/${id}`, {
      method: 'DELETE',
    });

    setComments((prev) => prev.filter((comment) => comment.id !== id));
  };

  return (
    <table className="min-w-full divide-y divide-gray-700 border border-gray-600">
      <thead className="bg-gray-800 text-white">
        <tr>
          <th className="px-6 py-3 text-left">ID</th>
          <th className="px-6 py-3 text-left">Comment</th>
          <th className="px-6 py-3 text-left">Author</th>
          <th className="px-6 py-3 text-left">Task ID</th>
          <th className="px-6 py-3 text-left">Actions</th>
        </tr>
      </thead>
      <tbody className="bg-gray-900 divide-y divide-gray-700 text-white">
        {comments.map((comment) => (
          <tr key={comment.id}>
            <td className="px-6 py-4">{comment.id}</td>
            <td className="px-6 py-4">{comment.commentText}</td>
            <td className="px-6 py-4">{comment.author}</td>
            <td className="px-6 py-4">{comment.relatedTaskId}</td>
            <td className="px-6 py-4 space-x-2">
              <button
                className="bg-blue-500 hover:bg-blue-600 px-3 py-1 rounded"
                onClick={() => router.push(`/comments/form?id=${comment.id}`)}
              >
                Edit
              </button>
              <button
                className="bg-red-500 hover:bg-red-600 px-3 py-1 rounded"
                onClick={() => handleDelete(comment.id)}
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
