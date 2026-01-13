
Public Class Frm_Tarifa
    Private _Contact As Contact

    Public Sub New(oContact As Contact)
        MyBase.New()
        Me.InitializeComponent()
        _Contact = oContact
        TextBoxClient.Text = _Contact.Clx
    End Sub

    Private Function Tarifa() As PdfTarifa
        Dim oClient As New Client(_Contact.Guid)
        Dim oTarifaType As PdfTarifa.Types = IIf(CheckBoxCosts.Checked, PdfTarifa.Types.Cost, PdfTarifa.Types.Venda)
        Dim retval As New PdfTarifa(DateTimePicker1.Value, oClient, oTarifaType)
        Return retval
    End Function

    Private Sub ButtonPdf_Click(sender As Object, e As System.EventArgs) Handles ButtonPdf.Click
        Dim oCustomer As New DTOCustomer(_Contact.Guid)
        Dim oPriceList As New DTOPricelistCustomer
        oPriceList.Customer = oCustomer
        Dim sUrl As String = ""
        Select Case CheckBoxCosts.Checked
            Case True
                sUrl = BLL.BLLPricelistCustomer.UrlCost(oPriceList, DateTimePicker1.Value)
            Case False
                sUrl = BLL.BLLPricelistCustomer.UrlPvp(oPriceList, DateTimePicker1.Value)
        End Select
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub ButtonCopyLink_Click(sender As Object, e As System.EventArgs) Handles ButtonCopyLink.Click
        Dim oCustomer As New DTOCustomer(_Contact.Guid)
        Dim oPriceList As New DTOPricelistCustomer()
        oPriceList.Customer = oCustomer
        Dim sUrl As String = ""
        Select Case CheckBoxCosts.Checked
            Case True
                sUrl = BLL.BLLPricelistCustomer.UrlCost(oPriceList, DateTimePicker1.Value)
            Case False
                sUrl = BLL.BLLPricelistCustomer.UrlPvp(oPriceList, DateTimePicker1.Value)
        End Select
        Clipboard.SetDataObject(sUrl, True)
        MsgBox("adreça copiada al portapapers:" & vbCrLf & sUrl)
    End Sub
End Class