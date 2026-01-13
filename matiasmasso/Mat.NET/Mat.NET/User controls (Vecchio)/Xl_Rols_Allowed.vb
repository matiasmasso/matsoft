
Imports System.Data.SqlClient

Public Class Xl_Rols_Allowed
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Chk
        Nom
    End Enum

    Public Event AfterUpdate(sender As Object, e As System.EventArgs)

    Public Shadows Sub Load(values As List(Of DTORol))
        mAllowEvents = False
        For Each oRow As DataGridViewRow In DataGridView1.Rows
            oRow.Cells(Cols.Chk).Value = False
        Next
        If values IsNot Nothing Then
            If DataGridView1.Rows.Count = 0 Then LoadGrid()
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                Dim RolId As DTORol.Ids = CType(oRow.Cells(Cols.Id).Value, Rol.Ids)
                For Each oRol As DTORol In values
                    If oRol.Id = RolId Then
                        oRow.Cells(Cols.Chk).Value = True
                        Exit For
                    End If
                Next
            Next
            mAllowEvents = True
        End If
    End Sub

    Public ReadOnly Property Rols As List(Of DTORol)
        Get
            Dim retVal As New List(Of DTORol)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Chk).Value Then
                    Dim RolId As DTORol.Ids = CType(oRow.Cells(Cols.Id).Value, Rol.Ids)
                    Dim oRol As New DTORol(RolId)
                    retVal.Add(oRol)
                End If
            Next
            Return retVal
        End Get
    End Property

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT ROL,CAST(0 AS BIT) AS CHK,NOM FROM USRROLS ORDER BY ROL"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .ReadOnly = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Chk)
                .HeaderText = ""
                .Width = 20
                .DefaultCellStyle.SelectionBackColor = Color.White
            End With
            With .Columns(Cols.Nom)
                .HeaderText = ""
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
        End With
    End Sub

    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If mAllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Chk

                    RaiseEvent AfterUpdate(sender, e)
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub
End Class
