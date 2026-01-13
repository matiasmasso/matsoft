Public Class Frm_LangTexts
    Private _Lang As DTOLang
    Private Async Sub Frm_LangTexts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = "Texts pendents de traduir"
        _Lang = DTOLang.POR
        Await refresca()
    End Sub

    Private Async Sub Xl_LangTexts1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_LangTexts1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oMissingWebMenuGroups = Await FEB2.LangTexts.MissingWebMenuGroups(_Lang, exs)
        Dim oMissingWebMenuItems = Await FEB2.LangTexts.MissingWebMenuItems(_Lang, exs)
        Dim oMissingWinMenuItems = Await FEB2.LangTexts.MissingWinMenuItems(Current.Session.Emp, _Lang, exs)
        Dim oMissingCategories = Await FEB2.LangTexts.MissingCategories(_Lang, exs)
        Dim oMissingSkus = Await FEB2.LangTexts.MissingSkus(_Lang, exs)

        Dim oLangTexts As New List(Of DTOLangText)
        oLangTexts.AddRange(oMissingWebMenuGroups)
        oLangTexts.AddRange(oMissingWebMenuItems)
        oLangTexts.AddRange(oMissingWinMenuItems)
        oLangTexts.AddRange(oMissingCategories)
        oLangTexts.AddRange(oMissingSkus)
        Xl_LangTexts1.Load(oLangTexts)
    End Function

    Private Sub AddRange(mainList As List(Of DTOLangText), itemsToAdd As List(Of DTOLangText))

    End Sub
End Class