using System;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class DAL : DbContext
	{
        //public DbSet<UserEntity> Users { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<UserEntity>()
        //        .HasKey(x => x.Id);

        //    builder.Entity<UserEntity>()
        //        .Property(x => x.FirstName)
        //        .HasMaxLength(DbPropertyLengths.NameLength);

        //    builder.Entity<UserEntity>()
        //        .Property(x => x.LastName)
        //        .HasMaxLength(DbPropertyLengths.NameLength);
        //}
    }
}

