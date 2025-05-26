// /src/types/task.ts

export interface Task {
  id: number
  title: string
  description?: string
  priority: TaskPriority
  status: TaskStatus
  deadline: string
  assignedUserId: string
  comments?: Comment[] // ← связь с комментариями
}

export type TaskPriority = 'Low' | 'Medium' | 'High'
export type TaskStatus = 'ToDo' | 'InProgress' | 'Done'

export interface Comment {
  id: number
  commentText: string
  author: string
  relatedTaskId: number
}
