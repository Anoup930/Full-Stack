<template>
  <router-link :to="{ name: 'DepartmentCreate'}" class="btn">Add Department</router-link>
  <table>
    <thead>
      <tr>
        <th>#id</th>
        <th>DepartmentName</th>
        <th>Action</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="department in departments" :key="department.id">
        <td>{{department.departmentId}}</td>
        <td>{{department.departmentName}}</td>
        
        <td>
          
          <router-link :to="{ name: 'DepartmentEdit', params: { id: department.departmentId }}" class="btn">Edit</router-link>
          <button @click="del(department.departmentId)" class="btn btn-del">Delete</button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
import {onMounted, ref} from 'vue';
export default {
  name: 'Departments',
  setup() {
    let departments = ref([]);
    onMounted( async () => {
     await getDepts()
    })
    async function getDepts(){
      const res = await fetch('http://localhost:29827/api/Department/Get');
      departments.value = await res.json();
    }
    const del = async (id) => {
      await fetch(`http://localhost:29827/api/Department?id=${id}`, {
        method: 'DELETE'
      }).then((res)=>{
         getDepts()
         alert('Are you sure?')

        

      }, 
      (error) => {
        console.log(error);
        alert('Error occurred')
      })
      
    }
    return { departments, del }
    
  }
}
</script>