import { createRouter, createWebHistory } from 'vue-router'
import Main from '../views/Main.vue'
import Admin from '../views/admin/Admin.vue'
import Departments from '../views/admin/Departments.vue'
import DepartmentCreate from '../views/admin/DepartmentCreate.vue'
import DepartmentEdit from '../views/admin/DepartmentEdit.vue'
import Employees from '../views/admin/Employees.vue'
import EmployeeCreate from '../views/admin/EmployeeCreate.vue'
import EmployeeEdit from '../views/admin/EmployeeEdit.vue'
import FileUpload from '../views/admin/FileUpload.vue'

const routes = [
  {
    path: '/',
    name: 'Main',
    component: Main
  },
  {
    path: '/admin',
    name: 'Admin',
    component: Admin,
    children:[
      {
        path: 'departments',
        component: Departments,
        name: 'Departments'
      },
      {
        path: 'departments/create',
        component: DepartmentCreate,
        name: 'DepartmentCreate'
      },
      {
        path: 'departments/:id/edit',
        component: DepartmentEdit,
        name: 'DepartmentEdit',
        props:true
      },
      {
        path: 'employees',
        component: Employees,
        name: 'Employees'
      },
      {
        path: 'employees/create',
        component: EmployeeCreate,
        name: 'EmployeeCreate'
      },
      {
        path: 'employees/:id/edit',
        component: EmployeeEdit,
        name: 'EmployeeEdit',
        props:true
      },
      {
        path: 'fileupload',
        component: FileUpload,
        name: 'FileUpload'
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
