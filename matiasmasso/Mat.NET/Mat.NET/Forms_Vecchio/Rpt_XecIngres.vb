Public Class Rpt_XecsPresentacio
    Private mXecsPresentacio As XecsPresentacio
    Private WithEvents mScratch As MaxiSrvr.ReportDocument

    Private mItmIdx As Integer

    Private mColEur As New MaxiSrvr.ReportColAmt("Import", 9999999.99)
    Private mColDoc As New MaxiSrvr.ReportColumn("xec", "0123456789012345")
    Private mColBnc As New MaxiSrvr.ReportColumn("banc", "AAAAAAAAA")
    Private mColNom As New MaxiSrvr.ReportColumn("lliurat")
    Private mColVto As New MaxiSrvr.ReportColFch("venciment")

    Public Sub New(ByVal oXecsPresentacio As XecsPresentacio)
        MyBase.New()
        mXecsPresentacio = oXecsPresentacio
        mScratch = New MaxiSrvr.ReportDocument()
        With mScratch
            .Font = New Font("Arial", 10, System.Drawing.FontStyle.Regular)
            .Arrastra(mColEur, mColNom)
            .GridVisible = True
            With .Columns
                .Add(mColEur)
                .Add(mColDoc)
                .Add(mColBnc)
                .Add(mColNom)
                .Add(mColVto)
            End With
            mColEur.Arrastra = True
        End With
    End Sub

    Public Sub Print(Optional ByVal oPrintMode As MaxiSrvr.ReportDocument.PrintModes = MaxiSrvr.ReportDocument.PrintModes.Preview)
        Select Case oPrintMode
            Case MaxiSrvr.ReportDocument.PrintModes.Preview
                Dim dlg As New PrintPreviewDialog
                With dlg
                    .Document = mScratch
                    .WindowState = FormWindowState.Maximized
                    .ShowDialog()
                End With
            Case MaxiSrvr.ReportDocument.PrintModes.Copia
                mScratch.PrintMode = MaxiSrvr.ReportDocument.PrintModes.Copia
                mScratch.Print()
            Case MaxiSrvr.ReportDocument.PrintModes.Original
                mScratch.PrintMode = MaxiSrvr.ReportDocument.PrintModes.Original
                mScratch.Print()
        End Select
    End Sub

    Private Sub mScratch_PrintPageBodyStart(ByVal sender As Object, ByVal e As MaxiSrvr.ReportPageEventArgs) Handles mScratch.PrintPageBodyStart
        Do While mItmIdx < mXecsPresentacio.Xecs.Count
            Dim oXec As Xec = mXecsPresentacio.Xecs(mItmIdx)
            If Not mScratch.PrintNextItmOnSamePage(oXec, e) Then Exit Sub
            mItmIdx = mItmIdx + 1
        Loop
        mScratch.PrintArrastre("Total EUR", e)
    End Sub

    Private Sub PrintItm(ByVal oObj As Object, ByVal e As MaxiSrvr.ReportPageEventArgs) Handles mScratch.PrintItm
        Dim oXec As Xec = CType(oObj, Xec)
        With e
            .WriteColAmt(oXec.Amt, mColEur, True)
            .WriteColumn(oXec.XecNum, mColDoc)
            .WriteColumn(BLL.BLLIban.BankNom(oXec.Iban), mColBnc)
            .WriteColumn(oXec.Lliurador.Clx, mColNom)
            .WriteColumn(oXec.Vto.ToShortDateString, mColVto)
            .WriteLine()
        End With
    End Sub

    Private Sub mScratch_ReportBegin(ByVal sender As Object, ByVal e As MaxiSrvr.ReportPageEventArgs) Handles mScratch.ReportBegin
        'nomes surt a la primera página
        Dim oBanc As Banc = mXecsPresentacio.Banc
        e.WriteLine()
        e.WriteLine()
        e.WriteLine()
        e.WriteLine()
        e.WriteLine(oBanc.Nom)
        e.WriteLine(oBanc.Adr.Text)
        e.WriteLine(oBanc.Adr.Zip.ZipyCit)
        e.WriteLine(BLL.BLLIban.Formated(oBanc.Iban))
        e.WriteLine()
        e.WriteLine()
        e.WriteLine("REMESA DE XECS PER INGRESSAR EN COMPTE", MaxiSrvr.ReportLineJustification.Centered)

    End Sub

    Private Sub DrawId(ByVal e As MaxiSrvr.ReportPageEventArgs)
        'Dim sId As String = "xx" 'mCsas(mCsaIdx).Id
        'Dim oFont As New Font("Arial", 24, FontStyle.Bold)
        'Dim iWidth As Integer = e.Graphics.MeasureString(sId, oFont).Width
        'Dim iHeight As Integer = e.Graphics.MeasureString(sId, oFont).Height
        'Dim x As Integer = e.MarginBounds.Right - iWidth
        'Dim Y As Integer = e.MarginBounds.Top
        'Dim oRect As New Rectangle(x, Y, iWidth, iHeight)
        'Dim MyFillBrush As Brush = Brushes.LightBlue
        'Dim MyFontBrush As Brush = Brushes.Black
        'Dim MyPen As Pen = Pens.Blue
        'e.Graphics.DrawEllipse(MyPen, oRect)
        'e.Graphics.FillEllipse(MyFillBrush, oRect)
        'e.Graphics.DrawString(sId, oFont, MyFontBrush, x, Y)
    End Sub
End Class
