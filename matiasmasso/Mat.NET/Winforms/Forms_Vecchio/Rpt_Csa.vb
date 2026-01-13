Imports System.Windows.Forms
Imports System.Drawing

Public Class Rpt_Csa
    Inherits System.Windows.Forms.Form

    Private WithEvents mScratch As maxisrvr.ReportDocument

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        Me.Text = "Rpt_Csa"
    End Sub

#End Region

    Private mCsas As List(Of DTOCsa)
    Private mCsaIdx As Integer
    Private mItmIdx As Integer

    Private mColDoc As New maxisrvr.ReportColumn("document", "2003.999.999")
    Private mColEur As New maxisrvr.ReportColAmt()
    Private mColVto As New maxisrvr.ReportColFch("venciment")
    Private mColNom As New maxisrvr.ReportColumn("lliurat")
    Private mColCcc As New maxisrvr.ReportColumn("compte corrent", "12341234121234567890")
    Private mColTxt As New maxisrvr.ReportColumn("concepte", "fra.0000 del 00/00/00")

    Public WriteOnly Property Csas() As List(Of DTOCsa)
        Set(ByVal Value As List(Of DTOCsa))
            mCsas = Value
            mScratch = New MaxiSrvr.ReportDocument()
            With mScratch
                .Font = New Font("Arial", 8, System.Drawing.FontStyle.Regular)
                .Arrastra(mColEur, mColNom)
                .GridVisible = True
                With .Columns
                    .Add(mColDoc)
                    .Add(mColEur)
                    .Add(mColVto)
                    .Add(mColNom)
                    .Add(mColCcc)
                    .Add(mColTxt)
                End With
                mColEur.Arrastra = True
            End With
        End Set
    End Property

    Public Sub Print(Optional ByVal oPrintMode As maxisrvr.ReportDocument.PrintModes = maxisrvr.ReportDocument.PrintModes.Preview)
        Select Case oPrintMode
            Case maxisrvr.ReportDocument.PrintModes.Preview
                Dim dlg As New PrintPreviewDialog
                With dlg
                    .Document = mScratch
                    .WindowState = FormWindowState.Maximized
                    .ShowDialog()
                End With
            Case Else
                mScratch.Print()
        End Select
    End Sub

    Private Sub mScratch_PrintPageBodyStart(ByVal sender As Object, ByVal e As maxisrvr.ReportPageEventArgs) Handles mScratch.PrintPageBodyStart
        Do While mItmIdx < mCsas(mCsaIdx).Items.Count
            Dim oCsb As DTO.DTOCsb = mCsas(mCsaIdx).Items(mItmIdx)
            If Not mScratch.PrintNextItmOnSamePage(oCsb, e) Then Exit Sub
            mItmIdx = mItmIdx + 1
        Loop
        mScratch.PrintArrastre("Total EUR", e)
    End Sub

    Private Sub PrintItm(ByVal oObj As Object, ByVal e As maxisrvr.ReportPageEventArgs) Handles mScratch.PrintItm
        Dim oCsb As MaxiSrvr.Csb = CType(oObj, MaxiSrvr.Csb)
        With e
            .WriteColumn(oCsb.Document, mColDoc)
            .WriteColAmt(oCsb.Amt, mColEur, True)
            .WriteColumn(oCsb.Vto.ToShortDateString, mColVto)
            .WriteColumn(oCsb.Client.Clx, mColNom)
            .WriteColumn(oCsb.Iban.Digits, mColCcc)
            .WriteColumn(oCsb.txt, mColTxt)
            .WriteLine()
        End With
    End Sub

    Private Sub mScratch_ReportBegin(ByVal sender As Object, ByVal e As maxisrvr.ReportPageEventArgs) Handles mScratch.ReportBegin
        'nomes surt a la primera página
        e.WriteLine("MATIAS MASSO, S.A.")
        e.WriteLine("Passeig de Sant Gervasi, 50 - 08022 BARCELONA")
        e.WriteLine("tel.: 932.541.522 - fax 932.541.521 - info@matiasmasso.es")
        e.WriteLine("remesa " & mCsas(mCsaIdx).Id & " del " & mCsas(mCsaIdx).fch & " a " & mCsas(mCsaIdx).Banc.Abr)
        e.WriteLine(mCsas(mCsaIdx).Items.Count & " efectos por un total de " & BLLCsa.TotalNominal(mCsas(mCsaIdx)).CurFormatted)
        e.WriteLine("efecto medio: " & BLLCsa.AverageAmount(mCsas(mCsaIdx)).CurFormatted & " a " & mCsas(mCsaIdx).Dias & " días")
        DrawId(e)
    End Sub

    Private Sub DrawId(ByVal e As maxisrvr.ReportPageEventArgs)
        Dim sId As String = mCsas(mCsaIdx).Id
        Dim oFont As New Font("Arial", 24, FontStyle.Bold)
        Dim iWidth As Integer = e.Graphics.MeasureString(sId, oFont).Width
        Dim iHeight As Integer = e.Graphics.MeasureString(sId, oFont).Height
        Dim x As Integer = e.MarginBounds.Right - iWidth
        Dim Y As Integer = e.MarginBounds.Top
        Dim oRect As New Rectangle(x, Y, iWidth, iHeight)
        Dim MyFillBrush As Brush = Brushes.LightBlue
        Dim MyFontBrush As Brush = Brushes.Black
        Dim MyPen As Pen = Pens.Blue
        e.Graphics.DrawEllipse(MyPen, oRect)
        e.Graphics.FillEllipse(MyFillBrush, oRect)
        e.Graphics.DrawString(sId, oFont, MyFontBrush, x, Y)
    End Sub
End Class
