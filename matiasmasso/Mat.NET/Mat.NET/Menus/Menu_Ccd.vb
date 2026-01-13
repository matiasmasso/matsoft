

Public Class Menu_Ccd
    Private mCcds As Ccds
    Private _emp as DTOEmp

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCcd As Ccd, Optional oEmp as DTOEmp = Nothing)
        MyBase.New()
        mCcds = New Ccds
        mCcds.Add(oCcd)
        _Emp = oEmp
    End Sub

    Public Sub New(ByVal oCcds As Ccds)
        MyBase.New()
        mCcds = oCcds
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Extracte(), _
        MenuItem_SubComptes(), _
        MenuItem_Contact(), _
        MenuItem_Excel(), _
        MenuItem_Block() _
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Extracte() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "extracte"
        If mCcds.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Extracte
        Return oMenuItem
    End Function

    Private Function MenuItem_SubComptes() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mCcds.Count = 1 Then
            If mCcds(0).Contact IsNot Nothing Then
                oMenuItem.Text = "SubComptes"
                oMenuItem.Image = My.Resources.People_Blue
                AddHandler oMenuItem.Click, AddressOf Do_SubComptes
            Else
                oMenuItem.Visible = False
            End If
        Else
            oMenuItem.Visible = False
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Contact() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        If mCcds.Count = 1 Then
            If mCcds(0).Contact IsNot Nothing Then
                oMenuItem.Text = "Contacte..."
                oMenuItem.DropDownItems.AddRange(New Menu_Contact(mCcds(0).Contact).Range)
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
        If mCcds.Count <= 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_Block() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "bloqueig"
        oMenuItem.Image = My.Resources.candau
        If mCcds.Count > 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Block
        Return oMenuItem
    End Function





    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_SubComptes(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCceCcds(New Cce(mCcds(0).Contact.Emp, mCcds(0).Cta, mCcds(0).Yea), CDate("1/1/" & Today.Year), Today)
    End Sub

    Private Sub Do_Extracte(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oCcd As Ccd = mCcds(0)
        Dim oContact As Contact = oCcd.Contact
        Dim oCta As PgcCta = oCcd.Cta
        Dim oEmp as DTOEmp
        If oContact Is Nothing Then
            If _Emp Is Nothing Then
                oEmp =BLL.BLLApp.Emp
            Else
                oEmp = _Emp
            End If
        Else
            oEmp = oContact.Emp
        End If
        Dim oExercici As New Exercici(oEmp, oCcd.Yea)
        Dim oFrm As New Frm_CliCtas(oContact, oCta, oExercici)
        oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDs As New DataSet
        Dim oTable As New DataTable
        oTable.Columns.Add("CLX", System.Type.GetType("System.String"))
        Dim oRow As DataRow
        oDs.Tables.Add(oTable)
        For Each oCcd As Ccd In mCcds
            oRow = oTable.NewRow
            oRow(0) = oCcd.Contact.Clx
            oTable.Rows.Add(oRow)
        Next
        MatExcel.GetExcelFromDataset(oDs).Visible = True
    End Sub

    Private Sub Do_Block(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowCcbBlock(mCcds(0))
    End Sub


End Class
