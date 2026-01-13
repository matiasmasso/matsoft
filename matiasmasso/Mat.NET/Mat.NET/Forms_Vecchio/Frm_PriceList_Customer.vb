Public Class Frm_PriceList_Customer
    Private _PriceList As DTOPricelistCustomer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oPriceList As DTOPricelistCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _PriceList = oPriceList
        BLL.BLLPricelistCustomer.load(_PriceList)
        With _PriceList
            If .IsNew Then
                ButtonOk.Enabled = True
            End If
            DateTimePicker1.Value = .Fch
            If Not .FchEnd = Nothing Then
                DateTimePickerFchEnd.Value = .FchEnd
                DateTimePickerFchEnd.Visible = True
            End If
            TextBoxConcepte.Text = .Concepte
            Xl_Contact21.Contact = .Customer
            Xl_PriceListItems_Customer1.Load(oPriceList.Items)
        End With
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With _PriceList
            .Fch = DateTimePicker1.Value
            .FchEnd = IIf(CheckBoxFchEnd.Checked, DateTimePickerFchEnd.Value, Nothing)
            .Concepte = TextBoxConcepte.Text
            If Xl_Contact21.Contact Is Nothing Then
                .Customer = Nothing
            Else
                If Xl_Contact21.Contact IsNot Nothing Then
                    .Customer = Nothing
                Else
                    .Customer = New DTOCustomer(Xl_Contact21.Contact.Guid)
                End If
            End If
            .Items = Xl_PriceListItems_Customer1.Items
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLPricelistCustomer.Update(_PriceList, exs) Then
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la tarifa:")
        End If
    End Sub

    Private Sub ControlChanged(sender As Object, e As System.EventArgs) Handles _
        TextBoxConcepte.TextChanged, _
         DateTimePicker1.ValueChanged

        If _allowevents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim oExcelSheet As DTOExcelSheet = BLL.BLLPricelistCustomer.ExcelSheet(_PriceList)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub

    Private Sub CheckBoxFchEnd_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchEnd.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchEnd.Visible = CheckBoxFchEnd.Checked
            DateTimePickerFchEnd.Value = Today
        End If
    End Sub

    Private Sub Xl_PriceListItems_Customer1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Customer1.RequestToAddNew
        Dim oItem As New DTOPricelistItemCustomer(_PriceList)
        Dim oFrm As New Frm_PricelistItemCustomer(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaItems
        oFrm.Show()
    End Sub

    Private Sub refrescaItems()
        BLL.BLLPricelistCustomer.Load(_PriceList, True)
        Xl_PriceListItems_Customer1.Load(_PriceList.Items)

    End Sub

    Private Sub Xl_PriceListItems_Customer1_RequestToImport(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Customer1.RequestToImport
        Dim oCsv As DTOCsv = Nothing
        If UIHelper.LoadCsvDialog(oCsv, "Importar tarifa Csv ref/pvp") Then
            Dim oEmp As DTOEmp = BLL.BLLApp.Emp
            BLL.BLLPricelistCustomer.Load(_PriceList, True)
            Dim errors As New List(Of String)
            For Each oRow As DTOCsvRow In oCsv.Rows
                Dim sRef As String = oRow.Cells(0)

                If IsNumeric(sRef) Then
                    Dim oSku As DTOProductSku = BLL.BLLProductSku.FromId(oEmp, sRef)
                    If oSku Is Nothing Then
                        errors.Add("l'article '" & sRef & "' no existeix")
                    Else
                        Dim sPvp As String = oRow.Cells(1)
                        Dim dcPvp As Decimal
                        If Decimal.TryParse(sPvp, dcPvp) Then
                            Dim item As New DTOPricelistItemCustomer(_PriceList)
                            item.Sku = BLL.BLLProductSku.Find(oSku.Guid)
                            item.Retail = New DTOAmt(dcPvp)
                            _PriceList.Items.Add(item)
                        Else
                            errors.Add("el preu " & sPvp & " de l'article '" & sRef & "' no es numeric")
                        End If
                    End If
                Else
                    errors.Add("la referencia de article '" & sRef & "' no es numerica")
                End If
            Next

            Xl_PriceListItems_Customer1.Load(_PriceList.Items)

        End If
    End Sub
End Class