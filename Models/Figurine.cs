using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Both_TamasArpad_Proiect.Data;
using System.Diagnostics;
using System.Xml.Linq;
using System.ComponentModel;

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
        //public int LikeCounter { get; set; } = 0;
        //public int DislikeCounter { get; set; } = 0;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
