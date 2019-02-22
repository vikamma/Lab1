using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;



namespace Matsiuk01
{
    class Test : INotifyPropertyChanged

    
    {
        public DateTime _date = DateTime.Today;
        public int _age;
        public string westZodiac;
        public string chinaZodiac;

        private RelayCommand<object> fieldGoroscope;

        public string Age => $"Age: {_age}";
        public string Zodiac1 => $"Western horoscope: {westZodiac}";
        public string Zodiac2 => $"Chinees horoscope: {chinaZodiac}";


        public int GetAge(DateTime _date)
        {
            DateTime currentDate = DateTime.Parse(DateTime.Now.Date.ToShortDateString());
            _age = currentDate.Year - _date.Year;
            if (currentDate.Month < _date.Month)
            {
                return --_age;
            }
            else if ((currentDate.Month >= _date.Month)
                     && (currentDate.Day < _date.Day))
            {
                return --_age;
            }

            return _age;

        }

        public string GetWesternHoroscope(DateTime _date)
        {
            switch (_date.Month)
            {
                case 1:
                    return _date.Day <= 19 ? "Capricorn" : "Aquarius";                 
                case 2:
                    return _date.Day <= 18 ? "Aquarius" : "Pisces";
                    
                case 3:
                    return _date.Day <= 20 ? "Pisces" : "Aries";
                    
                case 4:
                    return _date.Day <= 19 ? "Aries" : "Taurus";
                    
                case 5:
                    return _date.Day <= 20 ? "Taurus" : "Gemini";
                    
                case 6:
                    return _date.Day <= 20 ? "Gemini" : "Cancer";
                    
                case 7:
                    return _date.Day <= 22 ? "Cancer" : "Leo";
                    
                case 8:
                    return _date.Day <= 22 ? "Leo" : "Virgo";
                    
                case 9:
                    return _date.Day <= 22 ? "Virgo" : "Libra";
                    
                case 10:
                    return _date.Day <= 22 ? "Libra" : "Scorpio";
                    
                case 11:
                    return _date.Day <= 21 ? "Scorpio" : "Sagittarius";
                    
                case 12:
                    return _date.Day <= 21 ? "Sagittarius" : "Capricorn";
                
            }

            return "";
        }

        public string GetChinaHoroscope(DateTime _date)
        {
            switch ((_date.Year - 4) % 12)
            {
                case 0:
                    return "Rat";
                case 1:
                    return "Ox";
                case 2:
                    return "Tiger";
                case 3:
                    return "Rabbit";
                case 4:
                    return "Dragon";
                case 5:
                    return "Snake";
                case 6:
                    return "Horse";
                case 7:
                    return "Goat";
                case 8:
                    return "Monkey";
                case 9:
                    return "Rooster";
                case 10:
                    return "Dog";
                case 11:
                    return "Pig";

            }

            return "";
        }

        public RelayCommand<object> ShowGoroscope
        {
            get
            {
                return fieldGoroscope ?? (fieldGoroscope = new RelayCommand<object>(
                          AsCommand, o => CanExecuteCommand()));
            }
        }
        private async void AsCommand(object obj)
        {
            await Task.Run(() =>
                {
                    _age = GetAge(_date);
                    westZodiac = GetWesternHoroscope(_date);
                    chinaZodiac = GetChinaHoroscope(_date);
                    OnPropertyChanged(nameof(Zodiac1));
                    OnPropertyChanged(nameof(Zodiac2));
                }
            );
        }
        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_date.ToString());
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;


                if (_date.CompareTo(DateTime.Today) > 0 )
                {
                    MessageBox.Show("You aren`t born yet!");
                }
                if ( GetAge(_date) > 135)
                {
                    MessageBox.Show("You are too old!");
                }

                if (_date.Day == DateTime.Today.Day && _date.Month == DateTime.Today.Month)
                    MessageBox.Show("Happy Birthday!!!");



                _age =GetAge(_date);
              
                  
                OnPropertyChanged(nameof(Age));
                OnPropertyChanged(nameof(Zodiac1));
                OnPropertyChanged(nameof(Zodiac2));
               // OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
