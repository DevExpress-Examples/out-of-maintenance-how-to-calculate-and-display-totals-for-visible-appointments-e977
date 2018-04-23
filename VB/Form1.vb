Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.Drawing
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.Native

Namespace CalculateTotals
	Partial Public Class Form1
		Inherits XtraForm
		Public Sub New()
			InitializeComponent()

		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			schedulerControl1.ActiveViewType = SchedulerViewType.Day
			schedulerControl1.DayView.DayCount = 1
			schedulerControl1.DayView.ResourcesPerPage = 3
			schedulerControl1.WorkWeekView.ResourcesPerPage = 3
			schedulerControl1.WeekView.ResourcesPerPage = 3
			schedulerControl1.MonthView.ResourcesPerPage = 3
			schedulerControl1.TimelineView.ResourcesPerPage = 3


			schedulerControl1.GroupType = SchedulerGroupType.Resource
			schedulerControl1.OptionsView.ResourceHeaders.ImageSize = New Size(100, 50)
			schedulerControl1.OptionsView.ResourceHeaders.ImageSizeMode = HeaderImageSizeMode.ZoomImage

			schedulerControl1.Start = New DateTime(2008,07,14)

			' TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
			Me.carsTableAdapter.Fill(Me.carsDBDataSet.Cars)
			' TODO: This line of code loads data into the 'carsDBDataSet.CarScheduling' table. You can move, or remove it, as needed.
			Me.carSchedulingTableAdapter.Fill(Me.carsDBDataSet.CarScheduling)

		End Sub


		Private Sub schedulerControl1_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As DevExpress.XtraScheduler.AppointmentFormEventArgs) Handles schedulerControl1.EditAppointmentFormShowing
			Dim apt As Appointment = e.Appointment
			Dim openRecurrenceForm As Boolean = apt.IsRecurring AndAlso schedulerStorage1.Appointments.IsNewAppointment(apt)

			Dim f As New MyAppointmentEditForm(CType(sender, SchedulerControl), apt, openRecurrenceForm)
			f.LookAndFeel.ParentLookAndFeel = Me.LookAndFeel.ParentLookAndFeel
			e.DialogResult = f.ShowDialog()
			e.Handled = True

			schedulerControl1.Refresh()
		End Sub

		Private Sub schedulerStorage1_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsChanged, schedulerStorage1.AppointmentsInserted, schedulerStorage1.AppointmentsDeleted
			carSchedulingTableAdapter.Update(Me.carsDBDataSet)
			Me.carsDBDataSet.AcceptChanges()

		End Sub

		Private Sub schedulerControl1_CustomDrawResourceHeader(ByVal sender As Object, ByVal e As CustomDrawObjectEventArgs) Handles schedulerControl1.CustomDrawResourceHeader

			Dim rh As ResourceHeader = CType(e.ObjectInfo, ResourceHeader)
			Dim sf As StringFormat = rh.Appearance.HeaderCaption.TextOptions.GetStringFormat()

			Dim newcaption As String = rh.Resource.Caption + Constants.vbLf & " Total:" & CalcCurrentTotals(rh.Interval, rh.Resource).ToString("C")

			If schedulerControl1.ActiveViewType = SchedulerViewType.Timeline Then
				e.Cache.DrawVString(newcaption, rh.Appearance.HeaderCaption.Font, Brushes.Black, e.Bounds, sf, 270)
			Else
				e.Cache.DrawString(newcaption, rh.Appearance.HeaderCaption.Font, Brushes.Black, e.Bounds, sf)
			End If


			e.Handled = True
		End Sub

		Private Function CalcCurrentTotals(ByVal interval As TimeInterval, ByVal resource As Resource) As Single
			Dim apts As AppointmentBaseCollection = Me.schedulerStorage1.GetAppointments(interval)
			Dim total As Single = 0.0F

			Dim resources As New ResourceBaseCollection()
			resources.Add(resource)
			Dim filter As New ResourcesAppointmentsFilter(resources)
			filter.Process(apts)
			For Each apt As Appointment In CType(filter.DestinationCollection, AppointmentBaseCollection)
				If apt.CustomFields("CustomPrice") IsNot Nothing Then
					total = total + Single.Parse(apt.CustomFields("CustomPrice").ToString())
				End If
			Next apt

			Return total
		End Function



	End Class
End Namespace