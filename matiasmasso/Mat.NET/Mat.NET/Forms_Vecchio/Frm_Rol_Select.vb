

Public Class Frm_Rol_Select
    Private mDs As DataSet
    Private mRol As DTORol
    Private mClosed As Boolean

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Property Rol() As DTORol
        Get
            Return mRol
        End Get
        Set(ByVal Value As DTORol)
            LoadGrid()
            If Not Value Is Nothing Then
                SelectRow(Value.Id, Cols.Id)
            End If
        End Set
    End Property

    Private Sub LoadGrid()
        Dim sField As String = BLL.BLLApp.Lang.Tradueix("NOM", "NOM_CAT", "NOM_ENG")
        Dim SQL As String = "SELECT ROL," & sField & " FROM USRROLS ORDER BY ROL"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentRol() As DTORol
        Dim oRol As DTORol = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iRol As DTORol.Ids = oRow.Cells(Cols.Id).Value
            oRol = New DTORol(iRol)
            oRol.NomEsp = oRow.Cells(Cols.Nom).Value
        End If
        Return oRol
    End Function

    Private Sub SelectRow(ByVal iKey As Integer, Optional ByVal ColIdx As Integer = 0)
        Dim oRow As DataGridViewRow
        For Each oRow In DataGridView1.Rows
            If oRow.Cells(Cols.Id).Value = iKey Then
                DataGridView1.CurrentCell = oRow.Cells(Cols.Nom)
                Exit Sub
            End If
        Next
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Me.Close()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            mClosed = True
            Me.Close()
        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If Not mClosed Then
            mRol = CurrentRol()
        End If
    End Sub
End Class