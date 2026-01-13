

Public Class Frm_RepBugs

    Private mRep As rep

    Private Enum ColPncs
        Yea
        Pdc
        Lin
        Fch
        Nom
        Qty
        Myd
    End Enum

    Public WriteOnly Property Rep() As Rep
        Set(ByVal value As Rep)
            mRep = value
            Me.Text = "CUADRAR " & mRep.Abr
            LoadGridPnc()
        End Set
    End Property

    Private Sub LoadGridPnc()
        Dim SQL As String = "SELECT Pdc.YEA, Pdc.PDC, PNC.LIN, " _
        & "PDC.fch, Cli_Geo3.RaoSocial,PNC.QTY,ART.myD " _
        & "FROM RepZon INNER JOIN " _
        & "ART ON RepZon.emp = ART.emp AND RepZon.tpa = ART.tpa INNER JOIN " _
        & "PNC ON ART.Guid = PNC.ArtGuid INNER JOIN " _
        & "PDC ON PNC.PdcGuid = PDC.Guid INNER JOIN " _
        & "Cli_Geo3 ON RepZon.emp = Cli_Geo3.emp AND RepZon.zon = Cli_Geo3.ZonId AND PDC.Emp = Cli_Geo3.emp AND PDC.cli = Cli_Geo3.Cli " _
        & "WHERE Pdc.EMP=" & BLLApp.Emp.Id & " AND " _
        & "RepZon.rep =" & mRep.Id & " AND " _
        & "PDC.fch BETWEEN RepZon.FchFrom AND RepZon.FchTo"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridViewPnc
            With .RowTemplate
                .Height = DataGridViewPnc.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = True
            With .Columns(ColPncs.Yea)
                .Visible = False
            End With
            With .Columns(ColPncs.Pdc)
                .Visible = False
            End With
            With .Columns(ColPncs.Lin)
                .Visible = False
            End With
            With .Columns(ColPncs.Fch)
                .HeaderText = "fecha"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColPncs.Nom)
                .HeaderText = "client"
                .Width = 150
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColPncs.Qty)
                .HeaderText = "cant"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColPncs.Myd)
                .HeaderText = "producte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With

        End With

    End Sub


End Class