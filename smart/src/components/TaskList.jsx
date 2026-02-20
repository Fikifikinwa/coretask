import TaskItem from "./TaskItem";
import { useTasks } from "../context/TaskContext";

const TaskList = () => {
  const { tasks, loading } = useTasks();

  if (loading) return <p>Loading tasks...</p>;
  if (!tasks.length) return <p>No tasks yet!</p>;

  return (
    <ul className="list-group mb-3">
      {tasks.map((task) => (
        <TaskItem key={task.id} task={task} />
      ))}
    </ul>
  );
};

export default TaskList;