'use client'

import {BoardHeader} from './BoardHeader'
import CategoryColumn from './CategoryColumn'
import { CategoryModel } from '@/types/category-model'
import { TaskModel }     from '@/types/task-model'

interface BoardProps {
  title: string
  boardColor: string
  categories: CategoryModel[]
  tasks: TaskModel[]
  onAddTask: (catId: number) => void
  onEditTask: (task: TaskModel) => void
  onDeleteTask: (taskId: number) => void
}

export default function Board({
  title,
  boardColor,
  categories,
  tasks,
  onAddTask,
  onEditTask,
  onDeleteTask,
}: BoardProps) {
  return (
    <div className="h-screen overflow-hidden">
      {/* Шапка доски фиксированная */}
      <BoardHeader title={title} boardColor={boardColor} />

      {/* Контейнер колонок */}
      <div
        className="
          flex overflow-x-auto overflow-y-hidden  /* только горизонтальный скролл */
          space-x-6
          pt-10                                  /* 64px global + 48px board header = 112px (7rem)*/
          px-6                                   /* если нужны боковые отступы колонок */
          h-full
        "
      >
        {categories.map(cat => (
          <CategoryColumn
            key={cat.id}
            category={cat}
            tasks={tasks.filter(t => t.categoryId === cat.id)}
            onAddTask={() => onAddTask(cat.id)}
            onEditTask={onEditTask}
            onDeleteTask={onDeleteTask}
          />
        ))}

        {/* «Add another list» placeholder */}
        <div className="min-w-[12rem] flex items-center justify-center">
          <button className="px-4 py-2 bg-pink-100 text-pink-600 rounded-md">
            + Add another list
          </button>
        </div>
      </div>
    </div>
  )
}
