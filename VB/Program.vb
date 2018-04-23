' Developer Express Code Central Example:
' Custom form, custom fields and custom actions on reminder alert
' 
' The problem: Here is what I want to do: 1) Create an appointment with custom
' fields and an alarm 2) Add occurrence for that appointment with alarm as well 3)
' Prevent the alarm from showing and insert my own code for all occurrences 4)
' Being able to access the custom fields data for all occurrences 5) Dismiss the
' handled recurrent appointment The solution: To accomplish the required tasks,
' perform the following actions: Run the attached project. Click the "Create
' appointment with reminder" button. See the appointment series created at a
' current time of the day. The alert will be fired in one minute. Before it
' happens, open the newly created today's appointment, change its "Price" value
' and save modifications. When the reminder is triggered, a new appointment is
' created with a Subject line informing of the changed value for the Price custom
' field.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E382


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace CalculateTotals
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
		<STAThread> _
		Shared Sub Main()
			Application.EnableVisualStyles()
			Application.SetCompatibleTextRenderingDefault(False)
			Application.Run(New Form1())
		End Sub
	End Class
End Namespace