<template>
  <form @submit.prevent="submit" >
    <label>Department Name</label>
    <input type="text" name="departmentName" v-model="departmentName"/>
    <button>Save</button>
</form>
</template>

<script>
import { onMounted, ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'
export default {
    name: "DepartmentEdit",
    props: ['id'],
    setup() {
       const departmentName = ref(''); 
       
       const router = useRouter();
       const route = useRoute()

       onMounted(async() => {
           const res = await fetch(`http://localhost:29827/api/Department/${route.params.id}`);
           const department = await res.json();
           departmentName.value = department.departmentName;
           
       })
       const submit = async () => {
           await fetch(`http://localhost:29827/api/Department/${route.params.id}`, {
               method: 'PUT',
               headers: {"Content-type": "application/json"},
               body: JSON.stringify({ 
                   departmentName: departmentName.value,
                   
                })
           });
           await router.push('/admin/departments');
       }
       
       return {departmentName, submit}
    }
    
}
</script>