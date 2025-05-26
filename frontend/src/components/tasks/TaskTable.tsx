'use client';

import { useEffect, useState } from 'react';
import { useRouter } from 'next/navigation';

type Task = {
  id: number;
  title: string;
  description: string;
  deadline?: string;
};

export default function TaskTable() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const router = useRouter();

  useEffect(() => {
    fetch('http://localhost:5178/api/Task')
      .then((res) => res.json())
      .then((data) => setTasks(data))
      .catch((err) => console.error('Fetch error:', err));
  }, []);

  const handleDelete = async (id: number) => {
    const confirmDelete = confirm('Are you sure you want to delete this task?');
    if (!confirmDelete) return;

    const res = await fetch(`http://localhost:5178/api/Task/${id}`, {
      method: 'DELETE',
    });

    if (res.ok) {
      setTasks((prev) => prev.filter((task) => task.id !== id));
    }
  };

  return (
    <div className="p-6">
      <h1 className="text-3xl font-bold mb-6 text-white">Task List</h1>
      <button
        onClick={() => router.push('/tasks/form')}
        className="mb-4 bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded"
      >
        + Add Task
      </button>

      <table className="min-w-full divide-y divide-gray-700 border border-gray-600">
        <thead className="bg-gray-800 text-white">
          <tr>
            <th className="px-6 py-3 text-left">ID</th>
            <th className="px-6 py-3 text-left">Title</th>
            <th className="px-6 py-3 text-left">Description</th>
            <th className="px-6 py-3 text-left">Deadline</th>
            <th className="px-6 py-3 text-left">Actions</th>
          </tr>
        </thead>
        <tbody className="bg-gray-900 divide-y divide-gray-700 text-white">
          {tasks.map((task) => (
            <tr key={task.id}>
              <td className="px-6 py-4">{task.id}</td>
              <td className="px-6 py-4">{task.title}</td>
              <td className="px-6 py-4">{task.description}</td>
              <td className="px-6 py-4">
                {task.deadline
                  ? new Date(task.deadline).toLocaleString('en-US')
                  : '—'}
              </td>
              <td className="px-6 py-4 space-x-2">
                <button
                  onClick={() => router.push(`/tasks/form?id=${task.id}`)}
                  className="bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 rounded"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDelete(task.id)}
                  className="bg-red-500 hover:bg-red-600 text-white px-3 py-1 rounded"
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
