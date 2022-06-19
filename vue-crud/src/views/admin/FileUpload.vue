<template>
    <form @submit.prevent="submit" enctype="multipart/form-data">
    <input type="text" name="FileUpload">
    <UploadImages ref="multipleFiles" @change= "handleFileUpload()" />
    <button>Add</button>
    </form>
</template>
<script>
import { onMounted} from 'vue'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import UploadImages from "@/components/DropFile";
export default{
    name: "FileUpload",
        components: {
        
        UploadImages,
    },
    setup() {
        const formData = new FormData()
        let multipleFiles=ref(null); 
       // var Filess=ref(null); 
        var uploadedFiles=[]
        const handleFileUpload = async() => { 
        for( var i = 0; i < multipleFiles.value.files.length; i++ ){
            formData.append('Files', multipleFiles.value.files[i]);
        }
        }
        const submit = async () => {
            //const formData = new FormData()
            // formData.files=Files
            
           await fetch('http://localhost:29827/api/Employee/uploadDocuments', {
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
       return { multipleFiles,submit,handleFileUpload}
    }


}
    

 
</script>


