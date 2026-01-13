

Public Class Xl_DropdownList_Visa
    Private mVisa As VisaCard

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Private Enum Cols
        Id
        Nom
        Img
    End Enum

    Public Property Visa() As VisaCard
        Get
            Return CurrentVisa()
        End Get
        Set(ByVal oVisa As VisaCard)
            For Each oRow As DataGridViewRow In DataGridView1.Rows
                If oRow.Cells(Cols.Id).Value = oVisa.Id Then
                    DataGridView1.CurrentCell = oRow.Cells(Cols.Img)
                    Exit Property
                End If
            Next
        End Set
    End Property

    Private Sub Xl_DropdownList_Visa_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadVisas()
    End Sub

    Private Sub LoadVisas()
        Dim SQL As String = "SELECT ID,NOM,IMG FROM VISA ORDER BY NOM"
        Dim oDs As DataSet = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridView1
            .DataSource = oTb
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .RowTemplate.Height = 48
            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Nom)
                .Visible = False
            End With
            With .Columns(Cols.Img)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                '.Width = 48
            End With
        End With

        SetContextMenu()

    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItemZoom As New ToolStripMenuItem("zoom", My.Resources.prismatics_16, AddressOf Do_Zoom)
        oContextMenu.Items.Add(oMenuItemZoom)
        Dim oMenuItemAdd As New ToolStripMenuItem("afegir nova", My.Resources.AddFileGroc, AddressOf Do_AddNew)
        oContextMenu.Items.Add(oMenuItemAdd)


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Function CurrentVisa() As VisaCard
        Dim oVisa As VisaCard = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oVisa = New VisaCard(oRow.Cells(Cols.Id).Value)
        End If
        Return oVisa
    End Function

    Private Sub Do_Zoom()
        Dim oFrm As New Frm_VisaCard_Old
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Visa = CurrentVisa()
            .Show()
        End With
    End Sub

    Private Sub Do_AddNew()
        Dim oFrm As New Frm_VisaCard_Old
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Visa = New VisaCard
            .Show()
        End With
    End Sub

    Private Sub DataGridView1_CellToolTipTextNeeded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles DataGridView1.CellToolTipTextNeeded
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oVisa As New VisaCard(oRow.Cells(Cols.Id).Value)
        e.ToolTipText = oVisa.Nom
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Do_Zoom()
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        mVisa = CurrentVisa()
        RaiseEvent AfterUpdate(mVisa, e)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadVisas()
    End Sub
End Class
