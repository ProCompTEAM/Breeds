using Common;
using Common.Constants;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DogBreed> DogBreeds { get; set; }

        public DbSet<ChildDogBreed> ChildDogBreeds { get; set; }

        private Properties Properties { get; set; }

        private MySqlConnectionStringBuilder Builder { get; set; }

        private string dbAddress;
        private string dbName;
        private string dbUser;
        private string dbPassword;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            CreatePropertiesFile();

            LoadProperties();

            LoadConnectionStringBuilder();

            optionsBuilder.UseLazyLoadingProxies()
                .UseMySQL(Builder.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DogBreed>()
                .HasMany(b => b.Childs);
        }

        private void LoadConnectionStringBuilder()
        {
            Builder = new MySqlConnectionStringBuilder
            {
                Server = dbAddress,
                Database = dbName,
                UserID = dbUser,
                Password = dbPassword,
                PersistSecurityInfo = true,
                CharacterSet = "utf8"
            };
        }

        private void CreatePropertiesFile()
        {
            Properties = new Properties(Environment.CurrentDirectory + $"/{DatabaseConstants.PropertiesFileName}");

            Dictionary<string, string> defaults = new Dictionary<string, string>()
            {
                { "address", DatabaseConstants.DefaultAddress },
                { "name", DatabaseConstants.DefaultDatabaseName },
                { "user", DatabaseConstants.DefaultUser },
                { "password", DatabaseConstants.DefaultPassword }
            };

            Properties.SetDefaults(defaults, true);
        }

        private void LoadProperties()
        {
            dbAddress = Properties.GetValue("address");
            dbName = Properties.GetValue("name");
            dbUser = Properties.GetValue("user");
            dbPassword = Properties.GetValue("password");
        }
    }
}
