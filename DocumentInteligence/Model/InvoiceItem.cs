namespace DocumentInteligence.Model;

public class InvoiceItem
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public decimal TaxBase { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal Total { get; set; }
}