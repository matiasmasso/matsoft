

Public Class Menu_Ccd
    Private _Ccds As List(Of DTOCcd)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCcd As DTOCcd)
        MyBase.New()
        _Ccds = New List(Of DTOCcd)
        _Ccds.Add(oCcd)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Extracte(),
        MenuItem_Contact(),
        MenuItem_Excel()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extracte"
        If _Ccds.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function



    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If _Ccds.Count = 1 Then
            If _Ccds.First.Contact IsNot Nothing Then
                oMenuItem.Text = "Contacte..."
                oMenuItem.DropDownItems.AddRange(New Menu_Contact(_Ccds.First.Contact).Range)
            Else
                oMenuItem.Visible = False
            End If
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "excel"
        oMenuItem.Image = My.Resources.Excel
        If _Ccds.Count <= 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function




    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================


    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCcd As DTOCcd = _Ccds(0)
        Dim oFrm As New Frm_Extracte(oCcd.Contact, oCcd.Cta, oCcd.Exercici)
        oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDs As New DataSet
        Dim oTable As New DataTable
        oTable.Columns.Add("CLX", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        oDs.Tables.Add(oTable)
        For Each oCcd As DTOCcd In _Ccds
            oRow = oTable.NewRow
            oRow(0) = oCcd.Contact.FullNom
            oTable.Rows.Add(oRow)
        Next
        Dim oSheet = MatHelperStd.ExcelHelper.Sheet.Factory(oDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub



End Class
