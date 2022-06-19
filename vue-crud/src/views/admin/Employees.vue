<template>
  <router-link :to="{ name: 'EmployeeCreate'}" class="btn">Add Employee</router-link>
  <table>
    <thead>
      <tr>
        <th>#id</th>
        <th>EmployeeName</th>
        <th>Department</th>
        <th>DateOfJoining</th>
        <th>PhotoFileName</th>
        <th>Action</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="employee in employees" :key="employee.id">
        <td>{{employee.employeeId}}</td>
        <td>{{employee.employeeName}}</td>
        <td>{{employee.department}}</td>
        <td>{{employee.dateOfJoining}}</td>
        <td>
          
          <img v-if="employee.photoFileName != null" :src="convertArrayBufferToBase64(employee.photoFileName)" class="padding">
          <img v-else src="../../assets/anonymous.png" class="padding">
          
                
        </td>
        
        <td>
          
          <router-link :to="{ name: 'EmployeeEdit', params: { id: employee.employeeId }}" class="btn">Edit</router-link>
          <button @click="del(employee.employeeId)" class="btn btn-del">Delete</button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
import {onMounted, ref} from 'vue';
export default {
  name: 'Employees',
  setup() {
    function convertArrayBufferToBase64(arrayBuffer){

      return "data:image/jpeg;base64,"+  arrayBuffer.toString('base64');
    }
    let employees = ref([]);
    onMounted( async () => {
     await getEmps()
    })
    async function getEmps(){
      const res = await fetch('http://localhost:29827/api/Employee/Get');
      employees.value = await res.json();
    }

    const del = async (id) => {
      await fetch(`http://localhost:29827/api/Employee?id=${id}`, {
        method: 'DELETE'
      }).then((res)=>{
         getEmps()
         alert('Are you sure?')



      }, 
      (error) => {
        console.log(error);
        alert('Error occurred')
      })
      
    }
    return { employees, del,convertArrayBufferToBase64 }
    
  }
}
</script>