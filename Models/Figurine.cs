using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Both_TamasArpad_Proiect.Data;
using System.Diagnostics;
using System.Xml.Linq;

namespace Both_TamasArpad_Proiect.Models
{
    public class Figurine
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50), Unique]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoLocation { get; set; }
        public int LikeCounter { get; set; } = 0;
        public int DislikeCounter { get; set; } = 0;
        public DateTime DateAdded { get; set; } = DateTime.Now;


        public Figurine Clone() => MemberwiseClone() as Figurine;

        public (bool IsValid, string? ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return (false, $"{nameof(Title)} is required.");
            }
            //else if (Price <= 0)
            //{
            //    return (false, $"{nameof(Price)} should be greater than 0.");
            //}
            return (true, null);
        }
    }
}
