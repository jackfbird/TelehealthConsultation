# 🩺 Telehealth Booking System

A sophisticated and comprehensive system for managing patient bookings. This project leverages cutting-edge technologies and best-in-class architectural patterns to deliver a seamless experience for both patients and administrators.

## 🏗️ System Architecture

### <img src="https://skillicons.dev/icons?i=vue" alt="Vue Logo" width="40" height="40" valign="middle"> &nbsp; <span style="vertical-align: middle;">Vue Frontend</span>
- **Tech Stack**: Vue 3, TypeScript, Vue Router, Axios, Options API
- **Features**:
  - Register new patients with `NewPatient.vue`.
  - Search for existing patients with `ExistingPatient.vue`.
  - Book desired timeslots using `TimeSlot.vue`.

### <img src="https://skillicons.dev/icons?i=dotnet" alt=".NET Logo" width="40" height="40" valign="middle"> &nbsp; <span style="vertical-align: middle;">.NET Web API Backend</span>
- Provides RESTful APIs for CRUD operations.
- Uses Entity Framework for database operations.
- Connects with a gRPC service for booking.

### <img src="https://skillicons.dev/icons?i=kafka" alt="Kafka Logo" width="40" height="40" valign="middle"> &nbsp; <span style="vertical-align: middle;">gRPC/Kafka Service</span>
- Handles booking operations.
- Communicates with an Azure cloud-hosted database.
- Produces messages to a Kafka broker on successful bookings.

### <img src="https://skillicons.dev/icons?i=dotnet" alt=".NET Logo" width="40" height="40" valign="middle"> &nbsp; <span style="vertical-align: middle;">.NET Console App (Kafka Consumer)</span>
- Consumes messages from the Kafka broker.
- Logs output for message consumption.

### <img src="https://skillicons.dev/icons?i=docker" alt="Docker Logo" width="40" height="40" valign="middle"> &nbsp; <span style="vertical-align: middle;">Docker</span>
- Orchestrates containerized services.

## 🔧 Setup & Usage

1. **Prerequisites**:
   - Ensure Docker and Docker Compose are installed.
  
2. **Initialization**:
   ```bash
   git clone 'https://github.com/jackfbird/TelehealthConsultation'
   cd TelehealthConsultation
   docker-compose up

3. **Web Interface**:
- Access `http://localhost:5173`
- Register or search as a patient.
- Book an available timeslot.

4. **Booking Process**:
- Bookings are persisted in an Azure database using Entity Framework.
- A Kafka message is produced post-booking.
- The .NET Console App consumes and logs this message.

## 🛠️ Tech Stack

- **Frontend**: Vue 3, TypeScript, Vue Router, Axios, Options API.
- **Backend**: .NET, Entity Framework.
- **Messaging**: Confluent.Kafka, Google Protobuf.
- **Containerization**: Docker.

## 🤝 Contributing

1. Fork the repository.
2. Create a new branch (`git checkout -b your-feature`).
3. Commit changes (`git commit -am 'Add feature'`).
4. Push the branch (`git push origin your-feature`).
5. Submit a pull request.
