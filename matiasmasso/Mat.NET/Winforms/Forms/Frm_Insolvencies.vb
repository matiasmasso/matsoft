Public Class Frm_Insolvencies
    Private _DefaultValue As DTOInsolvencia
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOInsolvencia = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Insolvencies_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Insolvencies1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Insolvencies1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Insolvencies1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Insolvencies1.RequestToAddNew
        Dim oInsolvencia = DTOInsolvencia.Factory()
        Dim oFrm As New Frm_Insolvencia(oInsolvencia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Insolvencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Insolvencies1.RequestToRefresh
        Await refresca()
    End Sub
    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub
    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oInsolvencies = Await FEB2.Insolvencias.All(exs)
        If exs.Count = 0 Then
            Xl_Insolvencies1.Load(oInsolvencies, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class