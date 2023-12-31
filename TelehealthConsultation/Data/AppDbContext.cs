﻿using Microsoft.EntityFrameworkCore;
using TelehealthConsultation.Models;

namespace TelehealthConsultation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<TimeSlot> TimeSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<TimeSlot>().ToTable("TimeSslots");
        }
    }
}