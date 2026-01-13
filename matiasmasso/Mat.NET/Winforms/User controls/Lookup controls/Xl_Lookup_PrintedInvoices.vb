Public Class Xl_Lookup_PrintedInvoices
    Inherits Xl_LookupTextboxButton

    Private _PrintedInvoice As DTOPrintedInvoice
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PrintedInvoice() As DTOPrintedInvoice
        Get
            Return _PrintedInvoice
        End Get
        Set(ByVal value As DTOPrintedInvoice)
            _PrintedInvoice = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PrintedInvoice = Nothing
    End Sub

    Private Sub Xl_LookupPrintedInvoice_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        'Dim oFrm As New Frm_PrintedInvoices(_PrintedInvoice, BLL.Defaults.SelectionModes.Selection)
        'AddHandler oFrm.onItemSelected, AddressOf onPrintedInvoiceSelected
        'oFrm.Show()
    End Sub

    Private Sub onPrintedInvoiceSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PrintedInvoice = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PrintedInvoice Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = String.Format("{0:dd/MM/yy HH:mm:ss} {1}", _PrintedInvoice.Fch, BLLUser.NicknameOrElse(_PrintedInvoice.User))
            'Dim oMenu_PrintedInvoice As New Menu_PrintedInvoice(_PrintedInvoice)
            'AddHandler oMenu_PrintedInvoice.AfterUpdate, AddressOf refresca
            'MyBase.SetContextMenuRange(oMenu_PrintedInvoice.Range)
        End If
    End Sub

End Class