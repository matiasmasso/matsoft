

Public Class Frm_EFras
    Private mAllowEvents As Boolean

    Private Enum Cols
        Cli
        Nom
        Fch
    End Enum

    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT        CLX.Guid, CLX.clx, MIN(SSCEMAIL.FchCreated) AS FCH " _
        & "FROM            CLX INNER JOIN " _
        & "Credit_ClassificacioClients ON Credit_ClassificacioClients.CliGuid = CLX.Guid " _
        & "LEFT OUTER JOIN EMAIL_CLIS " _
        & "INNER JOIN CCX ON EMAIL_CLIS.ContactGuid = CCX.Guid " _
        & "INNER JOIN EMAIL ON EMAIL_CLIS.EmailGuid = EMAIL.Guid " _
        & "INNER JOIN SSCEMAIL ON EMAIL.guid = SSCEMAIL.Email ON Credit_ClassificacioClients.CliGuid = CCX.CcxGuid " _
        & "GROUP BY CLX.cli, CLX.clx"

        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)

        Dim iCount As Integer
        For Each oRow As DataRow In oTb.Rows
            If Not IsDBNull(oRow(Cols.Fch)) Then
                iCount += 1
            End If
        Next

        LabelCount.Text = LabelCount.Text & " " & iCount & "/" & oTb.Rows.Count & " (" & Format(100 * iCount / oTb.Rows.Count, "0.0") & " %)"
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

            With .Columns(Cols.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Width = 80
            End With
        End With
    End Sub


    Private Function CurrentContact() As DTOContact
        Dim oContact As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Cli).Value
            oContact = New DTOContact(oGuid)
            oContact.FullNom = DataGridView1.CurrentRow.Cells(Cols.Nom).Value
        End If
        Return oContact
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oContact As DTOContact = CurrentContact()

        If oContact IsNot Nothing Then
            Dim oMenu_Contact As New Menu_Contact(oContact)
            AddHandler oMenu_Contact.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Contact.Range)

        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oObject As New Object
        'Dim oFrm As New Frm_ObjectProperties(oObject)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Nom
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If oGrid.CurrentRow Is Nothing Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub ButtonTels_Click(sender As System.Object, e As System.EventArgs) Handles ButtonTels.Click
        Dim SQL As String = "SELECT COUNT(DISTINCT FRA.fra) as List(Of Fra), CLX.clx, Cli_Tel_Default.tel " _
        & "FROM            FRA INNER JOIN " _
        & "CLX ON FRA.CliGuid = CLX.Guid INNER JOIN " _
        & "CliGral ON FRA.CliGuid = CliGral.Guid INNER JOIN " _
        & "Cli_Tel_Default ON FRA.CliGuid = Cli_Tel_Default.CliGuid " _
        & "WHERE        (CliGral.Emp = 1) AND (FRA.yea > 2011) AND (FRA.CliGuid NOT IN " _
        & "(SELECT  EMAIL_CLIS_1.ContactGuid " _
        & "FROM            EMAIL_CLIS AS EMAIL_CLIS_1 INNER JOIN " _
        & "EMAIL AS EMAIL_1 ON EMAIL_CLIS_1.EmailGuid = EMAIL_1.Guid INNER JOIN " _
        & "SSCEMAIL ON EMAIL_1.guid = SSCEMAIL.Email " _
        & "WHERE        (SSCEMAIL.Emp = 1) AND (SSCEMAIL.Ssc = 2) " _
        & "GROUP BY EMAIL_CLIS_1.ContactGuid)) " _
        & "GROUP BY CLX.clx, Cli_Tel_Default.tel " _
        & "ORDER BY fras DESC"

        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(oDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonEmails_Click(sender As System.Object, e As System.EventArgs) Handles ButtonEmails.Click
        Dim SQL As String = "SELECT        EMAIL.Adr, CLX.clx " _
        & "FROM            FRA INNER JOIN " _
        & "CLX ON FRA.CliGuid = CLX.Guid INNER JOIN " _
        & "CliGral ON FRA.CliGuid = CliGral.Guid INNER JOIN " _
        & "EMAIL_CLIS ON FRA.CliGuid = EMAIL_CLIS.ContactGuid INNER JOIN " _
        & "EMAIL ON EMAIL_CLIS.EmailGuid = EMAIL.Guid " _
        & "WHERE        (CliGral.Emp = 1) AND (FRA.yea > 2011) AND EMAIL.BADMAIL=0 AND EMAIL.PRIVAT=0 AND EMAIL.NONEWS=0 AND CLX.EX=0 AND (FRA.CliGuid NOT IN " _
        & "(SELECT        EMAIL_CLIS_1.ContactGuid " _
        & "FROM            EMAIL_CLIS AS EMAIL_CLIS_1 INNER JOIN " _
        & "EMAIL AS EMAIL_1 ON EMAIL_CLIS_1.EmailGuid = EMAIL_1.Guid INNER JOIN " _
        & "SSCEMAIL ON EMAIL_1.guid = SSCEMAIL.Email " _
        & "WHERE        (SSCEMAIL.Emp = 1) AND (SSCEMAIL.Ssc = 2)  " _
        & "GROUP BY EMAIL_CLIS_1.ContactGuid)) " _
        & "GROUP BY EMAIL.Adr, CLX.clx " _
        & "ORDER BY CLX.clx"


        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(oDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class