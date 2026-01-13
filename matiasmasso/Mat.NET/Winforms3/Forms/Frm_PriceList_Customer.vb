Public Class Frm_PriceList_Customer
    Private _PriceList As DTOPricelistCustomer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Sub New(oPriceList As DTOPricelistCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _PriceList = oPriceList
    End Sub

    Private Sub Frm_PriceList_Customer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.PriceListCustomer.Load(exs, _PriceList) Then
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
                Xl_PriceListItems_Customer1.Load(_PriceList.Items)
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        With _PriceList
            .Fch = DateTimePicker1.Value
            .FchEnd = IIf(CheckBoxFchEnd.Checked, DateTimePickerFchEnd.Value, Nothing)
            .Concepte = TextBoxConcepte.Text
            If Xl_Contact21.Contact Is Nothing Then
                .Customer = Nothing
            Else
                .Customer = New DTOCustomer(Xl_Contact21.Contact.Guid)
            End If
            .Items = Xl_PriceListItems_Customer1.Items
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.PriceListCustomer.Update(exs, _PriceList) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_PriceList))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
        Dim exs As New List(Of Exception)
        If FEB.PriceListCustomer.Load(exs, _PriceList) Then
            Dim oSheet As MatHelper.Excel.Sheet = DTOPricelistCustomer.ExcelSheet(_PriceList)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub CheckBoxFchEnd_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxFchEnd.CheckedChanged
        If _AllowEvents Then
            DateTimePickerFchEnd.Visible = CheckBoxFchEnd.Checked
            DateTimePickerFchEnd.Value = DTO.GlobalVariables.Today()
        End If
    End Sub

    Private Sub Xl_PriceListItems_Customer1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Customer1.RequestToAddNew
        Dim oItem As New DTOPricelistItemCustomer(_PriceList)
        Dim oFrm As New Frm_PricelistItemCustomer(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaItems
        oFrm.Show()
    End Sub

    Private Sub refrescaItems()
        Dim exs As New List(Of Exception)
        If FEB.PriceListCustomer.Load(exs, _PriceList, True) Then
            Xl_PriceListItems_Customer1.Load(_PriceList.Items)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_PriceListItems_Customer1_RequestToImport(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Customer1.RequestToImport
        Dim exs As New List(Of Exception)
        Dim oCsv As DTOCsv = Nothing

        If UIHelper.LoadCsvDialog(oCsv, "Importar tarifa Csv ref/pvp") Then
            UIHelper.ToggleProggressBar(Panel1, True)
            Dim oEmp As DTOEmp = Current.Session.Emp
            If FEB.PriceListCustomer.Load(exs, _PriceList, True) Then
                Dim errors As New List(Of String)
                For Each oRow As DTOCsvRow In oCsv.Rows
                    Dim sRef As String = oRow.Cells(0)

                    If IsNumeric(sRef) Then
                        Dim oSku = Await FEB.ProductSku.FromId(exs, Current.Session.Emp, sRef)
                        If exs.Count = 0 Then
                            If oSku Is Nothing Then
                                errors.Add("l'article '" & sRef & "' no existeix")
                            Else
                                Dim sPvp As String = oRow.Cells(1)
                                Dim dcPvp As Decimal
                                If Decimal.TryParse(sPvp, dcPvp) Then
                                    Dim item As New DTOPricelistItemCustomer(_PriceList)
                                    item.Sku = Await FEB.ProductSku.Find(exs, oSku.Guid)
                                    If exs.Count = 0 Then
                                        item.Retail = DTOAmt.Factory(dcPvp)
                                        _PriceList.Items.Add(item)
                                    Else
                                        UIHelper.WarnError(exs)
                                    End If
                                Else
                                    errors.Add("el preu " & sPvp & " de l'article '" & sRef & "' no es numeric")
                                End If
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        errors.Add("la referencia de article '" & sRef & "' no es numerica")
                    End If
                Next

                Xl_PriceListItems_Customer1.Load(_PriceList.Items)
                UIHelper.ToggleProggressBar(Panel1, False)
                If exs.Count > 0 Then
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.ToggleProggressBar(Panel1, False)
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("eliminem la tarifa " & _PriceList.Concepte & " del " & _PriceList.Fch.ToShortDateString & " ?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.PriceListCustomer.Delete(exs, _PriceList) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_PriceListItems_Customer1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Customer1.RequestToRefresh
        Dim exs As New List(Of Exception)
        _PriceList = Await FEB.PriceListCustomer.Find(exs, _PriceList.Guid)
        If exs.Count = 0 Then
        Else
            UIHelper.WarnError(exs)
        End If
        Xl_PriceListItems_Customer1.Load(_PriceList.Items)
    End Sub


End Class