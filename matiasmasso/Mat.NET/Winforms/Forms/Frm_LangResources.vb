Imports System.Resources
Imports FEB2

Public Class Frm_LangResources
    Private Async Sub Frm_LangResources_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Me.Text = Current.Session.Lang.resource("Translations")
        Dim items = Await FEB2.LangTexts.MissingTranslations(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_LangResources1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LangResources1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oLangText = e.Argument
        If Await FEB2.LangText.Update(exs, oLangText) Then
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ShowAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAllToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB2.LangTexts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub HideCompletedsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideCompletedsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB2.LangTexts.MissingTranslations(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class