<template>
  <form @submit.prevent="submit" >
    <input type="text" name="employeeName" v-model="employeeName"/>
    <label>Department</label>

    <select name="department" v-model="departmentName">

        <template v-for="department in departments" :key="department.departmentId">
        <option :value="department.departmentName" v-if="(department.departmentName == departmentName)?'selected':''">{{department.departmentName}}</option>
        <option :value="department.departmentName" v-else>{{department.departmentName}}</option>
        </template>
    </select>
    
    <label>DateOfJoining</label>
    <input type="text" name="dateOfJoining" v-model="dateOfJoining"/>
    
    <div v-if="showPhoto && photoFileNameNew !=null">
        <label>PhotoFileName</label>
        <img v-if="photoFileNameNew !=null" :src="convertArrayBufferToBase64(photoFileNameNew)" width="250" style="border-radius: 20px;">
        <img src="../../assets/delete-icon.png" alt=""
            @click="deleteImage(employeeId)"
            style="margin-left: -37px;position: absolute;width: 40px;margin-top: -3px;cursor:pointer">
    </div>
    
    <br>
    <button @click.prevent="showUploadOption = !showUploadOption" style="background-color:orange;border-radius:4px">
    <span v-if="showUploadOption">Hide Image Upload</span>
    <span v-else>Open Upload Image</span>
    </button>
    <br>
    <UploadImages ref="photoFileName" v-if="showUploadOption" @change= "handleFileUpload()" />


    
    
    <button>Update</button>
</form>
</template>

<script>
import UploadImages from "@/components/vue-upload-drop-images";
import { onMounted, ref } from 'vue'
import { useRouter, useRoute } from 'vue-router'
export default {
    name: "EmployeeEdit",
    components: {
        
        UploadImages,
    },
    props: ['id'],
    setup() {
        function convertArrayBufferToBase64(arrayBuffer){
            isPhotoNull.value = false;
            return "data:image/jpeg;base64,"+  arrayBuffer.toString('base64');
        }
        function deleteImage(empId){
            if(confirm('Are you sure to delete image?')){
                isPhotoNull.value = true;
                showPhoto.value = false

               //del(empId)
            }
        }
        const del = async (id) => {
            await fetch(`http://localhost:29827/api/Employee/deleteImage/${id}`, {
            method: 'DELETE'
           }).then((res)=>{
                console.log(res)
                showPhoto.value = false
                alert('Succeffully deleted')
        }, 
      (error) => {
        console.log(error);
        alert('Error occurred')
      })
      
    }
       const showPhoto = ref(true);
       const isPhotoNull = ref(true);
       const showUploadOption = ref(false);
       const employeeName = ref('');
       const employeeId = ref('');
       const departmentName=ref('');
       const dateOfJoining=ref('');
       let photoFileName=ref(''); 
       const photoFileNameNew=ref(''); 
       const departmentId=ref(''); 
       const departments = ref([])
       
       const router = useRouter();
       const route = useRoute()
        async function getDepts(){
            const res = await fetch('http://localhost:29827/api/Department/Get');
            departments.value = await res.json();
        }
        const handleFileUpload = async() => {
           
            photoFileName = photoFileName.value.files[0]

        }
       onMounted(async() => {
           const res = await fetch(`http://localhost:29827/api/Employee/Get/${route.params.id}`);
           const employee = await res.json();
           employeeId.value = employee.employeeId;
           employeeName.value = employee.employeeName;
           departmentName.value= employee.department;
           dateOfJoining.value=employee.dateOfJoining;
           photoFileNameNew.value=employee.photoFileName;
           if(employee.photoFileName == null){
               isPhotoNull.value = true
           }
           departmentId.value=employee.departmentId;
           await getDepts()
       })
    //    const submit = async () => {
    //        await fetch(`http://localhost:29827/api/Employee/${route.params.id}`, {
    //            method: 'PUT',
    //            headers: {"Content-type": "application/json"},
    //            body: JSON.stringify({ 
    //                employeeName: employeeName.value,
    //                department: departmentName.value,
    //                dateOfJoining: dateOfJoining.value,
    //                photoFileName: photoFileName.value,
                   
    //             })
    //        });
    //        await router.push('/admin/employees');
    //    }

           const submit = async () => {
            const formData = new FormData()
            formData.append('PhotoFile', photoFileName)
            formData.append('employeeName', employeeName.value)
            formData.append('department', departmentName.value)
            formData.append('dateOfJoining', dateOfJoining.value)
            formData.append('IsPhotoNull', isPhotoNull.value)
           await fetch(`http://localhost:29827/api/Employee/${route.params.id}`, {
               method: 'PUT',  
               body: formData
           })
           .then(response => response.json())
            .then(data => {
                
                console.log(data)
            })
            .catch((error) => {
                console.log(error)
            });
           await router.push('/admin/employees');
       }
       
       return {showUploadOption,showPhoto,employeeId,departments,employeeName,departmentName,dateOfJoining,photoFileNameNew,photoFileName
       ,submit,convertArrayBufferToBase64,deleteImage,handleFileUpload}
    }
    
}
</script>