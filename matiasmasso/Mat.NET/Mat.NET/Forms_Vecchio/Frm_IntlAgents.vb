

Public Class Frm_IntlAgents
    Private mContact As Contact
    Private mMode As Modes
    Private mAllowEvents As Boolean

    Public Enum Modes
        Representants
        Representades
    End Enum

    Private Enum Cols
        CliGuid
        CliNom
        AreaGuid
        AreaNom
    End Enum

    Public Sub New(oContact As Contact, oMode As bll.dEFAULTS.SelectionModes)
        MyBase.New()
        Me.InitializeComponent()
        mContact = oContact
        mMode = oMode
    End Sub


    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case mMode
            Case Modes.Representades
                Me.Text = "Representades de " & mContact.Nom
            Case Modes.Representants
                Me.Text = "Representants de " & mContact.Nom
        End Select

        LoadGrid()
        SetContextMenu()
        mAllowEvents = True
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = ""

        Select Case mMode
            Case Modes.Representades
                SQL = "SELECT Principal as Contact, Clx.Clx, IntlAgents.Area, Area.Nom FROM IntlAgents INNER JOIN " _
                    & "CLIGRAL ON Principal=CliGral.Guid inner join " _
                    & "CLX ON CLIGRAL.EMP=CLX.EMP AND CLIGRAL.CLI=CLX.CLI inner join " _
                    & "AREA ON IntlAgents.Area=Area.Guid " _
                    & "WHERE AGENT LIKE @Guid " _
                    & "ORDER BY CLX.CLX"
            Case Modes.Representants
                SQL = "SELECT Agent as Contact, Clx.Clx, IntlAgents.Area, Area.Nom FROM IntlAgents INNER JOIN " _
                    & "CLIGRAL ON Agent=CliGral.Guid inner join " _
                    & "CLX ON CLIGRAL.EMP=CLX.EMP AND CLIGRAL.CLI=CLX.CLI inner join " _
                    & "AREA ON IntlAgents.Area=Area.Guid " _
                    & "WHERE Principal LIKE @Guid " _
                    & "ORDER BY CLX.CLX"
        End Select

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "Guid", mContact.Guid.ToString)
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

            With .Columns(Cols.CliGuid)
                .Visible = False
            End With

            With .Columns(Cols.CliNom)
                '.HeaderText = "small text"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 70
            End With

            With .Columns(Cols.AreaGuid)
                .Visible = False
            End With

            With .Columns(Cols.AreaNom)
                '.HeaderText = "small text"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Width = 70
            End With
        End With
    End Sub

    Private Function CurrentItm() As IntlAgent
        Dim oItm As IntlAgent = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oCliGuid As Guid = CType(oRow.Cells(Cols.CliGuid).Value, Guid)
            Dim oPrincipal As Contact = Nothing
            Dim oAgent As Contact = Nothing
            Select Case mMode
                Case Modes.Representades
                    oAgent = mContact
                    oPrincipal = New Contact(oCliGuid)
                Case Modes.Representants
                    oPrincipal = mContact
                    oAgent = New Contact(oCliGuid)
            End Select
            Dim oArea As New DTOArea(CType(oRow.Cells(Cols.AreaGuid).Value, Guid))
            oItm = New IntlAgent(oPrincipal, oAgent, oArea)
        End If
        Return oItm
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenuStrip As New ContextMenuStrip
        Dim oMenuItem As ToolStripMenuItem = Nothing

        Dim oItm As IntlAgent = CurrentItm()
        If oItm IsNot Nothing Then
            oMenuItem = New ToolStripMenuItem("contacte")
            oContextMenuStrip.Items.Add(oMenuItem)

            Dim oContact As Contact = IIf(mMode = Modes.Representants, oItm.Agent, oItm.Principal)
            Dim oMenuContact As New Menu_Contact(oContact)
            oMenuItem.DropDownItems.AddRange(oMenuContact.Range)
        End If

        oContextMenuStrip.Items.Add(New ToolStripMenuItem("afegir...", My.Resources.clip, AddressOf AddNewItm))
        DataGridView1.ContextMenuStrip = oContextMenuStrip
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        If mAllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub AddNewItm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPrincipal As Contact = Nothing
        Dim oAgent As Contact = Nothing
        Select Case mMode
            Case Modes.Representades
                oAgent = mContact
            Case Modes.Representants
                oPrincipal = mContact
        End Select
        Dim oIntlAgent As New IntlAgent(oPrincipal, oAgent, New DTOArea(BLL.BLLCountries.DefaultCountry))
        Dim oFrm As New Frm_IntlAgent(oIntlAgent)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Zoom()
        Dim oFrm As New Frm_IntlAgent(CurrentItm)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Delete()
        Dim oItm As Object = CurrentItm()
        If oItm.AllowDelete Then
            Dim rc As MsgBoxResult = MsgBox("eliminem " & oItm.nom & "?", MsgBoxStyle.OkCancel, "MAT.NET")
            If rc = MsgBoxResult.Ok Then
                Dim BlSuccess As Boolean = oItm.Delete
                If BlSuccess Then
                    RefreshRequest(Nothing, EventArgs.Empty)
                Else
                    MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
                End If
            End If
        Else
            MsgBox("no es pot eliminar", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.CliNom
        Dim oGrid As DataGridView = DataGridView1

        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If oRow IsNot Nothing Then
            i = oRow.Index
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

End Class