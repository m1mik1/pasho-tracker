import TaskTable from '@/components/tasks/TaskTable';

export default function TasksPage() {
  return (
    <main className="p-6">
      <h1 className="text-2xl font-bold mb-4">List of tasks</h1>
      <TaskTable />
    </main>
  );
}
