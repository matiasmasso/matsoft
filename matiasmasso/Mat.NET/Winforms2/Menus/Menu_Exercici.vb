Public Class Menu_Exercici

    Inherits Menu_Base

    Private _Exercici As DTOExercici


    Public Sub New(ByVal oExercici As DTOExercici)
        MyBase.New()
        _Exercici = oExercici
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Balance())
    End Sub
    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Balance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Balanç"
        oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Balance
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Balance(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_Exercici(_Exercici)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub


End Class

