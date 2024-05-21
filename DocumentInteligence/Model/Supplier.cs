using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentInteligence.Model;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Service { get; set; }
    public string CustomerNumber { get; set; }
    
    public ICollection<Invoice> Invoices { get; set; }
}