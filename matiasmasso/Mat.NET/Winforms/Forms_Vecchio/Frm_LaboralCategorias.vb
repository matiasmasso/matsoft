Public Class Frm_LaboralCategorias
    Private mMode As Modes
    Private mDefaultLaboralCategoria As LaboralCategoria = Nothing
    Public Event AfterSelect(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Modes
        Consulta
        LookUp
    End Enum

    Public Enum Cols
        Guid
        GrupId
        GrupNom
        Ord
        Nom
    End Enum

    Public Sub New(ByVal oMode As Modes, Optional ByVal oLaboralCategoria As LaboralCategoria = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        mMode = oMode
        mDefaultLaboralCategoria = oLaboralCategoria
        LoadGrid()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT LC.Guid,LC.SegSocialGrup, SS.Nom AS GrupNom, LC.Ord, LC.Nom " _
        & "FROM StaffCategory LC INNER JOIN " _
        & "SegSocialGrups SS ON LC.SegSocialGrup = SS.Id " _
        & "ORDER BY LC.SegSocialGrup, LC.Ord"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
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
            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.GrupId)
                .Visible = False
            End With
            With .Columns(Cols.Ord)
                .Visible = False
            End With
            With .Columns(Cols.GrupNom)
                .HeaderText = "Grup de cotització"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Nom)
                .HeaderText = "Categoría laboral"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With

        If mDefaultLaboralCategoria IsNot Nothing Then
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Guid).Value.ToString = mDefaultLaboralCategoria.Guid.ToString Then
                    DataGridView1.CurrentCell = oRow.Cells(Cols.Nom)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Function CurrentItm() As LaboralCategoria
        Dim oItm As LaboralCategoria = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oItm = New LaboralCategoria(New System.Guid(oRow.Cells(Cols.Guid).Value.ToString))
        End If
        Return oItm
    End Function

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.GrupNom
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim GrupId As Integer = oRow.Cells(Cols.GrupId).Value
                e.Value = "Grup " & GrupId & ". " & e.Value
        End Select
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        If mMode = Modes.LookUp Then
            Dim oItm As LaboralCategoria = CurrentItm()
            If oItm IsNot Nothing Then
                RaiseEvent AfterSelect(CurrentItm, EventArgs.Empty)
                Me.Close()
            End If
        End If
    End Sub
End Class