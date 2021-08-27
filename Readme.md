<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128633953/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E977)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
* [MyAppointmentEditForm.cs](./CS/Forms/MyAppointmentEditForm.cs) (VB: [MyAppointmentEditForm.vb](./VB/Forms/MyAppointmentEditForm.vb))
<!-- default file list end -->
# How to calculate and display totals for visible appointments


<p>Problem: <br />
I have to calculate totals for visible appointments using values contained within custom fields and display the result within the Scheduler. How can I accomplish this?</p><p>Solution:<br />
You can display the result within the resource headers. To accomplish this, handle the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerControl_CustomDrawResourceHeadertopic">CustomDrawResourceHeader</a> event. Then you get the collection of appointments that fall within visible interval using the <a href="http://documentation.devexpress.com/#WindowsForms/DevExpressXtraSchedulerSchedulerStorageBase_GetAppointmentstopic">GetAppointments</a> method and perform the necessary calculations with custom fields of the appointments in a collection. Display the result by drawing the text via the <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressUtilsAppearanceObject_DrawStringtopic">DrawString</a> or <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressUtilsAppearanceObject_DrawVStringtopic">DrawVString</a> methods.</p><p>This example calculates the sum of "CustomPrice" fields for visible appointments for each resource and shows the result in resource headers. End-users should have the ability to edit an appointment's custom field, so a custom editing form is invoked instead of the default one.</p>

<br/>


