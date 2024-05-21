using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentInteligence.Model;

public class Invoice
{
    public int Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? InvoiceLocation { get; set; }
    public string? InvoiceName { get; set; }
    
    public int SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}