Imports System.Drawing.Printing

Public Class Frm_LabelPrinterNotFound

    Private Sub Frm_LabelPrinterNotFound_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Impresoras As String

        ' recorre las impresoras instaladas   
        For Each Impresoras In PrinterSettings.InstalledPrinters
            ListBoxPrinters.Items.Add(Impresoras.ToString)
        Next

        If ListBoxPrinters.SelectedIndex >= 0 Then ButtonOk.Enabled = True
    End Sub

    Private Sub ListBoxPrinters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxPrinters.SelectedIndexChanged
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        SaveSetting("MatSoft", "SpvCli", "LabelPrinter", ListBoxPrinters.Text)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

End Class