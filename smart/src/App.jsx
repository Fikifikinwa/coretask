import { TaskProvider } from "./context/TaskContext";
import Home from "./pages/Home";

function App() {
  return (
    <TaskProvider>
      <Home />
    </TaskProvider>
  );
}

export default App;
