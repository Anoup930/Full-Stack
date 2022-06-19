<template>
  <form @submit.prevent="submit" enctype="multipart/form-data">
    <label>EmployeeName</label>
    <input type="text" name="employeeName" v-model="employeeName"/>
    <label>Department</label>

    <select name="department" v-model="departmentName">
        <option value="">Select Department</option>
        <template v-for="department in departments" :key="department.departmentId">
        <option :value="department.departmentName">{{department.departmentName}}</option>
        </template>
    </select>
    <label>DateOfJoining</label>
    <input type="text" name="dateOfJoining" v-model="dateOfJoining"/>
    <label>PhotoFileName</label>

    <br/>
 
    <UploadImages ref="photoFileName" @change= "handleFileUpload()" />

    <button>Add</button>
  </form>
</template>

<script>

import UploadImages from "@/components/vue-upload-drop-images";
import { onMounted} from 'vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router' 
export default {
    name: "EmployeeCreate",
    components: {
        
        UploadImages,
    },

    setup() {



        

        const employeeName = ref('');
        const departmentName=ref('');
        const departments = ref([]);
        const dateOfJoining=ref('');
        let photoFileName=ref(null); 
       
       
       
        const handleFileUpload = async() => {
           
           photoFileName = photoFileName.value.files[0]

        }

       const router = useRouter();
        async function getDepts(){
            const res = await fetch('http://localhost:29827/api/Department/Get');
            departments.value = await res.json();

        }
       onMounted(async() => {

           await getDepts()
       })
       const submit = async () => {
            const formData = new FormData()
            formData.append('PhotoFile', photoFileName)
            formData.append('employeeName', employeeName.value)
            formData.append('department', departmentName.value)
            formData.append('dateOfJoining', dateOfJoining.value)
           await fetch('http://localhost:29827/api/Employee', {
               method: 'POST',  
               body: formData
           })
           .then(response => response.json())
            .then(data => {
                JSON.stringify(data)
                console.log(data)
            })
            .catch(error => {
                console.error(error)
            });
           await router.push('/admin/employees');
       }
       
       return {departments,employeeName,departmentName,dateOfJoining,photoFileName,submit,handleFileUpload}
    }
    
}
</script>
<style scoped>

</style>
