using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public class Nurse:BaseEntity{
    public string Name { get; set; }
    public string Locations {get;set;}
    public string About { get; set; }="";
    
    [NotMapped]
    public List<string> days { get; set; }=new List<string>();
    public string image { get; set; }
    public double rating { get; set; }=0;

    public List<ReserveNurse> ReserveNurses { get; set; }

}