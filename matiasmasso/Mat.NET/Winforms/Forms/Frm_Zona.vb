Public Class Frm_Zona
    Private _Zona As DTOZona
    Private Done As Boolean
    Private Done2 As Boolean

    Private _AllowEvent As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event AfterDelete(sender As Object, e As MatEventArgs)

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
        Dim exs As New List(Of Exception)
        If FEB2.Zona.Load(_Zona, exs) Then
            With _Zona
                TextBoxPaisNom.Text = DTOCountry.NomTraduit(.Country, DTOApp.current.lang)
                TextBoxNom.Text = .Nom
                Xl_LookupProvincia1.AreaProvincia = .Provincia
                ComboBoxExportCod.SelectedIndex = .ExportCod
                Xl_Langs1.Value = .Lang
                CheckBoxSplitByComarcas.Checked = .SplitByComarcas
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvent = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
         Xl_LookupProvincia1.AfterUpdate,
          ComboBoxExportCod.SelectedIndexChanged,
           Xl_Langs1.AfterUpdate,
            CheckBoxSplitByComarcas.CheckedChanged

        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Zona
            .Nom = TextBoxNom.Text
            .Provincia = Xl_LookupProvincia1.AreaProvincia
            .ExportCod = ComboBoxExportCod.SelectedIndex
            .Lang = Xl_Langs1.Value
            .SplitByComarcas = CheckBoxSplitByComarcas.Checked
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.Zona.Update(_Zona, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zona))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click

        Dim message As String = "Cal sel·leccionar una zona on assignar les poblacions i registres que estaven assignats a la zona que volem eliminar"
        Dim rc = MsgBox(message, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim oCountry = _Zona.Country
            oCountry.zonas.Remove(_Zona)
            Dim oCountries = {oCountry}.ToList
            Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona,, oCountries)
            AddHandler oFrm.onItemSelected, AddressOf onZonaToSelected
            oFrm.Show()
        End If

    End Sub

    Private Async Sub onZonaToSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oZonaTo = e.Argument
        If Await FEB2.Zona.Delete(exs, _Zona, oZonaTo) Then
            RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Locations
                If Not Done Then
                    refrescaLocations()
                    Done = True
                End If
            Case Tabs.Reps
                If Not Done2 Then
                    Await refrescaReps()
                    Done2 = True
                End If
        End Select
    End Sub

    Private Sub Xl_Locations1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToAddNew
        Dim oLocation = DTOLocation.Factory(_Zona)
        Dim oFrm As New Frm_Location(oLocation)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaLocations
        oFrm.Show()
    End Sub

    Private Sub Xl_Locations1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Locations1.RequestToRefresh
        refrescaLocations()
    End Sub

    Private Async Sub refrescaLocations()
        Dim exs As New List(Of Exception)
        Dim oLocations = Await FEB2.Locations.FromZona(_Zona, exs)
        If exs.Count = 0 Then
            Xl_Locations1.Load(oLocations)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub refrescaReps(sender As Object, e As MatEventArgs)
        Await refrescaReps()
    End Sub

    Private Async Function refrescaReps() As Task
        Dim exs As New List(Of Exception)
        Dim oRepProducts = Await FEB2.RepProducts.All(exs, _Zona)
        If exs.Count = 0 Then
            Xl_RepProducts1.Load(oRepProducts, Xl_RepProducts.Modes.ByArea)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_RepProductsxRep1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_RepProducts1.RequestToAddNew
        Dim oRepProduct = DTORepProduct.Factory(Nothing, Nothing, _Zona)
        Dim oRepProducts As New List(Of DTORepProduct)
        Dim oFrm As New Frm_RepProduct(oRepProducts)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaReps
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupProvincia1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupProvincia1.onLookUpRequest
        Dim exs As New List(Of Exception)
        If exs.Count = 0 Then
            Dim oProvincia = Xl_LookupProvincia1.AreaProvincia
            Dim oFrm As New Frm_AreaProvincias(_Zona.Country, oProvincia, DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.itemSelected, AddressOf onProvinciaSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onProvinciaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupProvincia1.AreaProvincia = e.Argument
        If _AllowEvent Then
            ButtonOk.Enabled = True
        End If

    End Sub
End Class