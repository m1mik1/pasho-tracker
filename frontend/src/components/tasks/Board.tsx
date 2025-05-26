'use client'

import { TaskColumn } from './TaskColumn'

export const Board = () => {
    return (
      <div className="flex space-x-4 overflow-x-auto p-4 h-[calc(100vh-5rem)]">
        <TaskColumn title="To Do" />
        <TaskColumn title="In Progress" />
        <TaskColumn title="Done" />
      </div>
    )
  }
  
