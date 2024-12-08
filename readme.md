# **Properly**

**Properly** is a full-stack real estate web application, designed to help users find their dream home.

## **Tech Stack**

### **Frontend:**

- React
- TypeScript
- TailwindCSS
- Shadcn-ui
- Axios
- React Router
- Zustand
- React Hook Form
- Zod
- Cloudinary
- React Toastify
- React Icons

### **Backend:**

- ASP.NET
- Entity Framework Core
- SQL Server
- OpenAPI

## **Startup Guide**

> **IMPORTANT:**
>
> 1. Update the connection string in the `appsettings.development.json` file with your own to allow the app to create the database.
> 2. Check the localhost links in both the `appsettings.development.json` file (API) and the `.env` file (frontend). Ensure the ports match your machine's configuration. **Otherwise, the application WON'T WORK!**

---

### **Steps**

#### **API Setup**

1. **Pull the repository.**
2. **Navigate to the `api` folder of the project.**
3. **Open the folder using your favorite IDE (e.g., Visual Studio).**
4. **Update the `ConnectionStrings:DevConnection` with your connection string in `appsettings.development.json`.**
5. **Start the project using the Visual Studio Debugger.**
   - The database will be created and seeded upon starting the project.
   - The initial configuration is in `appsettings.development.json`.
   - Test user accounts are pre-seeded. You can also register your own accounts.
   - **Admin Account:** The credentials are defined in the `appsettings.development.json` file.
     - This account cannot be modified or deleted.
     - No additional admin accounts can be created.
6. **If the localhost ports differ on your machine, update them in the configuration files.**

#### **Frontend Setup**

7. **Navigate to the `frontend` folder of the project.**
8. **Open the folder using your favorite IDE (e.g., Visual Studio Code).**
9. **Install the necessary npm packages:**
   ```bash
   npm install
   ```
10. **Verify and update the `.env` file:**
    - Ensure the API URL matches the port on which your backend is running.
11. **Start the frontend application:**
    ```bash
    npm run dev
    ```
12. **Access the application:**
    - Open the link provided in the terminal.
    - You should be redirected to the **Properly** home screen.
13. **If no data is displayed:**
    - Verify the API URL in the `.env` file matches the port used for the backend.
    - Update if necessary.

---

Let me know if you encounter any issues. 😊
