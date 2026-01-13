Public Class Menu_Cta
    Inherits Menu_Base

    Private _Cta As DTOPgcCta
    Private _Ctas As List(Of DTOPgcCta)
    Private mPgcCtas As PgcCtas

    Public Sub New(ByVal value As DTOPgcCta)
        MyBase.New()
        _Cta = value
        _Ctas = New List(Of DTOPgcCta)
        _Ctas.Add(value)

        mPgcCtas = New PgcCtas
        mPgcCtas.Add(New PgcCta(_Cta.Guid))
    End Sub

    Public Sub New(ByVal values As List(Of DTOPgcCta))
        MyBase.New()
        _Ctas = values
        _Cta = values(0)

        mPgcCtas = New PgcCtas
        For Each oCta As DTOPgcCta In values
            mPgcCtas.Add(New PgcCta(_Cta.Guid))
        Next
    End Sub

    Public Sub New(ByVal oPgcCta As PgcCta)
        MyBase.New()
        mPgcCtas = New PgcCtas
        mPgcCtas.Add(oPgcCta)

        _Cta = New DTOPgcCta(oPgcCta.Guid)
        _Ctas = New List(Of DTOPgcCta)
        _Ctas.Add(_Cta)
    End Sub

    Public Sub New(ByVal oPgcCtas As PgcCtas)
        MyBase.New()
        mPgcCtas = oPgcCtas
        _Ctas = New List(Of DTOPgcCta)
        For Each oCta As PgcCta In oPgcCtas
            _Ctas.Add(New DTOPgcCta(oCta.Guid))
        Next
        _Cta = _Ctas(0)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom()})
    End Function


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
        root.ShowCta(mPgcCtas(0))
    End Sub


End Class
