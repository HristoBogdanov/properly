---

# **Propely**

**Properly** is a full stack real estate web application, designed to help users find their dream home.

## **Tech Stack**

### **Frontend:**
- !React React
- !TypeScript TypeScript
- !TailwindCSS TailwindCSS
- !Shadcn-ui Shadcn-ui
- !Axios Axios
- !React Router React Router
- !Zustand Zustand
- !React Hook Form React Hook Form
- !Zod Zod
- !Cloudinary Cloudinary
- !React Toastify React Toastify
- !React Icons React Icons

### **Backend:**
- !ASP.NET ASP.NET
- !Entity Framework Core Entity Framework Core
- !SQL Server SQL Server
- !OpenAPI OpenAPI

## **Startup Guide**
> **IMPORTANT:** You have to change the connection string from the appsettings.development.json file with your own, in order for the app to create the database.

> **IMPORTANT:** The only other settings in the configuration files (`appsettings.development.json` for the API and `.env` for the frontend) that you may need to change are the localhost links, because your ports might be different. Please make sure that you go through both files, find the mismatched port numbers, and correct them with your own. Otherwise, the application **WON'T WORK!**

### **Steps:**

1. **Pull the repository.**
2. **Navigate to the `api` folder of the project.**
3. **Open the folder using your favorite IDE.** The following instructions are for Visual Studio.
4. **Update the ConnectionStrings:DevConnection with your own connection string.
5. **Start the project using the Visual Studio Debugger.** The database will be created and seeded upon starting the project. The configuration that is used is located in the `appsettings.development.json` file. There are several user accounts seeded that you can use for testing, or you can register your own.
   - **NOTE:** The admin account is seeded initially with the credentials from the `appsettings.development.json` file. Use these credentials to test admin functionalities. These credentials cannot be tampered with, the account cannot be deleted, and other admin accounts cannot be created.
6. **Change the localhost ports if they are different for your machine.**

7. **Navigate to the `frontend` folder of the project.**
8. **Open the folder using your favorite IDE.** The following instructions are for Visual Studio Code.
9. **Open a terminal in the same directory and install the necessary npm packages:**
   ```bash
   npm install
   ```
10. **Look over the `.env` file and change the mismatching localhost ports.** Make sure that the API port is the same as the one you started your backend on.
11. **Start the application:**
    ```bash
    npm run dev
    ```
12. **Open the link provided to you in the terminal.**
13. **The app should work now and you should be redirected to the home screen of the Properly website.**
14. **If you don't see any data displayed, then you probably forgot to change the API URL in the `.env` file.** Go through it once again and make sure that it matches the port on your machine.

---

I hope this helps! Let me know if you need any further assistance. 😊
