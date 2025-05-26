'use client'

import { TaskModel } from '@/types/task-model'
import { Badge } from '@/components/ui/badge'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Button } from '@/components/ui/button'
import { format } from 'date-fns'

interface TaskCardProps {
  task: TaskModel
  onEdit: (task: TaskModel) => void
  onDelete: (taskId: number) => void
}

export function TaskCard({ task, onEdit, onDelete }: TaskCardProps) {
  return (
    <Card className="mb-4 cursor-pointer shadow-sm hover:shadow-md transition-shadow">
      <CardHeader>
        <CardTitle className="text-base font-semibold text-pink-600 flex justify-between items-center">
          {task.title}
          <div className="space-x-2">
            <Button variant="ghost" size="sm" onClick={() => onEdit(task)}>
              Edit
            </Button>
            <Button variant="ghost" size="sm" onClick={() => onDelete(task.id)}>
              Delete
            </Button>
          </div>
        </CardTitle>
      </CardHeader>
      <CardContent className="text-sm text-gray-700 space-y-2">
        <p>{task.description}</p>
        <div className="flex items-center space-x-2">
          <Badge variant="secondary" className="text-xs">
            {task.status}
          </Badge>
          <Badge variant="outline" className="text-xs border-pink-200 text-pink-500">
            {task.priority}
          </Badge>
        </div>
        <p className="text-xs text-gray-400">
        Deadline:{' '}
        {format(new Date(task.deadline), 'dd/MM/yyyy')}
        </p>
      </CardContent>
    </Card>
  )
}
