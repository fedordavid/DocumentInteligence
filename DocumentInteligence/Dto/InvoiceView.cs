namespace DocumentInteligence.Dto;

public class InvoiceView
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string InvoiceNumber { get; set; }
}