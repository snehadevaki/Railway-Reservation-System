using Microsoft.EntityFrameworkCore;
using RailwaySystem.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RailwaySystem.API.Data
{
    public class TrainDbContext : DbContext
    {
        public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options)
        {

        }
        public DbSet<Train> trains { get; set; }
        public DbSet<Quota> quotas { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Tickets> tickets { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Seat> seat { get; set; }
        public DbSet<BankCred> bankCred { get; set; }
        public DbSet<Transaction> transaction { get; set; }
        public DbSet<Passenger> passenger { get; set; }
    }
}