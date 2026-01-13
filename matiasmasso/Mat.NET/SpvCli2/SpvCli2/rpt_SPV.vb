Imports VB = Microsoft.VisualBasic

Public Class rpt_SPV
    Inherits System.Windows.Forms.Form

    Dim FontStd As New Font("Arial", 11, FontStyle.Regular)
    Dim FontEpg As New Font("Arial", 8, FontStyle.Italic)
    Dim FontItl As New Font("Arial", 11, FontStyle.Italic)
    Dim FontNum As New Font("Arial", 16, FontStyle.Bold)
    Dim hTabLeft As Integer
    Dim hTabRight As Integer
    Dim hTabMid As Integer
    Dim StringOffset As Integer
    Const hTabData = 200
    Const hTabQty = 700
    Const hTabAlb = 500
    Const rowHeight = 24
    Dim row As Integer
    Friend WithEvents HelpProviderHG As HelpProvider
    Private _Spvs As List(Of DTOSpv)


    Public Sub New(oSpvs As List(Of DTOSpv))
        MyBase.New()
        InitializeComponent()
        _Spvs = oSpvs
    End Sub


#Region " Windows Form Designer generated code "

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rpt_SPV))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.HelpProviderHG.SetHelpKeyword(Me.Button1, "rpt_SPV.htm#Button1")
        Me.HelpProviderHG.SetHelpNavigator(Me.Button1, System.Windows.Forms.HelpNavigator.Topic)
        Me.Button1.Location = New System.Drawing.Point(224, 16)
        Me.Button1.Name = "Button1"
        Me.HelpProviderHG.SetShowHelp(Me.Button1, True)
        Me.Button1.Size = New System.Drawing.Size(48, 40)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'rpt_SPV
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.Button1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "rpt_SPV.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Name = "rpt_SPV"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "rpt_SPV"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub PrintDocument1_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static row As Integer

        StringOffset = e.Graphics.MeasureString("0", FontStd).Width
        hTabLeft = e.MarginBounds.Left
        hTabRight = e.MarginBounds.Right
        hTabMid = hTabRight - 310
        'PrintItm(mSpvs(row), sender, e)
        'If row < mSpvs.Count - 1 Then
        PrintItm(_Spvs(row), sender, e)
        If row < _Spvs.Count - 1 Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If
        row = row + 1
    End Sub


    Public Sub Printpreview()
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub


    Public Sub Print()
        With PrintDocument1
            '.DefaultPageSettings.PaperSize.PaperKind.A2()
            .Print()
        End With
        'PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub PrintItm(ByVal itm As DTOSpv, ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)
        Dim i As Integer
        Dim TmpStr As String

        With e.Graphics
            .DrawRectangle(Pens.Black, New Rectangle(hTabMid, 40, hTabRight - hTabMid, 50)) 'SPV nº
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, 130, hTabRight - hTabLeft, 180)) 'datos
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, 330, hTabRight - hTabLeft, 264)) 'averias
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, 660, hTabRight - hTabLeft, 310)) 'trabajos
            .DrawRectangle(Pens.Black, New Rectangle(hTabMid, 990, hTabRight - hTabMid, 50)) 'ALB nº
            '.FillRectangle(Brushes.Aquamarine, New Rectangle(500, 500, 500, 500))
            row = 2

            e.Graphics.DrawString("Parte de reparación nº", FontEpg, Brushes.Black, hTabMid + StringOffset, (row * rowHeight))
            TmpStr = itm.Id
            e.Graphics.DrawString(TmpStr, FontNum, Brushes.Gray, hTabRight - StringOffset - e.Graphics.MeasureString(TmpStr, FontNum).Width, (row * rowHeight))
            row = row + 1

            If itm.Incidencia IsNot Nothing Then
                e.Graphics.DrawString("Incidencia nº " & itm.incidencia.AsinOrNum(), FontEpg, Brushes.Black, hTabMid + StringOffset, (row * rowHeight))
            End If

            row = row + 3
            e.Graphics.DrawString("Cliente", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            e.Graphics.DrawString(itm.Customer.Nom, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("teléfono", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            e.Graphics.DrawString(itm.Customer.Telefon, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("contacto", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            e.Graphics.DrawString(itm.Contacto, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("entrada", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))

            Dim UsrNom As String = itm.UsrRegister.EmailAddress
            If itm.UsrRegister.Nom > "" Then UsrNom = itm.UsrRegister.Nom
            If itm.UsrRegister.NickName > "" Then UsrNom = itm.UsrRegister.NickName
            TmpStr = "(aviso registrado el " & itm.FchAvis & " por " & UsrNom & ")"

            If itm.SpvIn IsNot Nothing Then
                If itm.SpvIn.Id > 0 Then
                    TmpStr = "num." & itm.SpvIn.Id & " del " & itm.SpvIn.Fch & " " & TmpStr
                End If
            End If
            e.Graphics.DrawString(TmpStr, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("artículo", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            e.Graphics.DrawString(itm.product.nom.Esp, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("número de serie", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            row = row + 1
            e.Graphics.DrawString("su referencia:", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            e.Graphics.DrawString(itm.sRef, FontStd, Brushes.Black, hTabData, (row * rowHeight))
            row = 14
            e.Graphics.DrawString("averías:", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            row = row + 1
            'MsgBox(e.Graphics.MeasureString(TmpObs, FontStd).Width & "------" & (hTabRight - (hTabLeft + StringOffset)))

            If itm.ObsClient > "" Then
                Dim tmpObs As String = itm.ObsClient
                If e.Graphics.MeasureString(tmpObs, FontStd).Width > (hTabRight - hTabLeft - StringOffset) Then
                    i = CutString(tmpObs, hTabRight - hTabLeft - StringOffset, e)
                    'TmpStr = VB.Left(tmpObs, i)
                    tmpObs = VB.Left(tmpObs, i)
                Else
                    'TmpStr = tmpObs
                    'tmpObs = ""
                End If
                If e.Graphics.MeasureString(tmpObs, FontStd).Height > 13 * rowHeight Then
                    i = CutStringV(tmpObs, 13 * rowHeight, e)
                    'TmpStr = VB.Left(tmpObs, i)
                    tmpObs = VB.Left(tmpObs, i) ' VB.Mid(tmpObs, i + 1)
                Else
                    'TmpStr = tmpObs
                    'tmpObs = ""
                End If
                e.Graphics.DrawString(tmpObs, FontStd, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))

            End If

            row = 26
            'if itm.s
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft, (row * rowHeight), 15, 15))
            e.Graphics.DrawString("solicita garantía", FontStd, Brushes.Black, hTabLeft + 2 * StringOffset, (row * rowHeight))
            If itm.SolicitaGarantia Then
                e.Graphics.DrawString("X", FontStd, Brushes.Black, hTabLeft + 0 * StringOffset, (row * rowHeight))
            End If
            e.Graphics.DrawString("GARANTIA", FontStd, Brushes.Black, hTabLeft + 25 * StringOffset, (row * rowHeight))
            e.Graphics.DrawString("SI", FontStd, Brushes.Black, hTabLeft + 35 * StringOffset, (row * rowHeight))
            .DrawRectangle(Pens.Black, New Rectangle(hTabLeft + 38 * StringOffset, (row * rowHeight), 15, 15))
            e.Graphics.DrawString("NO", FontStd, Brushes.Black, hTabLeft + 41 * StringOffset, (row * rowHeight))
            .DrawRectangle(Pens.Black, New Rectangle(hTabRight - 15, (row * rowHeight), 15, 15))
            row = 28
            e.Graphics.DrawString("trabajos efectuados", FontEpg, Brushes.Black, hTabLeft + StringOffset, (row * rowHeight))
            TmpStr = "cantidad"
            e.Graphics.DrawString(TmpStr, FontEpg, Brushes.Black, hTabRight - StringOffset - e.Graphics.MeasureString(TmpStr, FontItl).Width, (row * rowHeight))
            row = 42
            e.Graphics.DrawString("bultos", FontEpg, Brushes.Black, hTabMid + StringOffset, (row * rowHeight))
            e.Graphics.DrawString("salida", FontEpg, Brushes.Black, hTabMid + 10 * StringOffset, (row * rowHeight))
        End With

    End Sub

    Private Function CutString(ByVal Source As String, ByVal Width As Integer, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
        Dim retval As Integer
        Dim i As Integer
        For i = Source.Length To 1 Step -1
            If e.Graphics.MeasureString(VB.Left(Source, i), FontStd).Width <= (hTabRight - (hTabLeft + StringOffset)) Then
                retval = i
                Exit For
            End If
        Next
        Return retval
    End Function

    Private Function CutStringV(ByVal Source As String, ByVal Height As Integer, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
        Dim retval As Integer
        Dim i As Integer
        For i = Source.Length To 1 Step -1
            If e.Graphics.MeasureString(VB.Left(Source, i), FontStd).Height <= Height Then
                retval = i
                Exit For
            End If
        Next
        Return retval
    End Function

End Class
