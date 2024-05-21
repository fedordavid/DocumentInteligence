using DocumentInteligence.Model;
using Microsoft.EntityFrameworkCore;

namespace DocumentInteligence.DbContext;

public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}