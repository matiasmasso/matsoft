

Public Class Frm_Rols_Allowed
    Private mRols As List(Of DTORol)
    Private mAllowEvents As Boolean

    Private Enum Cols
        Id
        Chk
        Nom
    End Enum

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oRols As List(Of DTORol))
        MyBase.New()
        Me.InitializeComponent()

        mRols = oRols
        LoadGrid()
        mAllowEvents = True
    End Sub

    Public ReadOnly Property Rols As List(Of DTORol)
        Get
            Dim oRols As New List(Of DTORol)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Chk).Value = True Then
                    Dim RolId As DTORol.Ids = CType(oRow.Cells(Cols.Id).Value, Rol.Ids)
                    Dim oRol As New DTORol(RolId)
                    oRols.Add(oRol)
                End If
            Next
            Return oRols
        End Get
    End Property

    Private Sub LoadGrid()
        Dim sField As String = BLL.BLLApp.Lang.Tradueix("NOM", "NOM_CAT", "NOM_ENG")
        Dim SQL As String = "SELECT ROL,CAST(0 AS bIT) AS CHK,(CASE WHEN " & sField & " IS NULL THEN NOM ELSE " & sField & " END) AS NOM FROM USRROLS ORDER BY ROL"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow As DataRow In oTb.Rows
            If CType(oRow(Cols.Id), Rol.Ids) = Rol.Ids.SuperUser Then
                oRow(Cols.Chk) = True
            Else
                For Each oRol As DTORol In mRols
                    If CInt(oRow(Cols.Id)) = CInt(oRol.Id) Then
                        oRow(Cols.Chk) = True
                    End If
                Next
            End If
        Next

        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.35
            End With
            .DataSource = oTb.DefaultView
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = True
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
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
        End With

    End Sub


    Private Function CurrentRol() As DTORol
        Dim oRol As DTORol = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If Not oRow Is Nothing Then
            Dim RolId As Integer = oRow.Cells(Cols.Id).Value
            oRol = New DTORol(RolId)
        End If
        Return oRol
    End Function


    Private Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Select Case e.ColumnIndex
            Case Cols.Chk
                ButtonOk.Enabled = True
        End Select
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case DataGridView1.CurrentCell.ColumnIndex
            Case Cols.Chk
                DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        RaiseEvent AfterUpdate(Me.Rols, EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class