import { useTasks } from "../context/TaskContext";

const TaskItem = ({ task }) => {
  const { removeTask, updateTask } = useTasks();

  const getPriorityBadgeClass = (priority) => {
    switch (priority) {
      case "High":
        return "badge bg-danger";
      case "Medium":
        return "badge bg-warning text-dark";
      case "Low":
        return "badge bg-success";
      default:
        return "badge bg-secondary";
    }
  };

  const formatDueDate = (dateString) => {
    if (!dateString) return "";
    const date = new Date(dateString);
    return date.toLocaleDateString(undefined, {
      year: "numeric",
      month: "short",
      day: "numeric",
    });
  };

  return (
  <li className="list-group-item d-flex justify-content-between align-items-center">
    <div className="d-flex align-items-start">
      <input
        type="checkbox"
        checked={task.isCompleted}
        onChange={(e) =>
          updateTask(task.id, e.target.checked)
        }
        className="form-check-input me-2 mt-1"
      />

      <div>
        <div
          className={
            task.isCompleted
              ? "text-decoration-line-through"
              : ""
          }
        >
          {task.title}
        </div>

        {task.dueDate && (
          <small className="text-muted">
            📅 {formatDueDate(task.dueDate)}
          </small>
        )}
      </div>
    </div>

    <div className="d-flex align-items-center gap-2">
      <span className={getPriorityBadgeClass(task.priority)}>
        {task.priority}
      </span>

      <button
        onClick={() => removeTask(task.id)}
        className="btn btn-sm btn-outline-danger"
      >
        Delete
      </button>
    </div>
  </li>
  );
};

export default TaskItem;