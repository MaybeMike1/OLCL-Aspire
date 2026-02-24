import { type FC } from 'react'
import './App.css'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient()

const App: FC = () => {
  return (
    <>
      <QueryClientProvider client={queryClient}>
        <div style={{ display: "flex", justifyContent: "center", alignItems: "center", height: "100vh", flexDirection: "column" }}>
          OLCL
        </div>
      </QueryClientProvider>
    </>
  )
}

export default App
