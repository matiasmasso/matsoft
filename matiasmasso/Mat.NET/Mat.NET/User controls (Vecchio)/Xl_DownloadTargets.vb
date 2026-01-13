

Public Class Xl_DownloadTargets
    Private mTargets As DownloadTargets

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Private Enum Cols
        TargetGuid
        TargetCod
        Nom
    End Enum

    Public Property Targets As DownloadTargets
        Get
            Return GetTargetsFromGrid
        End Get
        Set(value As DownloadTargets)
            mTargets = value
            LoadGrid()
        End Set
    End Property

    Private Sub LoadGrid()
        Dim oTb As New DataTable
        With oTb.Columns
            .Add("GUID", System.Type.GetType("System.Guid"))
            .Add("TARGET", System.Type.GetType("System.Int32"))
            .Add("TXT", System.Type.GetType("System.String"))
        End With

        Dim oRow As DataRow
        For Each oTarget As DownloadTarget In mTargets
            oRow = oTb.NewRow
            oRow(Cols.TargetGuid) = oTarget.TargetGuid.ToString
            oRow(Cols.TargetCod) = CInt(oTarget.TargetCod)
            oRow(Cols.Nom) = oTarget.TargetText
            oTb.Rows.Add(oRow)
        Next

        With DataGridViewProducts
            .RowTemplate.Height = .Font.Height * 1.3
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowUserToDeleteRows = True
            With .Columns(Cols.TargetGuid)
                .Visible = False
            End With
            With .Columns(Cols.TargetCod)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

    End Sub

    Private Function GetTargetsFromGrid() As DownloadTargets
        Dim oTargets As New DownloadTargets
        For Each oRow As DataGridViewRow In DataGridViewProducts.Rows
            Dim oTargetGuid As Guid = CType(oRow.Cells(Cols.TargetGuid).Value, Guid)
            Dim oTargetCod As DownloadTarget.Cods = CType(oRow.Cells(Cols.TargetCod).Value, DownloadTarget.Cods)
            Dim oTarget As New DownloadTarget(oTargetGuid, oTargetCod)
            oTargets.Add(oTarget)
        Next
        Return oTargets
    End Function

    Private Sub DataGridViewProducts_UserDeletedRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowEventArgs) Handles DataGridViewProducts.UserDeletedRow
        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
    End Sub


    Private Sub ButtonAddProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonAddProduct.Click
        mTargets = GetTargetsFromGrid()
        Dim oProduct As Product = Xl_LookupProduct1.Product
        Dim oTarget As New DownloadTarget(oProduct.Guid, DownloadTarget.Cods.Producte)
        mTargets.Add(oTarget)
        LoadGrid()
        Xl_LookupProduct1.Clear()
        ButtonAddProduct.Enabled = False
        RaiseEvent AfterUpdate(Me, EventArgs.Empty)
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_LookupProduct1.AfterUpdate
        ButtonAddProduct.Enabled = True
    End Sub
End Class
