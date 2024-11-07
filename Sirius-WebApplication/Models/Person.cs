using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;


namespace Sirius_WebApplication.Models
{



    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Имя пользователя"), Required]
        public string FName { get; set; }
        [Display(Name = "Фамилия пользователя"), Required]
        public string LName { get; set; }



        [NotMapped]
        public virtual string[] NotNullProps => new string[] { "LName", "FName" };

        /// <summary>
        /// Проверка на наличие значений важных атрибутов
        /// </summary>
        /// <returns></returns>
        public bool Valid()
        {
            bool valid = true;
            foreach (var prop in NotNullProps)
            {
                var pi = this.GetType().GetProperty(prop);
                if (pi != null)
                {
                    var value = pi.GetValue(this, null);
                    valid &= !pi.PropertyType.IsDefaultValue(value) && value.ToString() != "";
                }
            }
            return valid;
        }

        /// <summary>
        /// Обновление значений атрибутов
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public IEnumerable<string> Update(Person person)
        {
            List<string> updated = new List<string>();
            foreach (var pi in person.GetType().GetProperties().Where(i => i.CanWrite))
            {
                var v1 = pi.GetValue(this, null);
                var v2 = pi.GetValue(person, null);
                if (!v1.Equals(v2))
                {
                    pi.SetValue(this, v2, null);
                    updated.Add(pi.Name);
                }
            }
            return updated.ToArray();
        }
    }


    public class Employeer : Person
    {

    }
}
