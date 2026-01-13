
Imports Microsoft.Office.Interop

Public Class Frm_Stat_Geo_Mes
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mGeo As New Geo(mEmp)
    Private mTpa As Tpa
    Private mStp As Stp
    Private mArt As Art
    Private mDs As DataSet

    Private mAllowEvents As Boolean

    Private Enum Cols
        CliId
        Pais
        ZonaNom
        CitNom
        CliNom
        Qty
        Eur
        Mes
    End Enum

    Private Sub Frm_Stat_Geo_Mes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        LoadYeas()
        LoadGrid()
        Me.Cursor = Cursors.Default
    End Sub

    Public WriteOnly Property Tpa() As Tpa
        Set(ByVal Value As Tpa)
            If Not Value Is Nothing Then
                mTpa = Value
                mGeo.Tpa = mTpa
                Me.Text = mTpa.Nom
            End If
        End Set
    End Property

    Public WriteOnly Property Stp() As Stp
        Set(ByVal Value As Stp)
            If Not Value Is Nothing Then
                mStp = Value
                mTpa = mStp.Tpa
                mGeo.Stp = mStp
                Me.Text = mTpa.Nom & " / " & mStp.Nom
            End If
        End Set
    End Property


    Public WriteOnly Property Art() As Art
        Set(ByVal Value As Art)
            If Not Value Is Nothing Then
                mArt = Value
                mStp = mArt.Stp
                mTpa = mStp.Tpa
                mGeo.Art = mArt
                Me.Text = mArt.Nom_ESP
            End If
        End Set
    End Property

    Private Sub LoadGrid()
        Dim oLevel As Geo.Levels = ToolStripComboBoxLevel.SelectedIndex + 1

        mGeo.Yea = CurrentYea()
        mDs = mGeo.DataSet
        Dim oTb As DataTable = mDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            With .Columns(Cols.Pais)
                .Visible = False
            End With

            With .Columns(Cols.ZonaNom)
                .HeaderText = "Zona"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            Select Case oLevel
                Case Geo.Levels.Cli, Geo.Levels.Cit, Geo.Levels.NotSet
                    With .Columns(Cols.CitNom)
                        .HeaderText = "Poblacio"
                        .Width = 120
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                        .Visible = True
                    End With
                Case Else
                    With .Columns(Cols.CitNom)
                        .Visible = False
                    End With
            End Select

            With .Columns(Cols.CliId)
                .Visible = False
            End With

            Select Case oLevel
                Case Geo.Levels.Cli, Geo.Levels.NotSet
                    With .Columns(Cols.CliNom)
                        .HeaderText = "Client"
                        .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                        .Visible = True
                    End With
                Case Else
                    With .Columns(Cols.CliNom)
                        .Visible = False
                    End With
            End Select

            With .Columns(Cols.Qty)
                .HeaderText = "unitats"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,##0.00 €"
            End With

            Dim mes As Integer
            For mes = 1 To 12
                With .Columns(Cols.Mes + mes - 1)
                    .HeaderText = maxisrvr.mes(mes, BLL.BLLApp.Lang)
                    .Width = 32
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#,###"
                End With
            Next
        End With
        mAllowEvents = True
    End Sub

    Private Function CurrentYea() As Integer
        Return CInt(ComboBoxYea.Text)
    End Function

    Private Sub LoadYeas()
        Dim oDs As DataSet = mGeo.YeaDs
        With ComboBoxYea
            .DataSource = oDs.Tables(0)
            .ValueMember = "YEA"
            .DisplayMember = "YEA"
            If oDs.Tables(0).Rows.Count > 0 Then
                .SelectedIndex = 0
            End If
        End With
    End Sub

    Private Sub ComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            LoadGrid()
        End If
    End Sub

    Private Sub MenuItemExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'MatExcel.GetExcelFromDataset(mDs).Visible = True
        DoExcel()
    End Sub


    Private Function CurrentContact() As Contact
        Dim CliId As Long = DataGridView1.CurrentRow.Cells(Cols.CliId).Value
        Return MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, CliId)
    End Function

    Private Sub ZoomToStps(ByVal sender As Object, ByVal e As System.EventArgs)
        'root.ShowStatArtsMes(CurrentYea, New Client(CurrentContact))
    End Sub

    Private Sub ZonAdrs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sKey As String = DataGridView1.CurrentRow.Cells(Cols.ZonaNom).Value
        Dim oContact As Contact
        Dim oRow As DataRow
        Dim s As String = ""
        For Each oRow In mDs.Tables(0).Rows
            If Not IsDBNull(oRow(Cols.ZonaNom)) Then

                If oRow(Cols.ZonaNom) = sKey Then
                    oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, oRow(Cols.CliId))
                    s = s & oContact.AdrFullAscii(True) & vbCrLf
                End If
            End If
        Next
        Clipboard.SetDataObject(s, True)
        MsgBox("adreçes copiades a portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub CitAdrs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sKey As String = DataGridView1.CurrentRow.Cells(Cols.CitNom).Value
        Dim oContact As Contact
        Dim oRow As DataRow
        Dim s As String = ""
        For Each oRow In mDs.Tables(0).Rows
            If oRow(Cols.CitNom) = sKey Then
                oContact = MaxiSrvr.Contact.FromNum(BLL.BLLApp.Emp, oRow(Cols.CliId))
                s = s & oContact.AdrFullAscii(True) & vbCrLf
            End If
        Next
        Clipboard.SetDataObject(s, True)
        MsgBox("adreçes copiades a portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem

        oMenuItem = New ToolStripMenuItem
        With oMenuItem
            .Text = "Excel"
            .Image = My.Resources.Excel
        End With
        AddHandler oMenuItem.Click, AddressOf MenuItemExcel_Click
        oContextMenu.Items.Add(oMenuItem)

        If mGeo.PerGeo Then
            Select Case DataGridView1.CurrentCell.ColumnIndex
                Case Cols.ZonaNom
                    oMenuItem = New ToolStripMenuItem
                    With oMenuItem
                        .Text = "adreçes"
                        '.Image = My.Resources.Excel
                    End With
                    AddHandler oMenuItem.Click, AddressOf ZonAdrs
                    oContextMenu.Items.Add(oMenuItem)
                Case Cols.CitNom
                    oMenuItem = New ToolStripMenuItem
                    With oMenuItem
                        .Text = "adreçes"
                    End With
                    AddHandler oMenuItem.Click, AddressOf CitAdrs
                    oContextMenu.Items.Add(oMenuItem)
                Case Cols.CliNom
                    oMenuItem = New ToolStripMenuItem
                    With oMenuItem
                        .Text = "detall"
                    End With
                    AddHandler oMenuItem.Click, AddressOf ZoomToStps
                    oContextMenu.Items.Add(oMenuItem)

                    oMenuItem = New ToolStripMenuItem
                    With oMenuItem
                        .Text = "contacte..."
                        Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, DataGridView1.CurrentRow.Cells(Cols.CliId).Value)
                        Dim oMenuContact As New Menu_Contact(oContact)
                        .DropDownItems.AddRange(oMenuContact.Range)
                    End With
                    oContextMenu.Items.Add(oMenuItem)
                    'omenuitem.DropDownItems.AddRange()
            End Select
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Dim oCell As DataGridViewCell = DataGridView1.CurrentCell
        If oCell Is Nothing Then
            If DataGridView1.Rows.Count > 0 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(0).Cells(Cols.ZonaNom)
                SetContextMenu()
            End If
        Else
            SetContextMenu()
        End If
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        DoExcel()
    End Sub

    Private Sub DoExcel()
        Cursor = Cursors.WaitCursor
        Dim oTb As DataTable = mDs.Tables(0)
        ProgressBar1.Visible = True
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = oTb.Rows.Count

        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        With oSheet
            .Cells.Font.Size = 9
        End With

        Dim i As Integer = 1
        Dim j As Integer

        oSheet.Cells(i, 1) = "Id"
        oSheet.Cells(i, 2) = "pais"
        oSheet.Cells(i, 3) = "zona"
        oSheet.Cells(i, 4) = "población"
        oSheet.Cells(i, 5) = "cliente"
        oSheet.Cells(i, 6) = "Uds"
        oSheet.Cells(i, 7) = "Eur"
        For j = 1 To 12
            oSheet.Cells(i, j + 7) = BLL.BLLApp.Lang.MesAbr(j)
        Next

        oSheet.Columns("A:A").EntireColumn.Hidden = True
        oSheet.Columns("G:G").NumberFormat = "#,##0.00;-#,##0.00;#"

        i = i + 1
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            i = i + 1
            For j = 0 To oTb.Columns.Count - 1
                oSheet.Cells(i, j + 1) = oRow(j)
            Next
            ProgressBar1.Increment(1)
        Next

        Cursor = Cursors.Default
        ProgressBar1.Visible = False
        oApp.Visible = True

    End Sub

    Private Sub ToolStripComboBoxLevel_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ToolStripComboBoxLevel.SelectedIndexChanged
        If mAllowEvents Then
            Dim oLevel As Geo.Levels = ToolStripComboBoxLevel.SelectedIndex + 1
            mGeo.Level = oLevel
            LoadGrid()
        End If
    End Sub
End Class