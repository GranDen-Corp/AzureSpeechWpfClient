﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AzureSpeechWpfClient.Settings
{
    [Serializable]
    public class DisplaySettings : INotifyPropertyChanged
    {
        private ObservableCollection<string> urlHistory = new ObservableCollection<string>();
        private ObservableCollection<CustomActivityJsonDataEntry> customPayloadData = new ObservableCollection<CustomActivityJsonDataEntry>();

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> UrlHistory
        {
            get
            {
                return this.urlHistory;
            }

            /*
            set
            {
                if (this.urlHistory != null)
                {
                    this.urlHistory.CollectionChanged -= this.UrlHistory_CollectionChanged;
                }

                this.urlHistory = value;
                this.urlHistory.CollectionChanged += this.UrlHistory_CollectionChanged;
                this.OnPropertyChanged();
            }
            */
        }

        public ObservableCollection<CustomActivityJsonDataEntry> CustomPayloadData
        {
            get
            {
                return this.customPayloadData;
            }

            /*
            set
            {
                if (this.customPayloadData != null)
                {
                    this.customPayloadData.CollectionChanged -= this.CustomPayloadData_CollectionChanged;
                }

                this.customPayloadData = value;
                this.customPayloadData.CollectionChanged += this.CustomPayloadData_CollectionChanged;
                this.OnPropertyChanged();
            }
            */
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CustomPayloadData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UrlHistory_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged(nameof(this.UrlHistory));
        }
    }
}
