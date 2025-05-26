import { useState } from 'react'
import { AddTaskModal } from './AddTaskModal'
import { TaskCard } from './TaskCard'
import { Task } from '@/types/Task'

export const TaskColumn = ({ title }: { title: string }) => {
    const [isModalOpen, setModalOpen] = useState(false)
    const [tasks, setTasks] = useState<Task[]>([])
  
    const handleAddTask = (task: Task) => {
      setTasks((prev) => [...prev, task])
    }
  
    return (
      <div className="w-80 flex-shrink-0 bg-primaryLight rounded-xl shadow-md p-4">
        <h3 className="text-lg font-bold mb-4 text-darkText">{title}</h3>
        <div className="space-y-2">
          {tasks.map((task, idx) => (
            <TaskCard key={idx} title={task.title} description={task.description} dueDate={task.deadline} />
          ))}
        </div>
        <button onClick={() => setModalOpen(true)} className="mt-4 text-primary hover:underline">+ Добавить задачу</button>
  
        <AddTaskModal isOpen={isModalOpen} onClose={() => setModalOpen(false)} onSubmit={handleAddTask} />
      </div>
    )
  }
  