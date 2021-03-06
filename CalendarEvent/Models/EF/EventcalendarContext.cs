﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CalendarEvent.Models.EF
{
    public partial class EventcalendarContext : DbContext
    {
        public EventcalendarContext()
        {
        }

        public EventcalendarContext(DbContextOptions<EventcalendarContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EventScheduler> EventScheduler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventScheduler>(entity =>
            {
                entity.HasKey(e => e.Eventid)
                    .HasName("PK_EventScheduler1");

                entity.Property(e => e.CreateBy).IsFixedLength();

                entity.Property(e => e.UpdatedBy).IsFixedLength();

                entity.Property(e => e.Userid).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}