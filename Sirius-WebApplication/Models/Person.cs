using System.ComponentModel.DataAnnotations;


namespace Sirius_WebApplication.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }
}
