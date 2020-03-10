﻿using EkspertBooker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class PoslodavacEkspertiPreporukeViewModel : BaseViewModel
    {
        public int _projektId;
        private readonly APIService _preporukeService = new APIService("PoslodavacPreporuke");
        public ObservableCollection<EkspertPreporuka> PreporukeList { get; set; } = new ObservableCollection<EkspertPreporuka>();
        public PoslodavacEkspertiPreporukeViewModel(int projektId)
        {
            _projektId = projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            PreporukeList.Clear();
            var list = await _preporukeService.GetById<List<EkspertPreporuka>>(_projektId);
            if(list.Count > 0)
            {
                foreach(var preporuka in list)
                {
                    PreporukeList.Add(preporuka);
                }
            }else
            {
                Application.Current.MainPage.DisplayAlert("Info", "Trenutno nismo mogli izračunati nijednu preporuku", "OK");
            }

        }
    }
}