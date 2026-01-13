

Public Class Frm_ArtXStp_Repeticions
    Private mTpa As Tpa
    Private mStp As Stp
    'Private mCategoria As Categoria
    Private mArt As Art
    Private mEmp as DTOEmp
    Private mProduct As Product
    Private mDsHdr As DataSet
    Private mDsDtl As DataSet
    Private mSumClis As Integer
    Private mSumPdcs As Integer
    Private mSumUnits As Integer
    Private mSumImport As Decimal
    Private mAllowEvents As Boolean

    Private Enum ColsHdr
        pdcs
        clis
        quota
        units
        eur
    End Enum

    Private Enum ColsDtl
        guid
        pdcs
        units
        eur
        clx
    End Enum

    Public Sub New(ByVal oProduct As Product)
        MyBase.New()
        Me.InitializeComponent()
        mProduct = oProduct
        Select Case mProduct.ValueType
            Case Product.ValueTypes.Tpa
                mTpa = CType(mProduct.Value, Tpa)
            Case Product.ValueTypes.Stp
                mStp = CType(mProduct.Value, Stp)
                mTpa = mStp.Tpa
                'Case Product.ValueTypes.Categoria
                'mCategoria = CType(mProduct.Value, Categoria)
                'mStp = mCategoria.Stp
                'mTpa = mCategoria.Stp.Tpa
            Case Product.ValueTypes.Art
                mArt = CType(mProduct.Value, Art)
                'mCategoria = mArt.Categoria
                mStp = mArt.Stp
                mTpa = mStp.Tpa
        End Select
        mEmp = mTpa.emp
        refresca()
    End Sub

    Public WriteOnly Property Stp() As Stp
        Set(ByVal Value As Stp)
            mStp = Value
            mEmp = mStp.Tpa.emp
            refresca()
        End Set
    End Property

    Private Sub refresca()
        Cursor = Cursors.WaitCursor
        mAllowEvents = False
        LoadGridHdr()
        If mDsHdr.Tables(0).Rows.Count > 0 Then
            LoadGridDtl()
        End If
        Cursor = Cursors.Default
        mAllowEvents = True
    End Sub

    Private Sub LoadGridHdr()
        Dim SQL As String = "SELECT PDCS, COUNT(PDCS) AS CLIENTS, '' AS CUOTA, SUM(UNITS) AS UNITS, SUM(IMPORT) AS EUR FROM " _
        & "(SELECT  COUNT(DISTINCT Pdc.pdc) AS PDCS, SUM(PNC.qty) AS UNITS,SUM(PNC.QTY*PNC.EUR*(100-PNC.DTO)/100) AS IMPORT " _
        & "FROM ART INNER JOIN " _
        & "STP ON ART.Category = Stp.Guid INNER JOIN " _
        & "PNC ON ART.Guid=Pnc.ArtGuid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid " _
        & "WHERE ART.EMP=" & mEmp.Id & " AND STP.CODI=0 "

        If mArt IsNot Nothing Then
            SQL = SQL & " AND ART.Guid = '" & mArt.Guid.ToString & "' "
        End If
        'If mCategoria IsNot Nothing Then
        ' SQL = SQL & " AND ART.CTG = " & mCategoria.Id & " "
        ' End If
        If mStp IsNot Nothing Then
            SQL = SQL & " AND ART.Category = '" & mStp.Guid.ToString & "' "
        End If
        If mTpa IsNot Nothing Then
            SQL = SQL & " AND Stp.Brand = '" & mTpa.Guid.ToString & "' "
        End If


        SQL = SQL & " AND PDC.cod = 2 " _
        & "GROUP BY PDC.Guid) DERIVEDTBL " _
        & "GROUP BY PDCS " _
        & "ORDER BY PDCS DESC"
        mDsHdr = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsHdr.Tables(0)
        Dim oRow As DataRow
        mSumPdcs = 0
        mSumClis = 0
        mSumUnits = 0
        For Each oRow In oTb.Rows
            mSumPdcs += oRow("PDCS")
            mSumClis += oRow("CLIENTS")
            mSumUnits += oRow("UNITS")
            mSumImport += oRow("EUR")
        Next

        oRow = oTb.NewRow
        oRow("PDCS") = 0
        oRow("CLIENTS") = mSumClis
        oRow("UNITS") = mSumUnits
        oRow("EUR") = mSumImport
        oTb.Rows.InsertAt(oRow, 0)

        For Each oRow In oTb.Rows
            oRow("CUOTA") = Math.Round(100 * oRow("CLIENTS") / mSumClis, 0) & "%"
        Next

        With DataGridViewHdr
            With .RowTemplate
                .Height = DataGridViewHdr.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False


            With .Columns(ColsHdr.pdcs)
                .HeaderText = "comandes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.clis)
                .HeaderText = "clients"
                '.FooterText = mSumClis
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.quota)
                .HeaderText = "quota"
                .Width = 60
                '.FooterText = "100%"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.units)
                .HeaderText = "unitats"
                ' .FooterText = mSumUnits
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsHdr.eur)
                .HeaderText = "import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Sub DataGridViewHdr_RowPrePaint(sender As Object, e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewHdr.RowPrePaint
        If e.RowIndex = 0 Then
            DataGridViewHdr.Rows(0).DefaultCellStyle.BackColor = Color.LightBlue
        End If
    End Sub

    Private Function CurrentRepeticions() As Integer
        Dim iRepeticions As Integer = 0
        Dim oRow As DataGridViewRow = DataGridViewHdr.CurrentRow
        If oRow IsNot Nothing Then
            iRepeticions = oRow.Cells(ColsHdr.pdcs).Value
        End If
        Return iRepeticions
    End Function


    Private Sub LoadGridDtl()
        Dim SQL As String = "SELECT pdc.CliGuid, COUNT(DISTINCT Pdc.Guid) AS PDCS, SUM(PNC.qty) AS UNITS, SUM(PNC.QTY*PNC.EUR*(100-PNC.DTO)/100) AS EUR, CLX.clx " _
        & "FROM ART INNER JOIN " _
        & "STP ON ART.Category = Stp.Guid INNER JOIN " _
        & "PNC ON ART.Guid = PNC.ArtGuid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "CLX ON PDC.CliGuid = CLX.Guid " _
        & "WHERE ART.EMP=" & mEmp.Id & " "

        If mArt IsNot Nothing Then
            SQL = SQL & " AND ART.Guid = '" & mArt.Guid.ToString & "' "
        End If
        'If mCategoria IsNot Nothing Then
        ' SQL = SQL & " AND ART.CTG = " & mCategoria.Id & " "
        ' End If
        If mStp IsNot Nothing Then
            SQL = SQL & " AND ART.Category = '" & mStp.Guid.ToString & "' "
        End If
        If mTpa IsNot Nothing Then
            SQL = SQL & " AND Stp.Brand = '" & mTpa.Guid.ToString & "' "
        End If


        SQL = SQL & " AND PDC.cod = 2 " _
        & "GROUP BY PDC.CliGuid, CLX.CLX " _
        & "HAVING (COUNT(DISTINCT Pdc.Guid)=" & CurrentRepeticions() & ") " _
        & "ORDER BY SUM(PNC.qty) DESC"

        mDsDtl = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsDtl.Tables(0)
        With DataGridViewDtl
            With .RowTemplate
                .Height = DataGridViewDtl.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColsDtl.guid)
                .Visible = False
            End With
            With .Columns(ColsDtl.pdcs)
                .HeaderText = "comandes"
                '.FooterText = .Columns(ColsHdr.pdcs).Value * .Columns(ColsHdr.clis).Value
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.units)
                .HeaderText = "units"
                '.FooterText = .Columns(ColsHdr.units).Value
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.eur)
                .HeaderText = "import"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsDtl.clx)
                .HeaderText = "clients"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        End With
        SetContextMenuDtl()
    End Sub



    Private Sub DataGridViewHdr_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewHdr.SelectionChanged
        If mAllowEvents Then
            Cursor = Cursors.WaitCursor
            LoadGridDtl()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub DataGridViewDtl_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewDtl.SelectionChanged
        If mAllowEvents Then
            SetContextMenuDtl()
        End If
    End Sub


    Private Sub SetContextMenuDtl()
        Dim oGrid As DataGridView = DataGridViewDtl
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            Dim oContact As New DTOContact(oRow.Cells(ColsDtl.guid).Value)
            Dim oMenu As New Menu_Contact(oContact)

            oContextMenuStrip.Items.AddRange(oMenu.Range)
        End If

        oGrid.ContextMenuStrip = oContextMenuStrip
    End Sub

End Class
