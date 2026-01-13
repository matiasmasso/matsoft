

Public Class Frm_Art_Promo
    Private mPromo As ArtPromo
    Private mDirtyBigFile As MaxiSrvr.BigFileNew = Nothing
    Private mAllowEvents As Boolean = False
    Private mMgz As Mgz

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        ArtId
        ArtNom
        QtyAmbCarrec
        QtyNoCarrec
        Pvp
        Dto
        Amt
    End Enum

    Public Sub New(ByVal oPromo As ArtPromo)
        MyBase.New()
        Me.InitializeComponent()
        mMgz = New Mgz(BLL.BLLApp.Mgz.Guid)
        mPromo = oPromo
        refresca()
        CalculaTotals()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        With mPromo
            Me.Text = "PROMOCIO " & .Nom
            TextBoxNom.Text = .Nom
            If .FchFrom = Date.MinValue Then
                CheckBoxFchFrom.Checked = False
                DateTimePickerFchFrom.Value = Today
                DateTimePickerFchFrom.Enabled = False
            Else
                CheckBoxFchFrom.Checked = True
                DateTimePickerFchFrom.Value = .FchFrom
                DateTimePickerFchFrom.Enabled = True
            End If

            If .FchTo = Date.MinValue Then
                CheckBoxFchTo.Checked = False
                DateTimePickerFchTo.Value = Today
                DateTimePickerFchTo.Enabled = False
            Else
                CheckBoxFchTo.Checked = True
                DateTimePickerFchTo.Value = .FchTo
                DateTimePickerFchTo.Enabled = True
            End If

            CheckBoxToEndStk.Checked = .ToEndStk
            CheckBoxPortsPagats.Checked = .PortsPagats

            Xl_BigFile1.BigFile = .BigFile.BigFile
        End With
        loadgrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT P.ART, A.MYD, P.QTYAMBCARREC, P.QTYNOCARREC, P.PVP, P.DTO, ROUND(P.QTYAMBCARREC*P.PVP*(100-P.DTO)/100,2) AS AMT " _
        & "FROM ARTPROMOITEMS P INNER JOIN ART A ON P.EMP=A.EMP AND P.ART=A.ART WHERE P.GUID LIKE @GUID ORDER BY P.ORD"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@GUID", mPromo.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim BlOldEvents As Boolean = mAllowEvents
        mAllowEvents = False
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.5 '1.3
            End With
            .DataSource = oTb
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .RowHeadersWidth = 25
            .MultiSelect = False
            .AllowUserToAddRows = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.ArtId)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.QtyAmbCarrec)
                .HeaderText = "amb carrec"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.QtyNoCarrec)
                .HeaderText = "sense carrec"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 40
                .DefaultCellStyle.Format = "#,##0;-#,##0;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Pvp)
                .HeaderText = "preu"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "dte"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Amt)
                .HeaderText = "import"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .ReadOnly = True
            End With
        End With
        mAllowEvents = BlOldEvents

    End Sub


    Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        If mAllowEvents Then
            Dim dgc As DataGridViewCell = TryCast(DataGridView1.Item(e.ColumnIndex, e.RowIndex), DataGridViewCell)

            If dgc IsNot Nothing AndAlso dgc.ReadOnly Then
                SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView1.CellValidating
        If mAllowEvents Then
            Dim oArrayErrs As New ArrayList
            Dim sWarn As String = ""

            Select Case e.ColumnIndex
                Case Cols.QtyAmbCarrec, Cols.QtyNoCarrec
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    sWarn = CheckInteger(e.FormattedValue)
                    If sWarn > "" Then oArrayErrs.Add(sWarn)

                Case Cols.Pvp, Cols.Dto
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    sWarn = CheckDecimal(e.FormattedValue)
                    If sWarn > "" Then oArrayErrs.Add(sWarn)

            End Select

            If oArrayErrs.Count > 0 Then
                Dim sB As New System.Text.StringBuilder
                For Each s As String In oArrayErrs
                    sB.AppendLine(s)
                Next
                MsgBox(sB.ToString, MsgBoxStyle.Exclamation, "MAT.NET")
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                oRow.Cells(e.ColumnIndex).Selected = True
                e.Cancel = True
            End If
        End If
    End Sub


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.QtyAmbCarrec, Cols.Pvp, Cols.Dto
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    CalculaImports(oRow)
                Case Cols.ArtNom
                    Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim BlPreviousAllowEvents As Boolean = mAllowEvents
                    mAllowEvents = False
                    Dim sKey As String = oRow.Cells(Cols.ArtNom).Value
                    Dim oSku As ProductSku = Finder.FindSku(BLL.BLLApp.Mgz, sKey)
                    If oSku IsNot Nothing Then
                        oRow.Cells(Cols.ArtNom).Value = oSku.Nom_Esp
                        Dim oArt As New Art(oSku.Guid)
                        FeedRowFromArt(oRow, oArt)
                        mAllowEvents = BlPreviousAllowEvents
                    End If
            End Select
            SetDirty()
        End If
    End Sub


    Private Sub FeedRowFromArt(ByRef oRow As DataGridViewRow, ByVal oArt As Art)
        Dim iSuggestedQty As Integer = oArt.Dimensions.InnerPack
        If iSuggestedQty = 0 Then iSuggestedQty = 1
        Dim DecPvp As Decimal = oArt.TarifaA.Eur

        oRow.Cells(Cols.ArtId).Value = oArt.Id
        oRow.Cells(Cols.QtyAmbCarrec).Value = iSuggestedQty
        oRow.Cells(Cols.QtyNoCarrec).Value = 0
        oRow.Cells(Cols.Pvp).Value = DecPvp
        oRow.Cells(Cols.Dto).Value = 0
        oRow.Cells(Cols.Amt).Value = iSuggestedQty * DecPvp
    End Sub


    Private Sub CalculaImports(ByVal oRow As DataGridViewRow)
        Dim iQty As Integer = 0
        Dim DcPvp As Decimal = 0
        Dim DcDto As Decimal = 0
        Dim BlCarrec As Boolean = True
        Dim DcAmt As Decimal = 0

        If Not IsDBNull(oRow.Cells(Cols.QtyAmbCarrec).Value) Then iQty = oRow.Cells(Cols.QtyAmbCarrec).Value
        If Not IsDBNull(oRow.Cells(Cols.Pvp).Value) Then DcPvp = oRow.Cells(Cols.Pvp).Value
        If Not IsDBNull(oRow.Cells(Cols.Dto).Value) Then DcDto = oRow.Cells(Cols.Dto).Value

        DcAmt = Math.Round(iQty * DcPvp * (100 - DcDto) / 100, 2)

        oRow.Cells(Cols.Amt).Value = DcAmt
        CalculaTotals()
    End Sub

    Private Sub CalculaTotals()
        Dim DcTot As Decimal = 0
        Dim DcEur As Decimal = 0
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not IsDBNull(oRow.Cells(Cols.Amt).Value) Then
                DcEur = oRow.Cells(Cols.Amt).Value
                DcTot += DcEur
            End If
        Next

        Dim s As String = ""
        If DcTot <> 0 Then
            s = "total " & Format(DcTot, "#,##0.00 €")
        End If

        ToolStripStatusLabelTot.Text = s
    End Sub


    Private Function CheckDecimal(ByVal sSource As String) As String
        Dim sRetVal As String = ""
        If sSource > "" Then
            If Not IsNumeric(sSource) Then
                sRetVal = "nomes s'admeten valors numerics"
            End If
        End If
        Return sRetVal
    End Function

    Private Function CheckInteger(ByVal sSource As String) As String
        Dim sRetVal As String = ""
        If IsNumeric(sSource) Then
            If CDec(sSource) <> CInt(sSource) Then
                sRetVal = "no s'admeten decimals"
            End If
        Else
            If sSource > "" Then
                sRetVal = "nomes s'admeten valors numerics"
            End If
        End If
        Return sRetVal
    End Function

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        Dim oItems As New ArtPromoItems
        Dim oItem As ArtPromoItem = Nothing
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            If Not oRow.IsNewRow Then
                oItem = New ArtPromoItem
                With oItem
                    .Art = MaxiSrvr.Art.FromNum(mPromo.Tpa.emp, oRow.Cells(Cols.ArtId).Value)
                    .QtyAmbCarrec = oRow.Cells(Cols.QtyAmbCarrec).Value
                    .QtyNoCarrec = oRow.Cells(Cols.QtyNoCarrec).Value
                    .Pvp = oRow.Cells(Cols.Pvp).Value
                    .Dto = oRow.Cells(Cols.Dto).Value
                End With
                oItems.Add(oItem)
            End If
        Next

        With mPromo
            .Nom = TextBoxNom.Text
            If CheckBoxFchFrom.Checked Then
                .FchFrom = DateTimePickerFchFrom.Value
            Else
                .FchFrom = Date.MinValue
            End If
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Date.MinValue
            End If

            .ToEndStk = CheckBoxToEndStk.Checked
            .PortsPagats = CheckBoxPortsPagats.Checked
            .Items = oItems

            If mDirtyBigFile IsNot Nothing Then
                .BigFile = New BigFileSrc(DTODocFile.Cods.ArtPromo, .Guid)
                .BigFile.BigFile = mDirtyBigFile
                .Image = mDirtyBigFile.Img
                .Update()
            End If

            If mDirtyBigFile IsNot Nothing Then
                .BigFile = New BigFileSrc(DTODocFile.Cods.ArtPromo, .Guid, mDirtyBigFile)
            End If

            .Update()

        End With

        RaiseEvent AfterUpdate(mPromo, EventArgs.Empty)

        Me.Close()
    End Sub

    Private Sub SetDirty()
        ButtonOk.Enabled = True
    End Sub

    Private Sub CheckBoxFchFrom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFchFrom.Click
        DateTimePickerFchFrom.Enabled = DateTimePickerFchFrom.Checked
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxFchTo.CheckedChanged
        DateTimePickerFchTo.Enabled = DateTimePickerFchTo.Checked
    End Sub

    Private Sub Xl_BigFile1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_BigFile1.AfterUpdate
        mDirtyBigFile = Xl_BigFile1.BigFile
        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxNom.TextChanged, _
     DateTimePickerFchFrom.ValueChanged, _
      DateTimePickerFchTo.ValueChanged, _
       CheckBoxFchFrom.CheckedChanged, _
        CheckBoxFchTo.CheckedChanged, _
         CheckBoxToEndStk.CheckedChanged, _
          CheckBoxPortsPagats.CheckedChanged

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

End Class