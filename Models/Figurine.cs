using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;
using Both_TamasArpad_Proiect.Data;
using System.Diagnostics;
using System.Xml.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Both_TamasArpad_Proiect.Models
{
    public class Figurine : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50), Unique]
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhotoLocation { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        private int _likeCounter;
        public int LikeCounter
        {
            get { return _likeCounter; }
            set
            {
                if (_likeCounter != value)
                {
                    _likeCounter = value;
                    OnPropertyChanged(nameof(LikeCounter));
                }
            }
        }

        private int _dislikeCounter;
        public int DislikeCounter
        {
            get { return _dislikeCounter; }
            set
            {
                if (_dislikeCounter != value)
                {
                    _dislikeCounter = value;
                    OnPropertyChanged(nameof(DislikeCounter));
                }
            }
        }

        // New property for referencing Creator
        [ForeignKey(typeof(Creator))]
        public int CreatorId { get; set; }

        // Reference to the associated Creator
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Creator Creator { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
