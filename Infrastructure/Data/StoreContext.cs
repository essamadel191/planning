using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    // To fix the problem in the migration
    /// <summary>
    ///     That Required to have more property in the price
    ///     And The warning is : No store type was specified 
    ///     for the decimal property 'Price' on entity type 'Product'.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //********* So Any New Configration will be applied based on the product configration *************//
        // As long they're created in the same assembly which going to be our infrastructure assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
