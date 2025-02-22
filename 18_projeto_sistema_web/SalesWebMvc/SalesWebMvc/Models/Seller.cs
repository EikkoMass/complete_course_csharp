using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models;

public class Seller
{
    public int Id { get; set; }
    
    [Required(ErrorMessage="{0} required")]
    [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1} characters")]
    public string Name { get; set; }
    
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Enter a valid email")]
    [Required(ErrorMessage="{0} required")]
    public string Email { get; set; }
    
    [Display(Name = "Birth Date")]
    [Required(ErrorMessage="{0} required")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }
    
    [Display(Name = "Base Salary")]
    [Required(ErrorMessage="{0} required")]
    [Range(100.0, 50000.0, ErrorMessage="{0} must be from {1} to {2}")]
    [DisplayFormat(DataFormatString = "{0:F2}")]
    public double BaseSalary { get; set; }
    public Department Department { get; set; }
    public int DepartmentId { get; set; }
    public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

    public Seller()
    {
    }

    public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
    {
        Id = id;
        Name = name;
        Email = email;
        BirthDate = birthDate;
        BaseSalary = baseSalary;
        Department = department;
    }

    public void AddSales(SalesRecord sr)
    {
        Sales.Add(sr);
    }

    public void RemoveSales(SalesRecord sr)
    {
        Sales.Remove(sr);
    }

    public double TotalSales(DateTime initial, DateTime final)
    {
        return Sales
            .Where(sr => sr.Date >= initial && sr.Date <= final)
            .Sum(sr => sr.Amount);
    }
}