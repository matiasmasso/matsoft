Public Class Frm_Leads
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private _Leads As List(Of DTOUser)
    Private _Mode As DTO.Defaults.SelectionModes

    Public Sub New(Optional Leads As List(Of DTOUser) = Nothing, Optional Mode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Leads = Leads
        _Mode = Mode
    End Sub

    Private Async Sub Frm_Leads_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Leads Is Nothing Then
            _Leads = Await FEB2.Users.All(exs, Current.Session.Emp)
            If exs.Count = 0 Then
            Else
                UIHelper.WarnError(exs)
                Exit Sub
            End If
        End If
        Xl_Leads1.Load(_Leads, Nothing, _Mode)
    End Sub

    Private Sub Xl_Leads1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Leads1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Leads1.Filter = e.Argument
    End Sub


End Class