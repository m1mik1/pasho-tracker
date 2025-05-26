import { Header } from '@/components/dashboard/header/DashboardHeader'

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <>
      <Header />
      <main className="h-full w-full overflow-hidden">{children}</main>
    </>
  )
}
