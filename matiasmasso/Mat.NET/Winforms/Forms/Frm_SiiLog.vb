Public Class Frm_SiiLog
    Private _value As DTOSiiLog

    Public Sub New(value As DTOSiiLog)
        MyBase.New
        InitializeComponent()
        _value = value
    End Sub

    Private Sub Frm_SiiLog_Load(sender As Object, e As EventArgs) Handles Me.Load
        BLLSiiLog.Load(_value)
        With _value
            TextBoxFch.Text = Format(.Fch, "dd/MM/yy HH:mm:ss")
            TextBoxTipoDeComunicacion.Text = .TipoDeComunicacion
            PictureBox1.Image = BLLSiiLog.ResultIcon(_value)
            ToolTip1.SetToolTip(PictureBox1, BLLSiiLog.ResultText(_value))
            TextBoxCsv.Text = .Csv
            Select Case .Contingut
                Case DTOSiiLog.Continguts.Facturas_Emitidas
                    LabelContingut.Text = "Factures emeses:"
                    'Dim oInvoices As List(Of DTOInvoice) = BLLInvoices.Headers(_value)
                    'Dim oControl As New Xl_Invoices
                    'oControl.Dock = DockStyle.Fill
                    'oControl.Load(oInvoices)
                    'PanelContingut.Controls.Add(oControl)
                Case DTOSiiLog.Continguts.Facturas_Recibidas
                    LabelContingut.Text = "Factures rebudes:"
                    'Dim oBookFras As List(Of DTOBookFra) = BLLBookFras.All(_value)
                    Dim oControl As New Xl_BookFras
                    oControl.Dock = DockStyle.Fill
                    'oControl.Load(oBookFras)
                    PanelContingut.Controls.Add(oControl)
            End Select
        End With
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class