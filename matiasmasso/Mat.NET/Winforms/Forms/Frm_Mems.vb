Public Class Frm_Mems
    Private _Contact As DTOContact

    Public Sub New(Optional oContact As DTOContact = Nothing)
        MyBase.New
        InitializeComponent()
        _Contact = oContact
    End Sub

    Private Async Sub Frm_Mems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadYears()
        Await refresca()
    End Sub

    Private Async Sub Xl_Mems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Mems1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oMems = Await FEB2.Mems.All(exs, oContact:=_Contact, year:=CurrentYear)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Mems1.Load(oMems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub LoadYears()
        Dim years As New List(Of Integer)
        For i = Today.Year To 1996 Step -1
            years.Add(i)
        Next
        Xl_Years1.Load(years)
    End Sub

    Private Function CurrentYear() As Integer
        Return Xl_Years1.Value
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Dim src As String = e.Argument
        Xl_Mems1.Filter = src
    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        Await refresca()
    End Sub
End Class