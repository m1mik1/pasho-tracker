'use client'

import { useState } from 'react'
import { TaskPriority, TaskStatus, Task } from '@/types/Task'

interface AddTaskModalProps {
  isOpen: boolean
  onClose: () => void
  onSubmit: (task: Task) => void
}

export const AddTaskModal = ({ isOpen, onClose, onSubmit }: AddTaskModalProps) => {
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [priority, setPriority] = useState<TaskPriority>('Medium')
  const [status, setStatus] = useState<TaskStatus>('ToDo')
  const [deadline, setDeadline] = useState('')
  const [assignedUserId, setAssignedUserId] = useState('')

  const handleSubmit = () => {
    if (!title || !deadline || !assignedUserId) return // Простая валидация
    const newTask: Task = {
      id: Date.now(), // Временно, до интеграции с API
      title,
      description,
      priority,
      status,
      deadline,
      assignedUserId,
      comments: []
    }
    onSubmit(newTask)
    // Сброс формы
    setTitle('')
    setDescription('')
    setPriority('Medium')
    setStatus('ToDo')
    setDeadline('')
    setAssignedUserId('')
    onClose()
  }

  if (!isOpen) return null

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white p-6 rounded-xl shadow-lg w-full max-w-md">
        <h2 className="text-xl font-bold mb-4 text-darkText">Создать задачу</h2>
        
        <input
          type="text"
          placeholder="Название задачи"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          className="w-full p-3 mb-4 border border-gray-300 rounded-xl"
        />

        <textarea
          placeholder="Описание"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="w-full p-3 mb-4 border border-gray-300 rounded-xl"
        />

        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700 mb-1">Приоритет</label>
          <select value={priority} onChange={(e) => setPriority(e.target.value as TaskPriority)} className="w-full p-3 border border-gray-300 rounded-xl">
            <option value="Low">Низкий</option>
            <option value="Medium">Средний</option>
            <option value="High">Высокий</option>
          </select>
        </div>

        <div className="mb-4">
          <label className="block text-sm font-medium text-gray-700 mb-1">Статус</label>
          <select value={status} onChange={(e) => setStatus(e.target.value as TaskStatus)} className="w-full p-3 border border-gray-300 rounded-xl">
            <option value="ToDo">К выполнению</option>
            <option value="InProgress">В процессе</option>
            <option value="Done">Сделано</option>
          </select>
        </div>

        <input
          type="date"
          value={deadline}
          onChange={(e) => setDeadline(e.target.value)}
          className="w-full p-3 mb-4 border border-gray-300 rounded-xl"
        />

        <input
          type="text"
          placeholder="ID назначенного пользователя"
          value={assignedUserId}
          onChange={(e) => setAssignedUserId(e.target.value)}
          className="w-full p-3 mb-4 border border-gray-300 rounded-xl"
        />

        <div className="flex justify-end space-x-4">
          <button onClick={onClose} className="text-gray-500 hover:text-gray-700">Отмена</button>
          <button onClick={handleSubmit} className="bg-primary text-white px-4 py-2 rounded-xl hover:bg-accent">Создать</button>
        </div>
      </div>
    </div>
  )
}
