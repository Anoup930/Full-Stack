<template>
  <div class="sidebar">
    <ul class="side-menu">
        <li><router-link :to="{ name: 'Departments'}">Departments</router-link></li>
        <li><router-link :to="{ name: 'DepartmentCreate'}">Add Department</router-link></li>
        <li><router-link :to="{ name: 'Employees'}">Employees</router-link></li>
        <li><router-link :to="{ name: 'EmployeeCreate'}">Add Employee</router-link></li>
        <li><router-link :to="{ name: 'FileUpload'}">FileUpload</router-link></li>
    </ul>
</div>
</template>

<script>
export default {
    name: 'SideBar'
}
</script>