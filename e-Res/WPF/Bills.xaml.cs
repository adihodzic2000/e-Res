using Common.Dto.Bills;
using Common.Dto.ReservationServices;
using Common.Dtos.Services;
using Core.SearchObjects;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Forms;

using WPF.Dtos;

namespace WPF
{
    /// <summary>
    /// Interaction logic for Bills.xaml
    /// </summary>
    public partial class Bills : System.Windows.Controls.UserControl
    {
        public APIService APIService { get; set; } = new APIService();

        public Bills()
        {
            InitializeComponent();
            initialLoad();
        }
        private async void loadBills()
        {
            try
            {   
                SearchObject searchObject = new SearchObject
                {
                    Id = (Guid)APIService.CompanyId,
                    ByName = Name.Text,
                    DateFrom = (DateTime)(dateFrom.SelectedDate == null ? DateTime.MinValue : (DateTime)dateFrom.SelectedDate),
                    DateTo = (DateTime)(dateTo.SelectedDate == null ? DateTime.MaxValue : ((DateTime)dateTo.SelectedDate).AddDays(1).AddSeconds(-1))
                };
                var data = await APIService.Post<Core.Message>("Bills/get-bills-by-company-id", searchObject);
                var jsonResult = JsonConvert.DeserializeObject(data.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<BillsGetDto>>(jsonResult);
                List<BillsGetDtoWPF> billsGetDtoWPFs = new List<BillsGetDtoWPF>();
                foreach (var item in x)
                {
                    var billsGetDtoWPF = new BillsGetDtoWPF
                    {
                        Id = item.Id,
                        Name = item.Reservation.Guest.FirstName + " " + item.Reservation.Guest.LastName,
                        IsPaid = item.IsPaid ? "DA" : "NE",
                        NumberOfNights = (item.Reservation.DateTo - item.Reservation.DateFrom).TotalDays,
                        PriceOfServices = item.TotalAmountOfServices,
                        Price = item.Reservation.Room.Price,
                        TotalAmount = item.TotalAmount,
                        Reservation = item.Reservation
                    };
                    billsGetDtoWPFs.Add(billsGetDtoWPF);
                }
                gridData.ItemsSource = billsGetDtoWPFs;
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.MessageBox.Show(error.Result.Info);
            }
        }
        private void initialLoad()
        {
            loadBills();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Platili ovaj račun?",
             "Potvrda", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {

                Guid id = ((BillsGetDtoWPF)(((System.Windows.Controls.Button)sender).DataContext)).Id;
                BillsPayDto billsPayDto = new BillsPayDto { Id = id };
                try
                {
                    var data = await APIService.Post<Core.Message>("Bills/pay-bill", billsPayDto);

                    if (data.IsValid)
                    {
                        System.Windows.MessageBox.Show("Uspješno ste platili račun!");
                        loadBills();
                    }

                }
                catch (FlurlHttpException ex)
                {
                    var error = ex.GetResponseJsonAsync<Core.Message>();
                    System.Windows.MessageBox.Show(error.Result.Info);
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var reservation = ((BillsGetDtoWPF)(((System.Windows.Controls.Button)sender).DataContext)).Reservation;
            List<ReportData> data = new List<ReportData>();
            ReportData res = new ReportData
            {
                Name = "Noćenje",
                Price = reservation.Room.Price,
                Quantity = (int)(reservation.DateTo - reservation.DateFrom).TotalDays,
                TotalAmount = ((int)(reservation.DateTo - reservation.DateFrom).TotalDays) * reservation.Room.Price
            };
            data.Add(res);
            try
            {

                var data1 = await APIService.GetById("Reservation/get-reservation-services", reservation.Id);
                var jsonResult = JsonConvert.DeserializeObject(data1.Data.ToString()).ToString();
                var x = JsonConvert.DeserializeObject<List<ReservationServicesGetDto>>(jsonResult);


                foreach (var service in x)
                {
                    ReportData reportData = new ReportData
                    {
                        Name = service.Service.Title,
                        Price = service.Service.Price,
                        Quantity = service.Quantity,
                        TotalAmount = service.Quantity * service.Service.Price
                    };
                    data.Add(reportData);
                }
            }
            catch (FlurlHttpException ex)
            {
                var error = ex.GetResponseJsonAsync<Core.Message>();
                System.Windows.Forms.MessageBox.Show(error.Result.Info);
            }
            WPF.Report.Report frmReport = new WPF.Report.Report(reservation, data);
            frmReport.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            loadBills();
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadBills();
        }
    }
}
