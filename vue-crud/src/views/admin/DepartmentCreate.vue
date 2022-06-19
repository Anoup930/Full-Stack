<template>
  <form @submit.prevent="submit" >
    <label>DepartmentName</label>
    <input type="text" name="departmentName" v-model="departmentName"/>
    <button>Add Product</button>
</form>
</template>

<script>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
export default {
    name: "DepartmentCreate",
    setup() {
       const departmentName = ref(''); 
       
       const router = useRouter();

       const submit = async () => {
           await fetch('http://localhost:29827/api/Department', {
               method: 'POST',
               headers: {"Content-type": "application/json"},
               body: JSON.stringify({ 
                   departmentName: departmentName.value,
                   
                })
           });
           await router.push('/admin/departments');
       }
       
       return {departmentName,submit}
    }
    
}
</script>
