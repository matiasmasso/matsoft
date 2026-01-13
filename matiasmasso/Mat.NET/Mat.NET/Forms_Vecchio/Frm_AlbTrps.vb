

Public Class Frm_AlbTrps

    Private mDs As DataSet
    Private mZona As Zona
    Private mTrpZons As TrpZons

    Private Enum Cols
        Eur
        EurComp
        Kg
        TrpZon
        TrpNom
        TrpGuid
        Obsoleto
    End Enum

    Public WriteOnly Property Alb() As Alb
        Set(ByVal Value As Alb)
            Dim oAlb As Alb = Value
            mZona = oAlb.Zip.Location.Zona
            TextBoxM3.Text = oAlb.Itms.M3
            SetZona(mZona)
            mTrpZons = mZona.TrpZons(BLL.BLLApp.Emp, False)
            mDs = New DataSet
            mDs.Tables.Add(CreateDataSource())
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim oTrpZon As TrpZon
        For Each oTrpZon In mTrpZons
            AddRow(oTrpZon)
        Next
        Dim oTb As DataTable = mDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Eur)
                .HeaderText = "Cost"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.EurComp)
                .HeaderText = "Compara"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Kg)
                .HeaderText = "Kg cubic"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.TrpZon)
                .HeaderText = "modul"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.TrpNom)
                .HeaderText = "Transportista"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.TrpGuid)
                .Visible = False
            End With
            With .Columns(Cols.Obsoleto)
                .Visible = False
            End With
        End With
    End Sub

    Private Sub AddRow(ByVal oTrpZon As TrpZon)
        Dim SngM3 As Decimal = TextBoxM3.Text
        With oTrpZon
            Dim oCost As maxisrvr.Amt = .Cost(SngM3)
            If Not oCost Is Nothing Then
                Dim oTb As DataTable = mDs.Tables(0)
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow(Cols.Eur) = oCost.Eur
                oRow(Cols.EurComp) = oCost.Eur * 100 / (100 - .Transportista.CompensaPercent)
                oRow(Cols.Kg) = .KgCubicats(SngM3)
                oRow(Cols.TrpZon) = .Id
                oRow(Cols.TrpNom) = .Transportista.Abr
                oRow(Cols.TrpGuid) = .Transportista.Guid
                oRow(Cols.Obsoleto) = IIf(.Transportista.Activat, 0, 1)
            End If
        End With
    End Sub

    Private Sub SetZona(ByVal oZona As Zona)
        If ComboBoxZonas.Items.Count = 0 Then
            Xl_Pais1.Country = New DTOCountry(oZona.Country.Guid)
            LoadZonas(Xl_Pais1.Country)
            ComboBoxZonas.SelectedValue = oZona.Guid
        End If
    End Sub

    Private Sub LoadZonas(ByVal oCountry As DTOCountry)
        Dim SQL As String = "SELECT Guid, Nom FROM Zona WHERE " _
        & "Country = '" & oCountry.Guid.ToString & "' " _
        & "ORDER BY Nom"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        With ComboBoxZonas
            .DataSource = oDs.Tables(0)
            .ValueMember = "Guid"
            .DisplayMember = "Nom"
        End With
    End Sub

    Private Function CreateDataSource() As DataTable
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("EUR", System.Type.GetType("System.Decimal"))
            .Add("EURCOMP", System.Type.GetType("System.Decimal"))
            .Add("TRPKG", System.Type.GetType("System.Int32"))
            .Add("TRPZON", System.Type.GetType("System.Int32"))
            .Add("TRPNOM", System.Type.GetType("System.String"))
            .Add("OBSOLETO", System.Type.GetType("System.Int32"))
            .Add("TrpGuid", System.Type.GetType("System.Guid"))
        End With
        Return oTb
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oRow As DataGridViewRow = DataGridView1.Rows(DataGridView1.CurrentRow.Index)
        Dim oTrpGuid As Guid = oRow.Cells(Cols.TrpGuid).Value
        Dim TrpZonId As Integer = oRow.Cells(Cols.TrpZon).Value
        Dim oTrp As New Transportista(oTrpGuid)
        Dim oTrpZon As New TrpZon(oTrp, TrpZonId)

        Dim oFrm As New Frm_Trp_Tarifa(oTrpZon)
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlObsoleto As Boolean = (oRow.Cells(Cols.Obsoleto).Value = 1)
        If BlObsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

End Class
