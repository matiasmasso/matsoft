Public Class Menu_Cta
    Inherits Menu_Base

    Private _Cta As DTOPgcCta
    Private _Ctas As List(Of DTOPgcCta)

    Public Sub New(ByVal value As DTOPgcCta)
        MyBase.New()
        _Cta = value
        _Ctas = New List(Of DTOPgcCta)
        _Ctas.Add(value)
        AddMenuItems()
    End Sub

    Public Sub New(ByVal values As List(Of DTOPgcCta))
        MyBase.New()
        _Ctas = values
        _Cta = values(0)
        AddMenuItems()
    End Sub



    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PgcCta(_Cta)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


End Class
