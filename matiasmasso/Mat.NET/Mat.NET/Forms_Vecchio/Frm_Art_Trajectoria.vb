

Public Class Frm_Art_Trajectoria
    Private mArt As art
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Fch
        inp
        outp
        pn2
        pn1
        stk
        pn2x
        pn1x
        dias
    End Enum

    Public WriteOnly Property Stp() As Stp
        Set(ByVal oStp As Stp)
            mArt = Art.Empty(oStp.Tpa.emp)
            mArt.Stp = oStp
            PictureBoxGrafica.Image = GetGrafica()
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadYeas()
        Dim SQL As String = "SELECT YEA FROM PNC INNER JOIN " _
                & "ProductParent ON PNC.ArtGuid=ProductParent.Child " _
                & "WHERE ProductParent.parent =@Category" _
                & "GROUP BY YEA " _
                & "ORDER BY YEA DESC"

        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Category", mArt.Stp.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)

        With ComboBoxYea
            .DataSource = oTb
            .ValueMember = "yea"
            .DisplayMember = "yea"
            If oTb.Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
    End Sub

    Private Function CurrentYea() As Integer
        If IsNumeric(ComboBoxYea.Text) Then
            Return CInt(ComboBoxYea.Text)
        End If
        Return 2007
    End Function

    Private Sub LoadData()
        If CurrentYea() = 0 Then LoadYeas()

        Dim SQL As String = "SELECT HST.fch, " _
        & "SUM(HST.INP) AS INP, " _
        & "SUM(HST.OUTP) AS OUTP, " _
        & "SUM(HST.PN2) AS PN2, " _
        & "SUM(HST.PN1) AS PN1, " _
        & "0 AS STK, " _
        & "0 AS PN2X, " _
        & "0 AS PN1X, " _
        & "0 AS DIAS " _
        & "FROM " _
        & "(SELECT  emp, art, fch, INP, OUTP, PN1, PN2 " _
        & "FROM " _
        & "(SELECT emp, art, fch, SUM(CASE WHEN ARC.COD < 50 THEN QTY ELSE 0 END) AS INP, SUM(CASE WHEN ARC.COD < 50 THEN 0 ELSE QTY END) AS OUTP, 0 AS PN1, 0 AS PN2 " _
        & "FROM ARC GROUP BY emp, art, fch) AS derivedtbl_1 " _
        & "UNION " _
        & "SELECT Pdc.Emp, PNC.art, PDC.fch, 0 AS Expr1, 0 AS Expr2, SUM(CASE WHEN PNC.COD = 1 THEN QTY ELSE 0 END) AS Expr3,  SUM(CASE WHEN PNC.COD = 2 THEN QTY ELSE 0 END) AS Expr4 " _
        & "FROM  PNC INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "GROUP BY Pdc.Emp, PNC.art, PDC.fch) AS HST " _
        & "INNER JOIN ART ON HST.emp = ART.emp AND HST.art = ART.art " _
        & "WHERE Art.Category ='" & mArt.Stp.Guid.ToString & "' " _
        & "GROUP BY HST.fch " _
        & "ORDER BY HST.fch"

        mDs = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)
        Dim oRow As DataRow
        Dim iStk As Integer
        Dim iPn2 As Integer
        Dim iPn1 As Integer
        If oTb.Rows.Count > 0 Then
            Dim DtLastDate As Date = oTb.Rows(0)(Cols.Fch)
            For Each oRow In oTb.Rows
                iStk = iStk + oRow(Cols.inp) - oRow(Cols.outp)
                iPn2 = iPn2 + oRow(Cols.pn2) - oRow(Cols.outp)
                iPn1 = iPn1 + oRow(Cols.pn1) - oRow(Cols.inp)
                oRow(Cols.stk) = iStk
                oRow(Cols.pn2x) = iPn2
                oRow(Cols.pn1x) = iPn1
                oRow(Cols.dias) = DateDiff(DateInterval.Day, DtLastDate, oRow(Cols.Fch))
                DtLastDate = oRow(Cols.Fch)
            Next
        End If

    End Sub

    Private Sub LoadGrid()
        If mDs Is Nothing Then LoadData()

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mDs.Tables(0)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.inp)
                .HeaderText = "entrades"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.outp)
                .HeaderText = "sortides"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.stk)
                .HeaderText = "stock"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pn2)
                .HeaderText = "clients"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pn2x)
                .HeaderText = "pendent"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pn1)
                .HeaderText = "proveidors"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.pn1x)
                .HeaderText = "pendent"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.dias)
                .HeaderText = "dies"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 55
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

    End Sub

    Private Function GetGrafica() As Bitmap
        Dim iHeight As Integer = PictureBoxGrafica.Height
        Dim xPos As Integer = 0
        Dim yPos As Integer = 0
        Dim yFactor As Decimal
        Dim xFactor As Integer = 2

        Dim oBitmap As New Bitmap(365 * xFactor, iHeight)
        Dim oGraphics As Graphics = Graphics.FromImage(oBitmap)

        'pinta mesos
        Dim oRectangle As Rectangle
        Dim DtFirstDate As Date
        Dim i As Integer
        For i = 1 To 12
            If i Mod 2 = 1 Then
                DtFirstDate = New Date(CurrentYea, i, 1)
                xPos = DtFirstDate.DayOfYear * xFactor
                oRectangle = New Rectangle(xPos, 0, Date.DaysInMonth(CurrentYea, i) * xFactor, iHeight)
                oGraphics.FillRectangle(Brushes.WhiteSmoke, oRectangle)
            End If
        Next


        If mDs Is Nothing Then LoadData()
        Dim oTb As DataTable = mDs.Tables(0)


        Dim iMaxVal As Integer = 0
        Dim oRow As DataRow
        Dim iFirstRow As Integer = -1
        Dim iLastRow As Integer = -1
        Dim iCurrentYea As Integer = CurrentYea()
        Dim iYea As Integer
        For i = 0 To oTb.Rows.Count - 1
            oRow = oTb.Rows(i)
            iYea = CDate(oRow(Cols.Fch)).Year
            Select Case iYea
                Case Is < CurrentYea()
                Case CurrentYea()
                    If iFirstRow = -1 Then
                        iFirstRow = i
                    End If
                Case Is > CurrentYea()
                    iLastRow = i - 1
                    Exit For
            End Select

            If oRow(Cols.stk) > iMaxVal Then iMaxVal = oRow(Cols.stk)
            If oRow(Cols.pn2x) > iMaxVal Then iMaxVal = oRow(Cols.pn2x)
            If oRow(Cols.pn1x) > iMaxVal Then iMaxVal = oRow(Cols.pn1x)
        Next

        If iFirstRow = -1 Then
            iFirstRow = 0
        End If
        If iLastRow = -1 Then
            iLastRow = i - 1
        End If

        If iMaxVal <> 0 Then
            yFactor = iHeight / iMaxVal
        End If

        Dim oArrayStock(iLastRow - iFirstRow) As Point
        Dim oArrayPn2(iLastRow - iFirstRow) As Point
        Dim oArrayPn1(iLastRow - iFirstRow) As Point

        Dim oPoint As Point
        For i = iFirstRow To iLastRow
            oRow = oTb.Rows(i)
            xPos = xFactor * CDate(oRow(Cols.Fch)).DayOfYear
            yPos = iHeight - yFactor * oRow(Cols.stk)
            oPoint = New Point(xPos, yPos)
            oArrayStock(i - iFirstRow) = oPoint

            yPos = iHeight - yFactor * oRow(Cols.pn2x)
            oPoint = New Point(xPos, yPos)
            oArrayPn2(i - iFirstRow) = oPoint

            yPos = iHeight - yFactor * oRow(Cols.pn1x)
            oPoint = New Point(xPos, yPos)
            oArrayPn1(i - iFirstRow) = oPoint

            oGraphics.FillRectangle(Brushes.LightBlue, xPos, iHeight - yFactor * oRow(Cols.pn2), xFactor * oRow(Cols.dias), yFactor * oRow(Cols.pn2))
        Next

        Dim oPath As New Drawing2D.GraphicsPath
        If oArrayStock.Length > 0 Then
            oPath.AddLines(oArrayStock)
            Dim oPenStocks As New Pen(Color.Red, 2)
            oGraphics.DrawPath(oPenStocks, oPath)
        End If

        If oArrayPn2.Length > 0 Then
            oPath = New Drawing2D.GraphicsPath
            oPath.AddLines(oArrayPn2)
            Dim oPenPn2 As New Pen(Color.RoyalBlue, 2)
            oGraphics.DrawPath(oPenPn2, oPath)
        End If

        If oArrayPn1.Length > 0 Then
            oPath = New Drawing2D.GraphicsPath
            oPath.AddLines(oArrayPn1)
            Dim oPenPn1 As New Pen(Color.Green, 2)
            oGraphics.DrawPath(oPenPn1, oPath)
        End If

        Return oBitmap
    End Function

    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        LoadData()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        'If mAllowEvents Then
        Select Case TabControl1.SelectedTab.Text
            Case TabPageGrafica.Text
                Static BlLoadedGr As Boolean
                If BlLoadedGr Then Exit Sub
                'PictureBoxGrafica.Image = GetGrafica()
                BlLoadedGr = True
            Case TabPageData.Text
                Static BlLoadedData As Boolean
                If BlLoadedData Then Exit Sub
                LoadGrid()
                BlLoadedData = True
        End Select
        'End If

    End Sub


    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        MatExcel.GetExcelFromDataset(mDs).Visible = True
    End Sub
End Class