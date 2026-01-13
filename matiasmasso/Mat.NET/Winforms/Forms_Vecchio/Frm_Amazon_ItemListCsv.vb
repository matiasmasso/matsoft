

Public Class Frm_Amazon_ItemListCsv
    Private _Items As Amazon_ItemListCsvs
    Private _AllowEvents As Boolean


    Public Sub New(oItems As Amazon_ItemListCsvs)
        MyBase.New()
        Me.InitializeComponent()
        _Items = oItems

        Dim oReadOnlyBackColor As Color = Color.FromArgb(235, 235, 235)

        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = _Items
            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            '.MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Amazon_ItemListCsv.Fields.Asin)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .ReadOnly = True
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = oReadOnlyBackColor
            End With
            With .Columns(Amazon_ItemListCsv.Fields.VendorId)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 60
                .ReadOnly = True
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = oReadOnlyBackColor
            End With
            With .Columns(Amazon_ItemListCsv.Fields.Nombre)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 350
                .ReadOnly = True
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.BackColor = oReadOnlyBackColor
            End With
            With .Columns(Amazon_ItemListCsv.Fields.SKU)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .ReadOnly = True
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.BackColor = oReadOnlyBackColor
            End With
            With .Columns(Amazon_ItemListCsv.Fields.Unidades)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .ReadOnly = True
                .DefaultCellStyle.Format = "#,###"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.BackColor = oReadOnlyBackColor
            End With
            With .Columns(Amazon_ItemListCsv.Fields.Disponibles)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,###"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Amazon_ItemListCsv.Fields.Obsoleto)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Amazon_ItemListCsv.Fields.Comentarios)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            End With
        End With

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = ".csv"
            .FileName = "itemlist.csv"
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "guardar fitxer de sol·licitut disponibilitat de Amazon"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                'For i As Integer = 0 To _Items.Count - 1
                ' Dim oItem As Amazon_ItemListCsv = _Items(i)
                ' Next
                _Items.Save(.FileName)
                Me.Close()
            End If
        End With
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Function CurrentSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim ArtId As Integer = oRow.Cells(Amazon_ItemListCsv.Fields.SKU).Value
            retval = BLLProductSku.FromId(ArtId)
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oSku As DTOProductSku = CurrentSku()

        If oSku IsNot Nothing Then
            Dim oMenu_Sku As New Menu_ProductSku(oSku)
            oContextMenu.Items.AddRange(oMenu_Sku.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub
End Class