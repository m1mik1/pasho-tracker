'use client'

import { useState } from 'react'
import { CategoryModel } from '@/types/category-model'
import { TaskModel } from '@/types/task-model'
import {TaskCard } from './TaskCard'
import { Button } from '@/components/ui/button'
import { Plus } from 'lucide-react'

interface CategoryColumnProps {
  category: CategoryModel
  tasks: TaskModel[]
  onAddTask: (categoryId: number) => void
  onEditTask: (task: TaskModel) => void
  onDeleteTask: (taskId: number) => void
}

export default function CategoryColumn({
  category,
  tasks,
  onAddTask,
  onEditTask,
  onDeleteTask,
}: CategoryColumnProps) {
  return (
    <div className="bg-pink-50 border border-pink-200 rounded-lg p-4 w-72 flex flex-col space-y-4 shadow-sm">
      {/* Column Header */}
      <div className="flex justify-between items-center mb-2">
        <h2 className="text-lg font-bold text-pink-500">{category.categoryName}</h2>
        <Button variant="ghost" size="icon" onClick={() => onAddTask(category.id)}>
          <Plus className="w-4 h-4 text-pink-500" />
        </Button>
      </div>

      {/* Tasks */}
      <div className="flex flex-col space-y-3">
        {tasks.map((task) => (
          <TaskCard key={task.id} task={task} onEdit={onEditTask} onDelete={onDeleteTask} />
        ))}
      </div>
    </div>
  )
}
