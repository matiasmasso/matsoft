Public Class Frm_DocFiles

    Private Sub Frm_DocFiles_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadYears()
        refresca()
    End Sub

    Private Async Sub refresca()
        ProgressBar1.Visible = True
        ProgressBar1.Style = ProgressBarStyle.Marquee
        Dim exs As New List(Of Exception)
        Dim oDocFiles = Await FEB.Docfiles.All(Xl_Years1.Value, exs)
        If exs.Count = 0 Then
            Xl_DocFiles1.Load(oDocFiles)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_DocFiles1.filter = e.Argument
    End Sub

    Private Sub Xl_DocFiles1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocFiles1.RequestToAddNew
        Dim oDocfile As New DTODocFile
        Dim oFrm As New Frm_DocFile(oDocfile)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub LoadYears()
        Dim iYears As New List(Of Integer)
        For i As Integer = DTO.GlobalVariables.Today().Year To 1985 Step -1
            iYears.Add(i)
        Next
        Xl_Years1.Load(iYears)
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        refresca()
    End Sub

    Private Sub Xl_DocFiles1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DocFiles1.RequestToRefresh
        refresca()
    End Sub
End Class