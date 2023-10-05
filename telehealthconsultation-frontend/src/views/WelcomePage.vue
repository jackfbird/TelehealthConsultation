<template>
  <div class="welcome-container">
    <div class="content">
      <h1 class="display-4 mb-4">Telehealth Consultation</h1>
      <div class="button-section">
        <button @click="navigateTo('new-patient')" class="btn btn-primary mb-2">New Patient</button>
        <button @click="navigateTo('existing-patient')" class="btn btn-outline-primary mb-2">
          Existing Patient
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
export default {
  methods: {
    navigateTo(routeName: string) {
      this.$router.push(`/${routeName}`)
    }
  }
}
</script>

<style scoped>
.welcome-container {
  height: 100vh;
  background: linear-gradient(135deg, #e3f2fd, #90caf9); 
  display: flex;
  align-items: center;
  justify-content: center;
}

.content {
  text-align: center;
  background: #ffffff;
  padding: 40px;
  border-radius: 15px;
  box-shadow: 0px 4px 15px rgba(0, 0, 0, 0.1);
  width: 650px; 
  height: 450px; 
  position: relative;
  overflow-y: auto; 
}

.button-section {
  display: flex;
  gap: 15px;
  justify-content: center;
}

.btn-primary,
.btn-outline-primary {
  border: none;
  padding: 10px 30px; 
  width: 170px; 
  font-size: 0.9em; 
}

.btn-primary {
  background-color: #1e88e5;
  color: white;
}

.btn-primary:hover {
  background-color: #0d47a1;
  transition: 0.3s;
}

.btn-outline-primary {
  background-color: transparent;
  color: #1e88e5;
  border: 2px solid #1e88e5;
}

h1.display-4 {
  margin-top: 65px !important;
  margin-bottom: 70px !important;
}

.button-section {
  display: flex;
  gap: 30px; 
  justify-content: center;
  margin-top: 40px;
}

.btn-primary,
.btn-outline-primary {
  padding: 15px 50px; 
  width: 250px; 
  font-size: 1.1em;
}
</style>
