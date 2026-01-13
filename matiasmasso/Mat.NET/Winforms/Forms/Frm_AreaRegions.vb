Public Class Frm_AreaRegions
    Private _Country As DTOCountry
    Private _DefaultValue As DTOAreaRegio
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oCountry As DTOCountry, Optional oDefaultValue As DTOAreaRegio = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _Country = oCountry
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_AreaRegions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_AreaRegions1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_AreaRegions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToAddNew
        Dim oAreaRegio As New DTOAreaRegio
        Dim oFrm As New Frm_AreaRegio(oAreaRegio)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_AreaRegions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AreaRegions1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oAreaRegions = Await FEB2.AreaRegions.All(_Country, exs)
        If exs.Count = 0 Then
            Xl_AreaRegions1.Load(oAreaRegions, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class