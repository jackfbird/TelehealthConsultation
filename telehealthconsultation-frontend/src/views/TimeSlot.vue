<template>
  <div class="container">
    <div class="content">
      <div @click="goBack" class="back-icon">←</div>
      <h1 class="display-4 mb-4">Available Time Slots</h1>
      <ul class="time-slots-list">
        <li v-for="slot in timeSlots" :key="slot.id">
          <button @click="bookTimeSlot(slot.startTime)" class="btn btn-primary">
            {{ formatDate(slot.startTime) }}
          </button>
        </li>
      </ul>
    </div>
  </div>
</template>

<script lang="ts">
import axios from 'axios'
import { AxiosError } from 'axios'

interface TimeSlot {
  id: number
  startTime: string
}

export default {
  data() {
    return {
      timeSlots: [] as TimeSlot[],
      isBooking: false
    }
  },
  mounted() {
    this.fetchTimeSlots()
  },
  methods: {
    async fetchTimeSlots() {
      try {
        const response = await axios.get('https://localhost:7036/api/timeslots/available')
        this.timeSlots = response.data
      } catch (error) {
        console.error('Error fetching time slots:', error)
        this.$toast.open({
          message: 'Something went wrong fetching the time slots!',
          type: 'error'
        })
      }
    },
    goBack() {
      this.$router.go(-1)
    },
    formatDate(dateStr: string): string {
      const date = new Date(dateStr)
      const month = (date.getMonth() + 1).toString().padStart(2, '0') 
      const day = date.getDate().toString().padStart(2, '0')
      const year = date.getFullYear()

      const hours = date.getHours() % 12 || 12
      const minutes = date.getMinutes().toString().padStart(2, '0')
      const ampm = date.getHours() < 12 ? 'AM' : 'PM'

      return `${month}/${day}/${year} ${hours}:${minutes} ${ampm}`
    },
    async bookTimeSlot(startTime: string) {
      if (this.isBooking) {
        return 
      }

      this.isBooking = true

      try {
        const bookingData = {
          PatientId: 1, 
          DoctorId: 1, 
          BookingDate: new Date().toISOString().split('T')[0], 
          StartTime: startTime,
          EndTime: new Date(new Date(startTime).getTime() + 60 * 60 * 1000).toISOString() 
        }

        await axios.post('https://localhost:7036/api/bookings', bookingData)

        this.$toast.open({
          message: 'Time slot booked successfully!',
          type: 'success'
        })

        this.fetchTimeSlots() 
      } catch (error) {
        const axiosError = error as AxiosError; 
        console.error('Error message:', axiosError?.response?.data);

        this.$toast.open({
          message: 'Something went wrong booking the time slot!',
          type: 'error'
        })
      } finally {
        this.isBooking = false
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
.time-slots-list {
  list-style-type: none;
  padding: 0;
  margin: 0;
}
.btn-primary {
  margin: 10px 0;
  padding: 10px 30px;
  font-size: 0.9em;
  background-color: #1e88e5;
  color: white;
  border: none;
  cursor: pointer;
  transition: background-color 0.3s;
  width: 100%; 
}
.btn-primary:hover {
  background-color: #0d47a1;
}
</style>
