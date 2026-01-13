Public Class Frm_AeatModels
    Private _DefaultValue As DTOAeatModel
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOAeatModel = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_AeatModels_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_AeatModels1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_AeatModels1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_AeatModels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_AeatModels1.RequestToAddNew
        Dim oAeatModel As New DTOAeatModel
        Dim oFrm As New Frm_AeatModel(oAeatModel)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_AeatModels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_AeatModels1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        'TODO: Habilitar selecció de exercicis anteriors
        Dim exs As New List(Of Exception)
        Dim oAeatModels = Await FEB.AeatModels.All(exs, Current.Session.User)
        If exs.Count = 0 Then
            Xl_AeatModels1.Load(oAeatModels, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class