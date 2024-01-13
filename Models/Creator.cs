using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Both_TamasArpad_Proiect.Models
{
    public class Creator : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [SQLite.MaxLength(50), Required(ErrorMessage = "First name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed in the first name")]
        public string FirstName { get; set; }

        [SQLite.MaxLength(50), Required(ErrorMessage = "Last name is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed in the last name")]
        public string LastName { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Figurine> Figurines { get; set; }

        [Ignore]
        public string FullName => $"{FirstName} {LastName}";

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
