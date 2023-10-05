import { createRouter, createWebHistory } from 'vue-router'
import WelcomePage from '../views/WelcomePage.vue'
import NewPatient from '../views/NewPatient.vue'
import ExistingPatient from '../views/ExistingPatient.vue'
import TimeSlot from '../views/TimeSlot.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'WelcomePage',
      component: WelcomePage
    },
    {
      path: '/new-patient',
      name: 'NewPatient',
      component: NewPatient
    },
    {
      path: '/existing-patient',
      name: 'ExistingPatient',
      component: ExistingPatient
    },
    {
      path: '/time-slot',
      name: 'TimeSlot',
      component: TimeSlot
    }
  ]
})

export default router
