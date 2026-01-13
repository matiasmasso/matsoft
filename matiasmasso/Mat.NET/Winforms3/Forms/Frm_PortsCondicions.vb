Public Class Frm_PortsCondicions
    Private _DefaultValue As DTOPortsCondicio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOPortsCondicio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_PortsCondicios_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PortsCondicions1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PortsCondicions1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_PortsCondicions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PortsCondicions1.RequestToAddNew
        Dim oPortsCondicio As New DTOPortsCondicio
        Dim oFrm As New Frm_PortsCondicio(oPortsCondicio)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_PortsCondicions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PortsCondicions1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oPortsCondicios = Await FEB.PortsCondicions.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_PortsCondicions1.Load(oPortsCondicios, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


End Class