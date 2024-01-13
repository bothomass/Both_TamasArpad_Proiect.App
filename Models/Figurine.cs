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
using System.ComponentModel.DataAnnotations;

namespace Both_TamasArpad_Proiect.Models
{
    public class Figurine : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [SQLite.MaxLength(50), Unique, Required(ErrorMessage = "Creation name is required")]
        public string Title { get; set; }
        [SQLite.MaxLength(50), Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string PhotoLocation { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [ForeignKey(typeof(LikeDislikeCounter))]
        public int LikeDislikeCounterId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public LikeDislikeCounter LikeDislikeCounter { get; set; }

        [ForeignKey(typeof(Creator))]
        public int CreatorId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public Creator Creator { get; set; }

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
