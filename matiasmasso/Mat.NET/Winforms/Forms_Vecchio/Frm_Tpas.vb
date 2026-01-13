

Public Class Frm_Tpas
    Private mTpa As Tpa
    Private mLoaded As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Guid
        Nom
        Logo
    End Enum

    Public Sub New(ByVal oTpa As Tpa)
        MyBase.New()
        Me.InitializeComponent()
        mTpa = oTpa
        LoadGrid()
    End Sub


    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Guid,DSC,LOGO FROM TPA WHERE EMP=" & BLLApp.Emp.Id & " ORDER BY ORD"
        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = 48
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToAddRows = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.Logo)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Nom)
                .Visible = False
            End With
        End With

        If mTpa IsNot Nothing Then
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Guid).Value.Equals(mTpa.Guid) Then
                    oRow.Selected = True
                    Exit For
                End If
            Next
        End If

    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim sTpaNom As String = oRow.Cells(Cols.Nom).Value
        e.ToolTipText = sTpaNom
    End Sub

    Private Function CurrentItem() As Tpa
        Dim retVal As Tpa = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
            retVal = New Tpa(oGuid)
            retVal.Nom = oRow.Cells(Cols.Nom).Value
        End If
        Return retVal
    End Function

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Sortida()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Sortida()
        End If
    End Sub

    Private Sub Sortida()
        Dim oTpa As Tpa = CurrentItem()
        If oTpa IsNot Nothing Then
            RaiseEvent AfterUpdate(CurrentItem, New System.EventArgs)
        End If
        Me.Close()
    End Sub
End Class