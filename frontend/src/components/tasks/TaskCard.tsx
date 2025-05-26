'use client'

interface TaskCardProps {
  title: string
  description?: string
  dueDate?: string
}

export const TaskCard = ({ title, description, dueDate }: TaskCardProps) => {
  return (
    <div className="p-4 bg-white rounded-xl shadow hover:shadow-lg transition">
      <h4 className="font-bold text-darkText mb-2">{title}</h4>
      {description && <p className="text-sm text-gray-600 mb-2">{description}</p>}
      {dueDate && (
        <p className="text-xs text-gray-400">
          Дедлайн: <span className="text-gray-700">{dueDate}</span>
        </p>
      )}
    </div>
  )
}
