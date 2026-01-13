

Public Class Frm_CliDtos

    Private mClient As client
    Private mDs As DataSet
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Brand
        Dsc
        Dto
        obsoleto
    End Enum

    Public Sub New(ByVal oClient As Client)
        MyBase.New()
        Me.InitializeComponent()
        mClient = oClient
        If mClient.Dto = 0 Then
            RadioButtonDtoTpa.Checked = True
            TextBoxDtoGlobal.Enabled = False
        Else
            RadioButtonDtoGlobal.Checked = True
            TextBoxDtoGlobal.Text = mClient.Dto
            DataGridView1.Enabled = False
        End If
        TextBoxCliNom.Text = mClient.Clx
        LoadGrid()
        mAllowEvents = True
    End Sub


    Private Sub LoadGrid()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CLIDTO.Brand, TPA.DSC, CLIDTO.Dto, TPA.OBSOLETO ")
        sb.AppendLine("FROM TPA ")
        sb.AppendLine("LEFT OUTER JOIN ")
        sb.AppendLine("CLIDTO ON TPA.Guid=CLIDTO.Brand ")
        sb.AppendLine("WHERE CLIDTO.Customer='" & mClient.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY TPA.OBSOLETO, TPA.ORD, TPA.Dsc")

        Dim SQL As String = sb.ToString
        mDs = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = mDs.Tables(0)

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

            Select Case MaxiSrvr.Emp.FromDTOEmp(mClient.Emp).WinUsr.Rol.Id
                Case Rol.Ids.SuperUser, Rol.Ids.Admin
                    .ReadOnly = False
                Case Else
                    .ReadOnly = True
            End Select

            With .Columns(Cols.Brand)
                .Visible = False
            End With
            With .Columns(Cols.Dsc)
                .HeaderText = "marca comercial"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(Cols.Dto)
                .HeaderText = "descompte"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.obsoleto)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click

        Dim SQL As String = "DELETE CLIDTO WHERE Customer = '" & mClient.Guid.ToString & "'"
        MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi)

        If RadioButtonDtoTpa.Checked Then
            Dim oRow As DataRow
            Dim SngDto As Decimal = 0
            For Each oRow In mDs.Tables(0).Rows
                If Not IsDBNull(oRow(Cols.Dto)) Then
                    SngDto = oRow(Cols.Dto)
                    If SngDto <> 0 Then
                        Dim sDto As String = SngDto.ToString.Replace(",", ".")
                        SQL = "INSERT INTO CLIDTO (Customer,Brand,Dto) VALUES (@Customer,@Brand,@Dto)"
                        MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@Customer", mClient.Guid.ToString, "@Brand", CType(oRow(Cols.Brand), Guid).ToString, "@Dto", sDto)
                    End If
                End If
            Next

            Dim DecDtoGlobal As Decimal = 0
            If RadioButtonDtoGlobal.Checked Then
                DecDtoGlobal = TextBoxDtoGlobal.Text
            End If

            If IsNumeric(TextBoxDtoGlobal.Text) Then
                SQL = "UPDATE CLICLIENT SET DTO=@DTO WHERE Guid=@Customer"
                MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi, "@DTO", DecDtoGlobal, "@Customer", mClient.Guid.ToString)
            End If
        Else
            mClient.Dto = TextBoxDtoGlobal.Text
        End If

        RaiseEvent AfterUpdate(mClient, System.EventArgs.Empty)
        Me.Close()
    End Sub

    Private Sub EnableButtons()
        Select Case BLL.BLLSession.Current.User.Rol.id
            Case Rol.Ids.SuperUser, Rol.Ids.Admin
                ButtonOk.Enabled = True
        End Select
    End Sub

    Private Sub DataGridView1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellValidated
        EnableButtons()
    End Sub

    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim BlObsoleto As Boolean = CBool(oRow.Cells(Cols.obsoleto).Value)
        Select Case BlObsoleto
            Case True
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub

    Private Sub RadioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    RadioButtonDtoGlobal.CheckedChanged, RadioButtonDtoTpa.CheckedChanged
        If mAllowEvents Then
            If RadioButtonDtoTpa.Checked Then
                DataGridView1.Enabled = True
                TextBoxDtoGlobal.Enabled = False
            Else
                DataGridView1.Enabled = False
                TextBoxDtoGlobal.Enabled = True
            End If
            EnableButtons()
        End If

    End Sub

    Private Sub TextBoxDtoGlobal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxDtoGlobal.TextChanged
        EnableButtons()
    End Sub
End Class