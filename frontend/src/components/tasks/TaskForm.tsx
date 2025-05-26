'use client';

import { useRouter, useSearchParams } from 'next/navigation';
import { useEffect, useState } from 'react';

export default function TaskForm() {
  const router = useRouter();
  const params = useSearchParams();
  const id = params.get('id');

  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [deadline, setDeadline] = useState('');
  const [priority, setPriority] = useState(0);
  const [status, setStatus] = useState(0);
  const [assignedUserId, setAssignedUserId] = useState('string');

  useEffect(() => {
    if (id) {
      fetch(`http://localhost:5178/api/Task/${id}`)
        .then((res) => res.json())
        .then((data) => {
          setTitle(data.title);
          setDescription(data.description);
          setDeadline(data.deadline);
          setPriority(data.priority);
          setStatus(data.status);
          setAssignedUserId(data.assignedUserId);
        });
    }
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const deadlineUTC = new Date(deadline).toISOString();

    const task = {
      id: id ? parseInt(id) : 0,
      title,
      description,
      deadline: deadlineUTC,
      priority,
      status,
      assignedUserId,
    };

    console.log('Sending task:', task);

    const res = await fetch(`http://localhost:5178/api/Task${id ? `/${id}` : ''}`, {
      method: id ? 'PUT' : 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(task),
    });

    if (res.ok) {
      router.push('/tasks');
    } else {
      console.error('Failed to save task');
    }
  };

  return (
    <div className="max-w-xl mx-auto mt-10 p-6 bg-gray-900 text-white rounded shadow">
      <h1 className="text-2xl font-bold mb-6">{id ? 'Edit Task' : 'Create New Task'}</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          required
          className="w-full p-2 rounded bg-gray-800 border border-gray-600"
        />
        <textarea
          placeholder="Description"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          required
          className="w-full p-2 rounded bg-gray-800 border border-gray-600"
        />
        <input
          type="datetime-local"
          value={deadline ? deadline.slice(0, 16) : ''}
          onChange={(e) => setDeadline(e.target.value)}
          required
          className="w-full p-2 rounded bg-gray-800 border border-gray-600"
        />
        <div className="flex space-x-4">
          <div className="flex-1">
            <label className="block mb-1 text-sm">Priority</label>
            <select
              value={priority}
              onChange={(e) => setPriority(Number(e.target.value))}
              className="w-full p-2 rounded bg-gray-800 border border-gray-600"
            >
              <option value={0}>Low</option>
              <option value={1}>Medium</option>
              <option value={2}>High</option>
            </select>
          </div>
          <div className="flex-1">
            <label className="block mb-1 text-sm">Status</label>
            <select
              value={status}
              onChange={(e) => setStatus(Number(e.target.value))}
              className="w-full p-2 rounded bg-gray-800 border border-gray-600"
            >
              <option value={0}>New</option>
              <option value={1}>In Progress</option>
              <option value={2}>Done</option>
            </select>
          </div>
        </div>
        <button
          type="submit"
          className="bg-green-600 hover:bg-green-700 px-4 py-2 rounded text-white w-full"
        >
          Save
        </button>
      </form>
    </div>
  );
}
