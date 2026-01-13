Public Class Frm_MarketPlaces
    Private _DefaultValue As DTOMarketPlace
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOMarketPlace = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_MarketPlaces_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_MarketPlaces1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_MarketPlaces1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub AddNewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewToolStripMenuItem.Click
        Do_AddNew()
    End Sub

    Private Sub Xl_MarketPlaces1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_MarketPlaces1.RequestToAddNew
        Do_AddNew()
    End Sub

    Private Async Sub Xl_MarketPlaces1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_MarketPlaces1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oMarketPlaces = Await FEB2.MarketPlaces.All(exs, GlobalVariables.Emp)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_MarketPlaces1.Load(oMarketPlaces, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Do_AddNew()
        Dim oMarketPlace As New DTOMarketPlace
        Dim oFrm As New Frm_MarketPlace(oMarketPlace)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub
End Class