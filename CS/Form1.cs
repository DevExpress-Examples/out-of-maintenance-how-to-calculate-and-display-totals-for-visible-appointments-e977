using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Native;

namespace CalculateTotals {
    public partial class Form1 : XtraForm {
        public Form1() {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e) {
            schedulerControl1.ActiveViewType = SchedulerViewType.Day;
            schedulerControl1.DayView.DayCount = 1;
            schedulerControl1.DayView.ResourcesPerPage = 3;
            schedulerControl1.WorkWeekView.ResourcesPerPage = 3;
            schedulerControl1.WeekView.ResourcesPerPage = 3;
            schedulerControl1.MonthView.ResourcesPerPage = 3;
            schedulerControl1.TimelineView.ResourcesPerPage = 3;


            schedulerControl1.GroupType = SchedulerGroupType.Resource;
            schedulerControl1.OptionsView.ResourceHeaders.ImageSize = new Size(100, 50);
            schedulerControl1.OptionsView.ResourceHeaders.ImageSizeMode = HeaderImageSizeMode.ZoomImage;

            schedulerControl1.Start = new DateTime(2008,07,14);
            
            // TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.carsDBDataSet.Cars);
            // TODO: This line of code loads data into the 'carsDBDataSet.CarScheduling' table. You can move, or remove it, as needed.
            this.carSchedulingTableAdapter.Fill(this.carsDBDataSet.CarScheduling);

        }


        private void schedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e) {
            Appointment apt = e.Appointment;
            bool openRecurrenceForm = apt.IsRecurring && schedulerStorage1.Appointments.IsNewAppointment(apt);

            MyAppointmentEditForm f = new MyAppointmentEditForm((SchedulerControl)sender, apt, openRecurrenceForm);
            f.LookAndFeel.ParentLookAndFeel = this.LookAndFeel.ParentLookAndFeel;
            e.DialogResult = f.ShowDialog();
            e.Handled = true;

            schedulerControl1.Refresh();
        }

        private void schedulerStorage1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
            carSchedulingTableAdapter.Update(this.carsDBDataSet);
            this.carsDBDataSet.AcceptChanges();

        }

        private void schedulerControl1_CustomDrawResourceHeader(object sender, CustomDrawObjectEventArgs e) {
            
            ResourceHeader rh = (ResourceHeader)e.ObjectInfo;
            StringFormat sf = rh.Appearance.HeaderCaption.TextOptions.GetStringFormat();
            
            string newcaption = rh.Resource.Caption + "\n Total:" + CalcCurrentTotals(rh.Interval, rh.Resource).ToString("C");

            if (schedulerControl1.ActiveViewType == SchedulerViewType.Timeline) {
                e.Cache.DrawVString(newcaption, rh.Appearance.HeaderCaption.Font, Brushes.Black, e.Bounds, sf, 270);
            }
            else {
                e.Cache.DrawString(newcaption, rh.Appearance.HeaderCaption.Font, Brushes.Black, e.Bounds, sf);
            }
            
            
            e.Handled = true;
        }

        private float CalcCurrentTotals(TimeInterval interval, Resource resource) {
            AppointmentBaseCollection apts = this.schedulerStorage1.GetAppointments(interval);
            float total = 0.0F;

            ResourceBaseCollection resources = new ResourceBaseCollection();
            resources.Add(resource);
            ResourcesAppointmentsFilter filter = new ResourcesAppointmentsFilter(resources);
            filter.Process(apts);
            foreach (Appointment apt in (AppointmentBaseCollection)filter.DestinationCollection) {
                if (apt.CustomFields["CustomPrice"] != null) {
                    total = total + float.Parse(apt.CustomFields["CustomPrice"].ToString());
                }
            }
            
            return total;
        }


        
    }
}