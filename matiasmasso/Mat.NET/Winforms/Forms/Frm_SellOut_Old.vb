Public Class Frm_SellOut_Old
    Private _value As DTOSellOut
    Private _AllowEvents As Boolean

    Public Sub New(Optional oSellOut As DTOSellOut = Nothing)
        MyBase.New
        InitializeComponent()

        If oSellOut Is Nothing Then
            _value = BLLSellOut.Factory(BLLSession.Current.User)
        Else
            _value = oSellOut
        End If

    End Sub

    Private Sub Frm_SellOut_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadConceptTypes()
        LoadProveidors()
        LoadChannels()
        refresca()
    End Sub

    Private Sub refresca()
        BLLSellOut.Load(_value)
        Dim iYears As List(Of Integer) = BLLSellOut.years(_value)
        Dim iYear As Integer = _value.YearMonths.Last.Year
        If Xl_Years1.IsEmpty Then Xl_Years1.Load(iYears, iYear)
        If _value.Products IsNot Nothing AndAlso _value.Products.Count > 0 Then Xl_LookupProduct1.Load(_value.Products.First)
        If _value.Areas IsNot Nothing AndAlso _value.Areas.Count > 0 Then Xl_LookupArea1.Area = _value.Areas.First
        If _value.Customers IsNot Nothing AndAlso _value.Customers.Count > 0 Then
            Dim oCustomer As DTOCustomer = _value.Customers.First
            BLLContact.Load(oCustomer)
            Me.Text = "Sellout " & oCustomer.FullNom
        End If
        ComboBoxFormat.SelectedIndex = _value.Format
        Xl_SellOut1.Load(_value)
        _AllowEvents = True
    End Sub

    Private Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        If _AllowEvents Then
            Dim iYear As Integer = Xl_Years1.Value
            Dim oYearMonthTo As New DTOYearMonth(iYear, 12)
            _value.YearMonths = BLLYearMonths.Last12YearMonths(oYearMonthTo)
            refresca()
        End If
    End Sub


    Private Sub ComboBoxConceptType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxConceptType.SelectedIndexChanged
        If _AllowEvents Then
            _value.ConceptType = ComboBoxConceptType.SelectedIndex
            refresca()
        End If
    End Sub

    Private Sub LoadConceptTypes()
        Dim oLang As DTOLang = BLLSession.Current.Lang
        Dim items As New List(Of String)
        For Each cod In [Enum].GetValues(GetType(DTOSellOut.ConceptTypes))
            ComboBoxConceptType.Items.Add(DTOSellOut.ConceptTypeNom(cod, oLang))
        Next
        ComboBoxConceptType.SelectedIndex = _value.ConceptType
    End Sub

    Private Sub LoadProveidors()
        Dim items As List(Of DTOProveidor) = BLLSellOut.Proveidors(_value)
        Dim NoItem As New DTOProveidor(Guid.Empty)
        NoItem.Nom = "(tots els proveidors)"
        items.Insert(0, NoItem)
        With ComboBoxProveidors
            .DataSource = items
            .DisplayMember = "Nom"
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub LoadChannels()
        Dim oLang As DTOLang = BLLSession.Current.Lang
        Dim items As List(Of DTODistributionChannel) = BLLSellOut.Channels(_value)
        Dim NoItem As New DTODistributionChannel(Guid.Empty)
        NoItem.LangText = New DTOLangText("(todos los canales)", "(tots els canals)", "(all channels)")
        items.Insert(0, NoItem)
        With ComboBoxChannels
            .Items.Clear()
            For Each item In items
                Dim oListItem As New MatListItem(item, item.LangText.Tradueix(oLang))
                ComboBoxChannels.Items.Add(oListItem)
            Next
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub ComboBoxProveidors_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxProveidors.SelectedIndexChanged
        If _AllowEvents Then
            Dim oProveidor As DTOProveidor = ComboBoxProveidors.SelectedItem
            _value.Proveidors = New List(Of DTOProveidor)
            _value.Proveidors.Add(oProveidor)
            refresca()
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oSheet As DTOExcelSheet = BLLSellOut.Excel(_value)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ComboBoxChannels_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxChannels.SelectedIndexChanged
        If _AllowEvents Then
            Dim oListItem As MatListItem = ComboBoxChannels.SelectedItem
            Dim oChannel As DTODistributionChannel = oListItem.Value
            Dim oChannels As New List(Of DTODistributionChannel)
            oChannels.Add(oChannel)
            _value.DistributionChannels = oChannels
            refresca()
        End If
    End Sub

    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        Dim oProducts As New List(Of DTOProduct)
        oProducts.Add(e.Argument)
        _value.Products = oProducts
        refresca()
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        Dim oAreas As New List(Of DTOArea)
        oAreas.Add(e.Argument)
        _value.Areas = oAreas
        refresca()
    End Sub

    Private Sub ComboBoxFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFormat.SelectedIndexChanged
        If _AllowEvents Then
            _value.Format = ComboBoxFormat.SelectedIndex
            refresca()
        End If
    End Sub

    Private Sub AgruparPerCompteClientToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GroupToolStripMenuItem.Click
        _value.GroupByHolding = GroupToolStripMenuItem.Checked
        refresca()
    End Sub
End Class