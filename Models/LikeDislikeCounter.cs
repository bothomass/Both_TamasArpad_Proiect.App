using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;

namespace Both_TamasArpad_Proiect.Models
{
    public class LikeDislikeCounter : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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

        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public List<Figurine> Figurines { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
