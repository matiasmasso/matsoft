

Public Class Frm_4Moms

    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Nom
        mamaroo
        origami
        obs
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT M.CLI,CLX.CLX,M.MAMAROO,M.ORIGAMI,M.OBS " _
                            & "FROM [4MOMS] M INNER JOIN " _
                            & "CLX ON M.CLI=CLX.CLI AND CLX.EMP=1 " _
                            & "ORDER BY FCHCREATED"

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
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Cli)
                .Visible = False
            End With

            With .Columns(Cols.Nom)
                .HeaderText = "nom"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

            With .Columns(Cols.mamaroo)
                .HeaderText = "mamaRoo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(Cols.origami)
                .HeaderText = "Origami"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

            With .Columns(Cols.obs)
                .HeaderText = "obs"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
            End With

        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Dim oContact As Contact = Xl_Contact1.Contact
        Dim mamaRoo As Integer = 0
        If IsNumeric(TextBoxMamaroo.Text) Then mamaRoo = CInt(TextBoxMamaroo.Text)
        Dim origami As Integer = 0
        If IsNumeric(TextBoxOrigami.Text) Then origami = CInt(TextBoxOrigami.Text)
        Dim SQL As String = "INSERT INTO [4MOMS] (CLI,mamaroo,origami,OBS) VALUES (@CLI,@MAMAROO,@ORIGAMI,@OBS)"
        MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@CLI", oContact.Id, "@MAMAROO", mamaRoo, "@ORIGAMI", origami, "@OBS", TextBoxObs.Text)
        LoadGrid()
        TextBoxMamaroo.Text = ""
        TextBoxOrigami.Text = ""
        TextBoxObs.Text = ""
        Xl_Contact1.Clear()
        'Xl_Contact1.Focus()
    End Sub

    Private Sub Xl_Contact1_AfterUpdate(sender As Object, e As System.EventArgs) Handles Xl_Contact1.AfterUpdate
        Button1.Enabled = True
    End Sub
End Class