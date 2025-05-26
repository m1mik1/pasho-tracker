'use client';

import { useRouter, useSearchParams } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function CommentForm() {
  const router = useRouter();
  const params = useSearchParams();
  const id = params.get('id');

  const [commentText, setCommentText] = useState('');
  const [author, setAuthor] = useState('');
  const [relatedTaskId, setRelatedTaskId] = useState<number | ''>('');

  useEffect(() => {
    if (id) {
      fetch(`http://localhost:5178/api/Comment/${id}`)
        .then((res) => res.json())
        .then((data) => {
          setCommentText(data.commentText);
          setAuthor(data.author);
          setRelatedTaskId(data.relatedTaskId);
        });
    }
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const comment = {
      id: id ? parseInt(id) : 0,
      commentText,
      author,
      relatedTaskId: Number(relatedTaskId),
    };

    const res = await fetch(`http://localhost:5178/api/Comment${id ? `/${id}` : ''}`, {
      method: id ? 'PUT' : 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(comment),
    });

    if (res.ok) {
      router.push('/comments');
    } else {
      console.error('Failed to save comment');
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-6 bg-gray-900 text-white rounded shadow">
      <h1 className="text-2xl font-bold mb-6">{id ? 'Edit' : 'Add'} Comment</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          placeholder="Comment"
          value={commentText}
          onChange={(e) => setCommentText(e.target.value)}
          required
          className="w-full p-2 bg-gray-800 border border-gray-600 rounded"
        />
        <input
          type="text"
          placeholder="Author"
          value={author}
          onChange={(e) => setAuthor(e.target.value)}
          required
          className="w-full p-2 bg-gray-800 border border-gray-600 rounded"
        />
        <input
          type="number"
          placeholder="Task ID"
          value={relatedTaskId}
          onChange={(e) => setRelatedTaskId(Number(e.target.value))}
          required
          className="w-full p-2 bg-gray-800 border border-gray-600 rounded"
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
