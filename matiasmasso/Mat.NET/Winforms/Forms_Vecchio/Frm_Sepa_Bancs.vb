

Public Class Frm_Sepa_Bancs
    Private mAllowEvents As Boolean

    Private Enum Cols
        Pais
        Bank
        Adr
        Cit
        Bic
        FchCreated
        FchDeleted
        Bn1
        Bn2
    End Enum

    Private Sub Frm_Sepa_Bancs_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LoadGrid()
        mAllowEvents = True
    End Sub


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Country, ParticipantName, Address, City, Bic, ReadinessDate, SchemeLeavingDate, BN1.Swift, BN2.Swift " _
                          & "FROM Sepa_Bancs LEFT OUTER JOIN " _
                          & "BN1 ON BN1.Swift LIKE Sepa_Bancs.Bic LEFT OUTER JOIN " _
                          & "BN2 ON BN2.Swift LIKE Sepa_Bancs.Bic " _
                          & "ORDER BY Country,Bic"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            'Dim oColumnPais As DataGridViewColumn = DataGridView1.Columns(Cols.Pais)
            'Dim oColumnBanc As DataGridViewColumn = DataGridView1.Columns(Cols.Bank)
            '.Sort(oColumnBanc)
            '.Sort(oColumnPais)

            With .Columns(Cols.Pais)
                .HeaderText = "pais"
                .Width = 100
            End With
            With .Columns(Cols.Bank)
                .HeaderText = "entitat"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Adr)
                .HeaderText = "adreça"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Bic)
                .HeaderText = "BIC"
                .Width = 100
            End With
            With .Columns(Cols.FchCreated)
                .HeaderText = "alta"
                .Width = 100
            End With
            With .Columns(Cols.FchDeleted)
                .HeaderText = "baixa"
                .Visible = False
            End With
            With .Columns(Cols.Bn1)
                .Visible = False
            End With
            With .Columns(Cols.Bn2)
                .Visible = False
            End With

        End With

        mAllowEvents = True
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Bic
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If IsDBNull(oRow.Cells(Cols.Bn1).Value) And IsDBNull(oRow.Cells(Cols.Bn2).Value) Then
                Else
                    e.CellStyle.BackColor = Color.LightBlue
                End If
        End Select
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        Dim oSepaBanc As SEPA_Bank = CurrentItm()
        If oSepaBanc IsNot Nothing Then
            Dim oBank As DTOBank = BLL.BLLBank.FromSwift(oSepaBanc.Bic.Value)

            oMenuItem = New ToolStripMenuItem("zoom", My.Resources.prismatics, AddressOf Do_Zoom)
            oMenuItem.Enabled = oBank IsNot Nothing
            oContextMenu.Items.Add(oMenuItem)

            oMenuItem = New ToolStripMenuItem("asignar a entitat bancaria", My.Resources.prismatics, AddressOf Do_SetBank)
            oMenuItem.Enabled = oBank Is Nothing
            oContextMenu.Items.Add(oMenuItem)

            oContextMenu.Items.Add("_")
        End If

        oMenuItem = New ToolStripMenuItem("importar fitxer de bancs SEPA B2B", My.Resources.prismatics, AddressOf Do_Importar_bancs)
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Function CurrentItm() As SEPA_Bank
        Dim retval As SEPA_Bank = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim sBic As String = oRow.Cells(Cols.Bic).Value
            Dim oBic As New SwiftBic(sBic)
            retval = New SEPA_Bank(oBic)
        End If
        Return retval
    End Function

    Private Sub Do_Zoom()
        Dim oBic As SwiftBic = CurrentItm.Bic
        Dim oBank As DTOBank = BLL.BLLBank.FromSwift(oBic.Value)
        Dim oFrm As New Frm_Bank(oBank)
        oFrm.Show()
    End Sub

    Private Sub Do_SetBank()
        Dim oBic As SwiftBic = CurrentItm.Bic
        Dim oBank As DTOBank = BLL.BLLBank.FromSwift(oBic.Value)
        Dim oFrm As New Frm_Banks(oBank.Country, Frm_Banks.Modes.SelectBank)
        AddHandler oFrm.OnItemSelected, AddressOf onBankSet
        oFrm.Show()
    End Sub

    Private Sub onBankSet(sender As Object, e As MatEventArgs)
        Dim oBank As DTOBank = e.Argument
        Dim oBic As SwiftBic = CurrentItm.Bic
        oBank.Swift = oBic.Value
        Dim exs as New List(Of exception)
        If BLL.BLLBank.Update(oBank, exs) Then
            RefreshRequest(sender, e)
        Else
            UIHelper.WarnError( exs, "error al desar el swift al banc")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Bic

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub Do_Importar_bancs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "IMPORTACIO DE TEXTES MANDATO SEPA"
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .DefaultExt = ".csv"
            .Multiselect = False
            If .ShowDialog = DialogResult.OK Then
                Me.Cursor = Cursors.WaitCursor

                Dim oFF As New FF(DTO.DTOFlatFile.ids.FFcolonSplitted, .FileName)
                ProgressBar1.Maximum = oFF.LineCount
                ProgressBar1.Visible = True
                Application.DoEvents()

                Dim SQL As String = "DELETE Sepa_Bancs"
                maxisrvr.executenonquery(SQL, maxisrvr.Databases.Maxi)

                SQL = "SELECT Country, ParticipantName, Address, City, Bic, ReadinessDate, SchemeLeavingDate FROM Sepa_Bancs"
                Dim oConn As SqlClient.SqlConnection = maxisrvr.GetSQLConnection(maxisrvr.Databases.Maxi)
                Dim oDa As SqlClient.SqlDataAdapter = maxisrvr.GetSQLDataAdapter(SQL, oConn)
                Dim oDs As New DataSet
                oDa.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                Dim oRow As DataRow

                For i As Integer = 1 To oFF.LineCount - 1
                    oRow = oTb.NewRow
                    Dim oValues As ArrayList = oFF.GetRegisterFieldValues(i)
                    For j As Integer = 0 To Cols.FchDeleted
                        If oValues(j) > "" Then
                            oRow(j) = oValues(j)
                        End If
                    Next
                    oTb.Rows.Add(oRow)
                    ProgressBar1.Increment(1)
                    Application.DoEvents()
                Next

                oDa.Update(oDs)
                oConn.Close()
                RefreshRequest(sender, e)

                Me.Cursor = Cursors.Default
                ProgressBar1.Visible = False
            End If
        End With
    End Sub

End Class