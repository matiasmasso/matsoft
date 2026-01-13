Public Class Frm_CodisMercancia
    Private _DefaultValue As DTOCodiMercancia
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOCodiMercancia = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_CodiMercancias_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_CodisMercancia1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_CodisMercancia1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub RequestToAddNew(sender As Object, e As MatEventArgs) Handles _
        Xl_CodisMercancia1.RequestToAddNew,
         NouCodiToolStripMenuItem.Click

        Dim oCodiMercancia As New DTOCodiMercancia("")
        Dim oFrm As New Frm_CodiMercancia(oCodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub


    Private Async Sub Xl_CodisMercancia1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_CodisMercancia1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oCodiMercancias = Await FEB2.CodisMercancia.All(exs)
        If exs.Count = 0 Then
            Xl_CodisMercancia1.Load(oCodiMercancias, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_CodisMercancia1.Filter = e.Argument
    End Sub
End Class