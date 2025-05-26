export type TaskStatus = 'ToDo' | 'InProgress' | 'Done'

export type TaskPriority = 'Low' | 'Medium' | 'High'

export interface CommentModel {
  id: number
  commentText: string
  author: string
  relatedTaskId: number
}

export interface TaskModel {
  id: number
  title: string
  description: string
  priority: TaskPriority
  status: TaskStatus
  deadline: string // ISO string
  assignedUserId: string
  comments: CommentModel[]
  categoryId: number
}

export interface CategoryModel {
  id: number
  categoryName: string
}
