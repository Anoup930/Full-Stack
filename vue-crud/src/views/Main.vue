<template>
  <div class="welcome">Welcome to Vue : )</div>
  <br>

  

  <router-link :to="{ name: 'Admin'}" class="btn">Admin pannel</router-link>
</template>

<script>

export default {
    name: "Main",
}
</script>

<style>

</style>