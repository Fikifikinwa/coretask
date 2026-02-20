import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:44337/api",
  headers: { "Content-Type": "application/json" },
});

// unwrap response.data
const apiCall = (promise) => promise.then(res => res.data);

// Tasks
export const getTasks = (params) =>
  apiCall(api.get("/tasks", { params }));

export const getTaskById = (id) =>
  apiCall(api.get(`/tasks/${id}`));

export const createTask = (task) =>
  apiCall(api.post("/tasks", task));

export const updateTask = (id, task) =>
  apiCall(api.put(`/tasks/${id}`, task));

export const deleteTask = (id) =>
  api.delete(`/tasks/${id}`); // no apiCall here needed