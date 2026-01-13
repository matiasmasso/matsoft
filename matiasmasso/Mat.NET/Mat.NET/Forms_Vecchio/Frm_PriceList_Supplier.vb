

Public Class Frm_PriceList_Supplier
    Private _PriceList As DTOPriceList_Supplier
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)


    Public Sub New(ByVal oPriceList As DTOPriceList_Supplier)
        MyBase.New()
        Me.InitializeComponent()
        'RichTextBox1.AllowDrop = True

        _PriceList = oPriceList
        If _PriceList.IsNew Then
            Me.Text = _PriceList.Proveidor.FullNom & ": nova tarifa de preus"
            'PanelImportExcel.Visible = True
        Else
            Me.Text = _PriceList.Proveidor.FullNom & ": tarifa de preus"
        End If
        Refresca()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _PriceList.IsLoaded = False
        BLL.BLLPriceList_Supplier.Load(_PriceList)
        With _PriceList
            DateTimePicker1.Value = .Fch
            TextBoxConcepte.Text = .Concepte
            Xl_PercentDiscount_OnInvoice.Value = .Discount_OnInvoice
            Xl_PercentDiscount_OffInvoice.Value = .Discount_OffInvoice
            Xl_Cur1.Cur = App.Current.GetOldCur(.Cur.Id.ToString)
            Xl_PriceListItems_Supplier1.Load(_PriceList)
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        DateTimePicker1.ValueChanged, _
         TextBoxConcepte.TextChanged, _
          Xl_PercentDiscount_OnInvoice.AfterUpdate, _
          Xl_PercentDiscount_OffInvoice.AfterUpdate, _
           Xl_Cur1.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        'If TextBoxSearch.Text > "" Then
        ' MsgBox("cal treure el filtre per guardar els canvis", MsgBoxStyle.Exclamation)
        ' Else
        With _PriceList
            .Fch = DateTimePicker1.Value
            .Concepte = TextBoxConcepte.Text
            .Discount_OnInvoice = Xl_PercentDiscount_OnInvoice.Value
            .Discount_OffInvoice = Xl_PercentDiscount_OffInvoice.Value
            .Cur = New DTOCur
            .Cur.Id = [Enum].Parse(GetType(DTOCur.Ids), Xl_Cur1.Cur.Id)
            .Items = Xl_PriceListItems_Supplier1.values

            Dim exs as New List(Of exception)
            If BLL.BLLPriceList_Supplier.Update(_PriceList, exs) Then
                RaiseEvent AfterUpdate(_PriceList, System.EventArgs.Empty)
                Me.Close()
            Else
                MsgBox("error al desar la tarifa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
        'End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs as New List(Of exception)
        If BLL.BLLPriceList_Supplier.Delete(_PriceList, exs) Then
            RaiseEvent AfterUpdate(Nothing, EventArgs.Empty)
            Me.Close()
        Else
            MsgBox("error al eliminar la tarifa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub AddNew(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oItem As New PriceListItem_Supplier(_PriceList)
        'Dim oFrm As New Frm_PriceListItem_Supplier(oItem)
        'AddHandler oFrm.AfterUpdate, AddressOf onAddNew
        'oFrm.show()
    End Sub


    Private Sub OnAddNew(sender As Object, e As System.EventArgs)
        'Dim oItems As PriceListItems_Supplier = DataGridView1.DataSource
        ' _PriceList.Items.Add(sender)
        ' LoadGrid(_PriceList.Items)
        ' ButtonOk.Enabled = True
    End Sub


    Private Sub TextBoxSearch_TextChanged(sender As Object, e As EventArgs) Handles TextBoxSearch.TextChanged
        Xl_PriceListItems_Supplier1.ApplyFilter(TextBoxSearch.Text)
        'Dim oItems As List(Of DTOPriceListItem_Supplier) = BLL_PriceListItems_Supplier.FilterBy(_PriceList.Items, TextBoxSearch.Text)
        'LoadGrid(oItems)
    End Sub


    Private Sub ImportarToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Xl_PriceListItems_Supplier1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Supplier1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_PriceListItems_Supplier1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Supplier1.RequestToAddNew
        Dim oItem As New DTOPriceListItem_Supplier()
        With oItem
            .Parent = _PriceList
            .IsNew = True
        End With
        Dim oFrm As New Frm_PriceListItem_Supplier(oItem)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub


    Private Sub Xl_PriceListItems_Supplier1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceListItems_Supplier1.RequestToRefresh
        Refresca()
    End Sub
End Class