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
  background: linear-gradient(135deg, #e3f2fd, #90caf9); /* Soft shades of blue */
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
  width: 650px; /* Fixed width */
  height: 450px; /* Fixed height */
  position: relative;
  overflow-y: auto; /* In case the content overflows due to the fixed height, this allows scrolling */
}

.button-section {
  display: flex;
  gap: 15px;
  justify-content: center;
}

.btn-primary,
.btn-outline-primary {
  border: none;
  padding: 10px 30px; /* Increased the horizontal padding to give more space */
  width: 170px; /* Increased width to fit the text on one line */
  font-size: 0.9em; /* Slightly reduced the font size for a better fit */
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
  margin-bottom: 70px !important; /* Adjusted spacing between the title and buttons */
}

.button-section {
  display: flex;
  gap: 30px; /* Increased gap between the buttons */
  justify-content: center;
  margin-top: 40px; /* Adjusted margin to give more space between buttons */
}

.btn-primary,
.btn-outline-primary {
  padding: 15px 50px; /* Increased the padding to make the buttons larger */
  width: 250px; /* Increased width */
  font-size: 1.1em; /* Increased the font size for more prominence */
}
</style>
