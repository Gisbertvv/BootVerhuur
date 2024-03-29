﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootVerhuurWpf.Model
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
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }
        public ViewModel()
        {
            //this is the filepath that is used to open the chosen damage form pdf
            //docStream = new FileStream(@"C:\Users\Damian\Downloads\schadeformulier-rv-naarden-1.pdf", FileMode.OpenOrCreate);
            //docStream = new FileStream(@"C:\Users\gisbe\Downloads\schadeformulier-rv-naarden.pdf", FileMode.OpenOrCreate);
            docStream = new FileStream(@"C:\Users\gisbe\source\repos\BootVerhuur\BootVerhuurWpf\PDF\pdf.pdf", FileMode.OpenOrCreate);



        }
    }
}
