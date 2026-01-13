
Public Class Frm_IncidenciesCods
    Private _Cod As DTOIncidenciaCod.cods
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oCod As DTOIncidenciaCod.cods, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Cod = oCod
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_IncidenciaCods_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case _Cod
            Case DTOIncidenciaCod.cods.averia
                Me.Text = "Codis apertura incidencia"
            Case DTOIncidenciaCod.cods.tancament
                Me.Text = "Codis tancament incidencia"
        End Select
        Await refresca()
    End Sub

    Private Sub Xl_IncidenciesCods1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_IncidenciesCods1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_IncidenciesCods1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_IncidenciesCods1.RequestToAddNew
        Dim item As New DTOIncidenciaCod
        item.Cod = _Cod
        Dim oFrm As New Frm_IncidenciaCod(item)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_IncidenciesCods1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_IncidenciesCods1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oIncidenciaCods = Await FEB2.IncidenciaCods.All(_Cod, exs)
        If exs.Count = 0 Then
            Xl_IncidenciesCods1.Load(oIncidenciaCods, Nothing, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class