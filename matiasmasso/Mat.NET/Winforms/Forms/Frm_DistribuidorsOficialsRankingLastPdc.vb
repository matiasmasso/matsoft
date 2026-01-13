Public Class Frm_DistribuidorsOficialsRankingLastPdc
    Private _Items As List(Of DTOCliProductBlocked)
    Private _AllowEvents As Boolean

    Private Async Sub Frm_DistribuidorsOficialsRankingLastPdc_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub ComboBoxBrands_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBrands.SelectedIndexChanged
        If _AllowEvents Then
            Dim oItems As List(Of DTOCliProductBlocked)
            If ComboBoxBrands.SelectedIndex = 0 Then
                oItems = _Items
            Else
                oItems = _Items.Where(Function(x) CType(x.product, DTOProduct).nom = ComboBoxBrands.SelectedItem).ToList
            End If
            Xl_DistribuidorsOficialsRankingLastPdc1.Load(_Items)
        End If
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        Dim oItems As List(Of DTOCliProductBlocked) = Nothing
        Dim oArea As DTOArea = e.Argument

        If TypeOf oArea Is DTOCountry Then
            oItems = _Items.Where(Function(x) x.Contact.Address.Zip.Location.Zona.Country.Equals(oArea)).ToList
        ElseIf TypeOf oArea Is DTOZona Then
            oItems = _Items.Where(Function(x) x.Contact.Address.Zip.Location.Zona.Equals(oArea)).ToList
        ElseIf TypeOf oArea Is DTOLocation Then
            oItems = _Items.Where(Function(x) x.Contact.Address.Zip.Location.Equals(oArea)).ToList
        ElseIf TypeOf oArea Is DTOZip Then
            oItems = _Items.Where(Function(x) x.Contact.Address.Zip.Equals(oArea)).ToList
        End If

        Xl_DistribuidorsOficialsRankingLastPdc1.Load(oItems)
    End Sub

    Private Async Sub Xl_DistribuidorsOficialsRankingLastPdc1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_DistribuidorsOficialsRankingLastPdc1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _AllowEvents = False
        _Items = Await FEB2.CliProductsBlocked.DistribuidorsOficialsRankingLastPdc(exs)
        If exs.Count = 0 Then
            Dim sProducts As List(Of String) = _Items.GroupBy(Function(g) CType(g.product, DTOProduct)).Select(Function(group) group.Key.nom.Tradueix(Current.Session.Lang)).Distinct.ToList
            sProducts.Insert(0, "(tots els productes")
            ComboBoxBrands.DataSource = sProducts
            ComboBoxBrands.SelectedIndex = 0

            Xl_DistribuidorsOficialsRankingLastPdc1.Load(_Items)
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If

    End Function
End Class