<template>
  <div class="container">
    <div class="content">
      <div @click="goBack" class="back-icon">←</div>
      <h1 class="display-4 mb-4">Existing Patient</h1>
      <form @submit.prevent="fetchPatientByEmail">
        <div class="form-group">
          <input type="text" class="form-control" placeholder="Name" v-model="patient.name" />
        </div>
        <div class="form-group">
          <input type="email" class="form-control" placeholder="Email" v-model="patient.email" />
        </div>
        <div class="form-group">
          <input type="date" class="form-control" v-model="patient.dateOfBirth" />
        </div>
        <div class="btn-section">
          <button type="submit" class="btn btn-primary" :disabled="isSubmitting">Search</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts">
import axios from 'axios'

export default {
  data() {
    return {
      patient: {
        name: '',
        email: '',
        dateOfBirth: ''
      },
      isSubmitting: false
    }
  },
  methods: {
    goBack() {
      this.$router.go(-1)
    },
    async fetchPatientByEmail() {
      if (this.isSubmitting) {
        return 
      }

      this.isSubmitting = true

      try {
        const response = await axios.get(
          `https://localhost:7036/api/patients/by-email?email=${this.patient.email}`
        )
        if (response.data) {
          this.$router.push('/time-slot')
        }
      } catch (error) {
        console.error('Error fetching patient:', error)
        this.$toast.open({
          message: 'Patient not found or something went wrong!',
          type: 'error'
        })
      } finally {
        this.isSubmitting = false
      }
    }
  }
}
</script>

<style scoped>
.container {
  height: 100vh;
  background: linear-gradient(135deg, #e3f2fd, #90caf9); 
  display: flex;
  align-items: center;
  justify-content: center;
  max-width: 100%;
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
.back-icon {
  position: absolute;
  top: 20px;
  left: 20px;
  font-size: 1.5em;
  cursor: pointer;
}

.form-group {
  margin-bottom: 20px;
}

.form-control {
  padding: 10px 20px;
  border-radius: 5px;
  border: 1px solid #b0bec5;
  width: 100%;
}

.btn-section {
  display: flex;
  gap: 15px;
  justify-content: center;
  margin-top: 20px;
}

.btn-primary {
  padding: 10px 30px;
  font-size: 0.9em;
  background-color: #1e88e5;
  color: white;
}

.btn-primary:hover {
  background-color: #0d47a1;
  transition: 0.3s;
}

.btn-primary,
.btn-outline-primary {
  padding: 15px 50px;
  width: 250px; 
  font-size: 1.1em; 
}
</style>
