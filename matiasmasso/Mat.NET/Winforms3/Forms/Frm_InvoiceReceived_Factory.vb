Public Class Frm_InvoiceReceived_Factory

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New()
        MyBase.New
        InitializeComponent()
    End Sub

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New
        InitializeComponent()
        Xl_Contact21.Contact = oProveidor
    End Sub

    Private Async Sub Frm_InvoiceReceived_Factory_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Xl_Contact21.Contact IsNot Nothing Then
            Await refresca()
        End If
    End Sub

    Private Function Proveidor() As DTOProveidor
        Return CType(Xl_Contact21.Contact, DTOProveidor)
    End Function

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim oProveidor = Await FEB.Proveidor.Find(Xl_Contact21.Contact.Guid, exs)
        If exs.Count = 0 AndAlso oProveidor IsNot Nothing Then
            Xl_Contact21.Contact = oProveidor
            Xl_Cur1.Cur = oProveidor.Cur
            UIHelper.ToggleProggressBar(PanelButtons, True)
            Dim oPncs = Await FEB.PurchaseOrderItems.Pending(exs, oProveidor, DTOPurchaseOrder.Codis.proveidor, GlobalVariables.Emp.Mgz)
            If exs.Count = 0 Then
                Xl_InvoiceReceivedItemsFactory1.Load(oPncs)
                UIHelper.ToggleProggressBar(PanelButtons, False)
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim firstItemCur = Xl_InvoiceReceivedItemsFactory1.Items.First().Amount.Cur
        Dim sameCur As Boolean = Xl_InvoiceReceivedItemsFactory1.Items.All(Function(x) x.Amount.Cur.Tag = firstItemCur.Tag)
        If Not sameCur Then
            UIHelper.WarnError("s'han seleccionat linies amb diferent divisa")
        ElseIf firstItemCur.Tag <> Xl_Cur1.Cur.Tag Then
            UIHelper.WarnError("la divisa de la factura (" & Xl_Cur1.Cur.Tag & ") no coincideix amb la de les seves linies (" & firstItemCur.Tag & ")")
        Else
            Dim oProveidor As DTOProveidor = Xl_Contact21.Contact
            Dim oInvoiceReceived As New DTOInvoiceReceived()
            With oInvoiceReceived
                .Proveidor = oProveidor
                .ProveidorEan = oProveidor.GLN
                .DocNum = TextBoxDocNum.Text
                .Fch = DateTimePicker1.Value
                .Cur = Xl_Cur1.Cur
                .Items = Xl_InvoiceReceivedItemsFactory1.Items
                .NetTotal = Xl_AmountCurTotal.Amt
                .TaxBase = .NetTotal
            End With
            If Await FEB.InvoiceReceived.Update(exs, oInvoiceReceived) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(oInvoiceReceived))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub

    Private Sub Xl_InvoiceReceivedItemsFactory1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_InvoiceReceivedItemsFactory1.AfterUpdate
        Xl_AmountCurTotal.Amt = Xl_InvoiceReceivedItemsFactory1.Total
        ButtonOk.Enabled = True
    End Sub

    Private Async Sub Xl_Contact21_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact21.AfterUpdate
        Await refresca()
    End Sub

    Private Sub Xl_Contact21_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_Contact21.RequestToToggleProgressBar
        UIHelper.ToggleProggressBar(PanelButtons, e.Argument)
    End Sub
End Class