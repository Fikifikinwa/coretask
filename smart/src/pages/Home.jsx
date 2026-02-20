import TaskForm from "../components/TaskForm";
import TaskList from "../components/TaskList";

const Home = () => {
  return (
    <div className="container py-5">
      <div className="card shadow-lg">
        <div className="card-body">
          <h1 className="text-center mb-4 text-success">
            Smart To-Do App
          </h1>

          <TaskForm />

          <hr className="my-4" />

          <TaskList />
        </div>
      </div>
    </div>
  );
};

export default Home;