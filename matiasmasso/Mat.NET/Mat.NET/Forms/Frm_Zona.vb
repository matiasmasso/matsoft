Public Class Frm_Zona
    Private _Zona As DTOZona
    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        General
        Locations
        Reps
        Transportistes
    End Enum

    Public Sub New(value As DTOZona)
        MyBase.New()
        Me.InitializeComponent()
        _Zona = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _Zona
            TextBoxPaisNom.Text = BLL.BLLCountry.Nom(.Country, BLL.BLLApp.Lang)
            TextBoxNom.Text = .Nom
            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvent = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Zona
            .Nom = TextBoxNom.Text
        End With
        Dim exs As New List(Of Exception)
        If BLL.BLLZona.Update(_Zona, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zona))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la zona")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem la zona?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLZona.Delete(_Zona, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zona))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la zona")
            End If
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Locations
                Static Done As Boolean
                If Not Done Then
                    refrescaLocations()
                    Done = True
                End If
            Case Tabs.Reps
                Static Done2 As Boolean
                If Not Done2 Then
                    refrescaReps()
                    Done2 = True
                End If
        End Select
    End Sub

    Private Sub Xl_Locations1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToAddNew
        Dim oLocation As DTOLocation = BLL.BLLLocation.NewFromZona(_Zona)
        Dim oFrm As New Frm_Location(oLocation)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaLocations
        oFrm.Show()
    End Sub

    Private Sub Xl_Locations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToRefresh
        refrescaLocations()
    End Sub

    Private Sub refrescaLocations()
        Dim oLocations As List(Of DTOLocation) = BLL.BLLLocations.FromZona(_Zona)
        Xl_Locations1.Load(oLocations)
    End Sub

    Private Sub refrescaReps()
        Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(_Zona)
        Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByArea)
    End Sub

    Private Sub Xl_RepProductsxRep1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProducts1.RequestToAddNew
        Dim oRepProduct As DTORepProduct = BLL.BLLRepProduct.NewRepProduct(Nothing, Nothing, _Zona)
        Dim oRepProducts As New List(Of DTORepProduct)
        Dim oFrm As New Frm_RepProduct(oRepProducts)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaReps
        oFrm.Show()
    End Sub
End Class