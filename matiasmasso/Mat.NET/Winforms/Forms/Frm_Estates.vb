Public Class Frm_Estates
    Private _DefaultValue As DTOEstate
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOEstate = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        Me.InitializeComponent()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
    End Sub

    Private Async Sub Frm_Estates_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_Estates1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Estates1.OnItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Estates1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Estates1.RequestToAddNew
        Dim oEstate As New DTOEstate
        Dim oFrm As New Frm_Estate(oEstate)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Estates1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Estates1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oEstates = Await FEB2.Estates.All(exs)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            Xl_Estates1.Load(oEstates, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function
End Class