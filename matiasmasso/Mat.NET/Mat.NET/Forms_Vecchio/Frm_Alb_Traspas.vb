

Public Class Frm_Alb_Traspas
    Private mAlb As Alb
    Private mAllowEvents As Boolean

    Private Enum Cols
        Qty
        ArtNum
        ArtNom
    End Enum

    Public WriteOnly Property Alb() As alb
        Set(ByVal value As alb)
            mAlb = value
            Me.Text = "ALB." & mAlb.Id & " DE TRASPAS DE MAGATZEM"
            LabelMgzFrom.Text = "de " & mAlb.Client.Nom
            LabelMgzTo.Text = "a " & mAlb.Mgz.Nom
            LabelFch.Text = mAlb.Fch.ToShortDateString
            LabelObs.Text = "Observaciones: " & mAlb.Obs
            LoadGrid()
            mAllowEvents = True
        End Set
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT QTY,ARC.ART,MYD FROM ARC INNER JOIN " _
        & "ART ON ARC.ArtGuid=ART.Guid " _
        & "WHERE ARC.AlbGuid=@Guid " _
        & "ARC.COD>50 " _
        & "ORDER BY LIN"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi, "@Guid", mAlb.Guid.ToString)
        Dim oTb As DataTable = oDs.Tables(0)
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

            With .Columns(Cols.Qty)
                .HeaderText = "Cant."
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.ArtNum)
                .Visible = False
            End With
            With .Columns(Cols.ArtNom)
                .HeaderText = "Article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = oRow.Cells(Cols.ArtNum).Value
            oArt = MaxiSrvr.Art.FromNum(BLL.BLLApp.Emp, LngId)
        End If
        Return oArt
    End Function

 

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oArt As Art = CurrentArt()

        If oArt Is Nothing Then
            Dim oMenu_Art As New Menu_Art(oArt)
            'AddHandler oMenu_Art.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Art.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If Not mallowevents Then
            SetContextMenu()
        End If
    End Sub
End Class