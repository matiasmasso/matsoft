Public Class Frm_LangResources
    Private items As List(Of DTOLangText)

    Private Async Sub Frm_LangResources_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Me.Text = Current.Session.Lang.resource("Translations")
        items = Await FEB.LangTexts.MissingTranslations(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(FilteredItems())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_LangResources1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LangResources1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oLangText = Await FEB.LangText.Update(exs, e.Argument)
        If exs.Count = 0 Then
            Dim idx = items.IndexOf(items.FirstOrDefault(Function(x) x.Guid.Equals(oLangText.Guid) And x.Src = oLangText.Src))
            items(idx) = oLangText
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ShowAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAllToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        items = Await FEB.LangTexts.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(FilteredItems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub HideCompletedsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideCompletedsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        items = Await FEB.LangTexts.MissingTranslations(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_LangResources1.Load(FilteredItems())
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_LangResources1.Load(FilteredItems())
    End Sub

    Private Sub ShowOutdatedsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowOutdatedsToolStripMenuItem.Click
        Xl_LangResources1.Load(FilteredItems())
    End Sub

    Private Function FilteredItems() As List(Of DTOLangText)
        Dim searchterm As String = Xl_TextboxSearch1.Value
        Dim retval = items
        If Not String.IsNullOrEmpty(searchterm) Then
            retval = items.Where(Function(x) x.Contains(searchterm)).ToList()
        End If
        If (ShowOutdatedsToolStripMenuItem.Checked) Then
            retval = retval.Where(Function(x) x.IsOutdated()).ToList()
        End If
        Return retval
    End Function

End Class