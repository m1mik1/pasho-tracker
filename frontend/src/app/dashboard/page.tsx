'use client'

import { useEffect, useState } from 'react'
import Board from '@/components/dashboard/Board'


import { TaskModel } from '@/types/task-model'

import { CategoryModel } from '@/types/category-model'

// Моки данных категорий и задач
const initialCategories: CategoryModel[] = [
  { id: 1, categoryName: 'Category 1' },
  { id: 2, categoryName: 'Category 2' },
]

const initialTasks: TaskModel[] = [
  {
    id: 1,
    categoryId: 1,
    title: 'Design UI',
    description: 'Create initial wireframes.',
    priority: 'High',
    status: 'ToDo',
    deadline: new Date().toISOString(),
    assignedUserId: 'user1',
    comments: [],
  },
  {
    id: 2,
    categoryId: 1,
    title: 'API Setup',
    description: 'Connect to backend.',
    priority: 'Medium',
    status: 'InProgress',
    deadline: new Date().toISOString(),
    assignedUserId: 'user2',
    comments: [],
  },
  {
    id: 3,
    categoryId: 2,
    title: 'Develop API',
    description: 'Build RESTful API.',
    priority: 'Medium',
    status: 'InProgress',
    deadline: new Date().toISOString(),
    assignedUserId: 'user3',
    comments: [],
  },
]

export default function DashboardPage() {
  
  useEffect(() => {
    document.body.classList.add('no-scroll');

    return () => {
      document.body.classList.remove('no-scroll');
    };
  }, []);

  const [categories, setCategories] = useState<CategoryModel[]>(initialCategories)
  const [tasks, setTasks] = useState<TaskModel[]>(initialTasks)

  // Добавить задачу
  const addTask = (categoryId: number) => {
    const newTask: TaskModel = {
      id: Date.now(),
      categoryId,
      title: 'New Task',
      description: 'Task description...',
      priority: 'Low',
      status: 'ToDo',
      deadline: new Date().toISOString(),
      assignedUserId: 'user4',
      comments: [],
    }
    setTasks(prev => [...prev, newTask])
  }

  // Редактировать задачу
  const editTask = (updatedTask: TaskModel) => {
    setTasks(prev => prev.map(task => task.id === updatedTask.id ? updatedTask : task))
  }

  
  // Удалить задачу
  const deleteTask = (taskId: number) => {
    setTasks(prev => prev.filter(task => task.id !== taskId))
  }

  return (
    <div className="h-screen bg-gradient-to-br from-pink-100 to-pink-50 overflow-hidden ">
      <main className="pt-20 px-4 h-full">
        <Board
          categories={categories}
          tasks={tasks}
          onAddTask={addTask}
          onEditTask={editTask}
          onDeleteTask={deleteTask} title={''} boardColor={"fccee8"}        />
      </main>
    </div>
  )
}
