using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        private Stream docStream;
        public Stream DocumentStream
        {
            get { return docStream; }
            set { docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }
        public ViewModel()
        {
            docStream = new FileStream(@"C:\Users\Damian\Downloads\schadeformulier-rv-naarden.pdf",FileMode.OpenOrCreate);
        }
    }
}
