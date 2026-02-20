# CoreTask
A simple To-Do Task Management App built with .NET 8 Web API and React + Vite. Perfect for managing tasks locally with a clean, scalable architecture.
# 📝 To-Do Task Management App

---

## 🚀 Tech Stack
- **Backend:** .NET 10 Web API  
- **Database:** EF Core with SQLite  
- **Frontend:** React + Vite  
- **Architecture:** Layered (Controller → Service → Repository)  


## 🏛 Architecture Decisions
- Layered architecture to separate concerns  
- DTOs for safe and clear data transfer  
- Service layer abstraction for business logic  
- Pagination implemented for scalability  


## 💡 Assumptions & Trade-offs
- **Single-user system** for MVP; multi-user support can be added later  
- **No authentication** required initially  
- Tasks stored locally in SQLite for simplicity  
- Trade-off: Using SQLite for the API provides real data persistence with minimal setup, while In-Memory for unit tests makes testing fast and isolated. The trade-off is that SQLite is lightweight but still not ideal for fully concurrent multi-user scenarios, whereas In-Memory cannot persist data across sessions. 
- Future production: PostgreSQL or another robust DB should be used  


## ⚡ Features
- Add, edit, delete, and view tasks  
- Mark tasks as completed  
- Pagination for large task lists  
- Frontend built with React + Vite for fast development  


## 🔮 Scalability Considerations
- Can easily extend to multi-tenant architecture  
- Database can migrate from SQLite → PostgreSQL with minimal changes  
- Backend designed to integrate authentication and roles in the future  



## 🛠 Future Improvements
- JWT-based authentication  
- Role-based access control  
- Dockerization for consistent environments  
- CI/CD pipeline for automated deployment  
- Comprehensive unit & integration tests  

---

## 📝 Setup & Installation

### 1️⃣ Clone the repository
```bash
git clone https://github.com/Fikifikinwa/CoreTask.git
cd CoreTask

### 2️⃣ Backend (CoreTask.Api)
```bash
cd CoreTask.Api       # go to backend
dotnet restore        # restore packages
dotnet ef database update  # create SQLite database
dotnet run            # run API on https://localhost:44337/api

### Swagger UI available at https://localhost:44337/

### 3️⃣ Frontend (React + Vite)
```bash
cd ../frontend
npm install           # install dependencies
npm run dev           # run frontend on http://localhost:5173

