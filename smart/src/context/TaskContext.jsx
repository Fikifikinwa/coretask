import { createContext, useContext, useState, useEffect } from "react";
import * as api from "../services/api";

const TaskContext = createContext();

export const useTasks = () => useContext(TaskContext);

export const TaskProvider = ({ children }) => {
  const [tasks, setTasks] = useState([]);
  const [loading, setLoading] = useState(false);

  const fetchTasks = async () => {
  setLoading(true);
  try {
    const response = await api.getTasks();
    if(!response.success)
      console.log(response.message);
    else
      setTasks(response.data.items);
  } catch (err) {
    console.error(err);
  } finally {
    setLoading(false);
  }
};

  const addTask = async (task) => {
    const response = await api.createTask(task);
     if(!response.success)
      console.log(response.message);
    else
      setTasks((prev) => [...prev, response.data]);
  };

  const removeTask = async (id) => {
    const response = await api.deleteTask(id);
    if(!response.success)
      console.log(response.message);
    else
      setTasks((prev) => prev.filter((t) => t.id !== id));
  };

const updateTask = async (id, isCompleted) => {
  const task = tasks.find((t) => t.id === id);
  if (!task) return;

  setTasks((prev) =>
    prev.map((t) =>
      t.id === id ? { ...t,  isCompleted } : t
    )
  );

  try {
    const response = await api.updateTask(id, {
      ...task,
       isCompleted,
    });
    if(!response.success)
      console.log(response.message);
  } catch (err) {
    console.error(err);
    //rollback
     setTasks((prev) =>
      prev.map((t) =>
        t.id === id ? { ...t, isCompleted: !isCompleted } : t
      )
    );
  }
};

  useEffect(() => {
    fetchTasks();
  }, []);

  return (
    <TaskContext.Provider
      value={{ tasks, loading, addTask, removeTask, updateTask }}
    >
      {children}
    </TaskContext.Provider>
  );
};
