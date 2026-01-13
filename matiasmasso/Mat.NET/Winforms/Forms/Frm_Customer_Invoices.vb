Public Class Frm_Customer_Invoices
    Private _Customer As DTOCustomer
    Private _allowEvents As Boolean

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New
        InitializeComponent()
        _Customer = oCustomer
    End Sub

    Private Async Sub Frm_Customer_Invoices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If _Customer.FullNom = "" Then FEB2.Contact.Load(_Customer, exs)
        Me.Text = String.Format("Factures de {0}", _Customer.FullNom)
        Await refresca()
        _allowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oInvoices = Await FEB2.Invoices.All(exs, _Customer)
        If exs.Count = 0 Then
            If CheckBoxDelivered.Checked = False Then
                oInvoices = oInvoices.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Pending).ToList
            ElseIf CheckBoxDeliverPending.Checked = False Then
                oInvoices = oInvoices.Where(Function(x) x.PrintMode <> DTOInvoice.PrintModes.Pending).ToList
            End If
            Xl_Invoices1.Load(oInvoices, HideCustomerColumn:=True)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Invoices1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Invoices1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub CheckBoxDeliver_CheckedChanged(sender As Object, e As EventArgs) Handles _
        CheckBoxDeliverPending.CheckedChanged,
         CheckBoxDelivered.CheckedChanged

        If _allowEvents Then
            _allowEvents = False
            Dim bothUnchecked As Boolean = CheckBoxDelivered.Checked = False And CheckBoxDeliverPending.Checked = False
            If bothUnchecked Then
                If sender Is CheckBoxDeliverPending Then
                    CheckBoxDelivered.Checked = True
                Else
                    CheckBoxDeliverPending.Checked = True
                End If
            End If
            Await refresca()
            _allowEvents = True
        End If

    End Sub
End Class