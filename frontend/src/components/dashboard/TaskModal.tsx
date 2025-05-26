'use client'

import { useState } from 'react'
import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { Textarea } from '@/components/ui/textarea'
import { Label } from '@/components/ui/label'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { TaskModel, TaskStatus, TaskPriority } from '@/types/task-model'

interface TaskModalProps {
  open: boolean
  setOpen: (open: boolean) => void
  initialData?: TaskModel
  onSave: (task: TaskModel) => void
}

export default function TaskModal({ open, setOpen, initialData, onSave }: TaskModalProps) {
  const [title, setTitle] = useState(initialData?.title || '')
  const [description, setDescription] = useState(initialData?.description || '')
  const [priority, setPriority] = useState<TaskPriority>(initialData?.priority || 'Medium')
  const [status, setStatus] = useState<TaskStatus>(initialData?.status || 'ToDo')
  const [deadline, setDeadline] = useState(initialData?.deadline || '')
  const [assignedUserId, setAssignedUserId] = useState(initialData?.assignedUserId || '')

  const handleSave = () => {
    const task: TaskModel = {
      id: initialData?.id || Date.now(), // temp id
      title,
      description,
      priority,
      status,
      deadline,
      assignedUserId,
      comments: initialData?.comments || [],
      categoryId: initialData?.categoryId || 1, // temp or dynamic later
    }
    onSave(task)
    setOpen(false)
  }

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogContent className="max-w-lg bg-white p-6 rounded-xl">
        <DialogHeader>
          <DialogTitle className="text-pink-500 text-2xl">{initialData ? 'Edit Task' : 'New Task'}</DialogTitle>
        </DialogHeader>

        <div className="space-y-4">
          {/* Title */}
          <div>
            <Label>Title</Label>
            <Input value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Task Title" />
          </div>

          {/* Description */}
          <div>
            <Label>Description</Label>
            <Textarea value={description} onChange={(e) => setDescription(e.target.value)} placeholder="Task Description" />
          </div>

          {/* Priority */}
          <div>
            <Label>Priority</Label>
            <Select value={priority} onValueChange={(value) => setPriority(value as TaskPriority)}>
              <SelectTrigger>
                <SelectValue placeholder="Select Priority" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Low">Low</SelectItem>
                <SelectItem value="Medium">Medium</SelectItem>
                <SelectItem value="High">High</SelectItem>
              </SelectContent>
            </Select>
          </div>

          {/* Status */}
          <div>
            <Label>Status</Label>
            <Select value={status} onValueChange={(value) => setStatus(value as TaskStatus)}>
              <SelectTrigger>
                <SelectValue placeholder="Select Status" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="ToDo">To Do</SelectItem>
                <SelectItem value="InProgress">In Progress</SelectItem>
                <SelectItem value="Done">Done</SelectItem>
              </SelectContent>
            </Select>
          </div>

          {/* Deadline */}
          <div>
            <Label>Deadline</Label>
            <Input type="date" value={deadline.slice(0, 10)} onChange={(e) => setDeadline(e.target.value)} />
          </div>

          {/* Assigned User */}
          <div>
            <Label>Assigned User</Label>
            <Input value={assignedUserId} onChange={(e) => setAssignedUserId(e.target.value)} placeholder="User ID" />
          </div>

          {/* Actions */}
          <div className="flex justify-end space-x-2">
            <Button variant="outline" onClick={() => setOpen(false)}>
              Cancel
            </Button>
            <Button className="bg-pink-500 hover:bg-pink-600 text-white" onClick={handleSave}>
              Save
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  )
}
