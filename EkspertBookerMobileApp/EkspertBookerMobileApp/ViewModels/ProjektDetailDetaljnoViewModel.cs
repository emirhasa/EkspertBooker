﻿using EkspertBooker.Model;
using EkspertBooker.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EkspertBookerMobileApp.ViewModels
{
    public class ProjektDetailDetaljnoViewModel : BaseViewModel
    {
        int _projektId;
        Projekt _projekt;

        public Projekt Projekt
        {
            get { return _projekt; }
            set { SetProperty(ref _projekt, value); }
        }

        ProjektDetalji _projektDetalji;
        public ProjektDetalji ProjektDetalji
        {
            get { return _projektDetalji; }
            set { SetProperty(ref _projektDetalji, value); }
        }

        PrilogMetaDataDTO _projektDetaljiPrilog;
        public PrilogMetaDataDTO ProjektDetaljiPrilog
        {
            get { return _projektDetaljiPrilog; }
            set { SetProperty(ref _projektDetaljiPrilog, value); }
        }

        string _prilogNaziv;
        public string PrilogNaziv
        {
            get { return _prilogNaziv; }
            set { SetProperty(ref _prilogNaziv, value); }
        }

        bool _prilogPostoji;
        public bool PrilogPostoji
        {
            get { return _prilogPostoji; }
            set { SetProperty(ref _prilogPostoji, value); }
        }


        private readonly APIService _projektiService = new APIService("Projekti");
        private readonly APIService _projektDetaljiService = new APIService("ProjektDetalji");
        private readonly APIService _projektDetaljiPriloziService = new APIService("ProjektDetaljiPrilozi");

        public ProjektDetailDetaljnoViewModel(int projektId)
        {
            _projektId = projektId;
            InitCommand = new Command(async () => await Init());
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            Projekt = await _projektiService.GetById<Projekt>(_projektId);
            var projekt_detalji = await _projektDetaljiService.Get<List<ProjektDetalji>>(new ProjektDetaljiSearchRequest
            {
                ProjektId = _projektId
            });
            ProjektDetalji = projekt_detalji[0];
            AktivanDetaljanOpis = ProjektDetalji.AktivanDetaljanOpis;
            Napomena = ProjektDetalji.Napomena;
            var prilog_metadata = await _projektDetaljiPriloziService.Get<PrilogMetaDataDTO>(new ProjektDetaljiPrilogSearchRequest
            {
                ProjektDetaljiId = ProjektDetalji.ProjektDetaljiId,
                GetOnlyMetaData = true
            });
            if (prilog_metadata == null)
            {
                PrilogNaziv = "Prilog još uvijek nije uploadovan!";
                PrilogPostoji = false;
            } else
            {
                PrilogNaziv = prilog_metadata.PrilogNaziv;
                PrilogPostoji = true;
            }

        }

        string _aktivanDetaljanOpis;
        public string AktivanDetaljanOpis
        {
            get { return _aktivanDetaljanOpis; }
            set { SetProperty(ref _aktivanDetaljanOpis, value); }
        }

        string _napomena;
        public string Napomena
        {
            get { return _napomena; }
            set { SetProperty(ref _napomena, value); }
        }

        public bool ValidationTriggered { get; set; } = false;

        bool _urediFormErrorVisible = false;
        public bool UrediFormErrorVisible
        {
            get { return _urediFormErrorVisible; }
            set { SetProperty(ref _urediFormErrorVisible, value); }
        }

        public async Task<bool> SacuvajPromjene()
        {
            if (IsValid())
            {
                var request = new ProjektDetaljiUpsertRequest
                {
                    ProjektId = _projektId,
                    AktivanDetaljanOpis = ProjektDetalji.AktivanDetaljanOpis,
                    Napomena = ProjektDetalji.Napomena
                };
                if (Napomena != ProjektDetalji.Napomena) request.Napomena = Napomena;
                if (AktivanDetaljanOpis != ProjektDetalji.AktivanDetaljanOpis) request.AktivanDetaljanOpis = AktivanDetaljanOpis;
                var result = await _projektDetaljiService.Update<ProjektDetalji>(ProjektDetalji.ProjektDetaljiId, request);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }

        public bool IsValid()
        {
            ValidationTriggered = true;
            if (!string.IsNullOrWhiteSpace(AktivanDetaljanOpis) || !string.IsNullOrWhiteSpace(Napomena))
            {
                UrediFormErrorVisible = false;
                return true;
            }
            else
            {
                UrediFormErrorVisible = true;
                return false;
            }
        }

    }
}