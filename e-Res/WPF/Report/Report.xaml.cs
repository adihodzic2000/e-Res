using Common.Dto.Reservations;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Dtos;

namespace WPF.Report
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public ReservationGetDto Reservation { get; set; }
        public List<ReportData> Data { get; set; }
        public Report(ReservationGetDto reservation, List<ReportData> data)
        {
            InitializeComponent();
            Reservation = reservation;
            Data = data;
            Icon = BitmapSource.Create(1, 1, 0, 0, PixelFormats.Bgra32, null, new byte[4], 4);
            //IconHelper.RemoveIcon(this);
            ReportView.ProcessingMode = ProcessingMode.Local;
            ReportView.LocalReport.EnableExternalImages = true;
            ReportView.ShowToolBar = true;
            ReportView.BorderStyle = BorderStyle.None;
            ReportView.LocalReport.DataSources.Clear();
            ReportView.ShowBackButton = false;
            ReportView.ShowContextMenu = false;
            ReportView.ShowZoomControl = false;
            ReportView.ShowStopButton = false;
            ReportView.ShowPageNavigationControls = false;
            ReportView.ShowDocumentMapButton = false;
            ReportView.ShowRefreshButton = false;
            ReportView.ShowCredentialPrompts = false;
            ReportView.ShowFindControls = false;
            ReportView.ShowParameterPrompts = false;
            ReportView.ShowProgress = false;
            ReportView.ShowPromptAreaButton = false;

            Parameters parameter = new Parameters
            {
              FullName =APIService.FullName,
              Date=DateTime.Now.ToString("dd.MM.yyyy"),
              Time=DateTime.Now.ToString("HH:mm:ss"),
              LogoPath=APIService.LogoPath
            };
            List<Parameters> parameters = new List<Parameters>();
            parameters.Add(parameter);
            ReportDataSource _data = new ReportDataSource("Data", data);
            ReportDataSource _parameters = new ReportDataSource("Parameters", parameters);

            ReportView.LocalReport.DataSources.Add(_data);
            ReportView.LocalReport.DataSources.Add(_parameters);


            ReportView.LocalReport.ReportEmbeddedResource = @"WPF.Report.Bills.rdlc";
            ReportView.RefreshReport();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
