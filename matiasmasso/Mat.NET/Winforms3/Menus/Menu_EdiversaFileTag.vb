Public Class Menu_EdiversaFileTag
    Inherits Menu_Base

    Private _Tags As List(Of String)
    Private _Tag As String

    Public Sub New(ByVal oTags As List(Of String))
        MyBase.New()
        _Tags = oTags
        If _Tags IsNot Nothing Then
            If _Tags.Count > 0 Then
                _Tag = _Tags.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oTag As String)
        MyBase.New()
        _Tag = oTag
        _Tags = New List(Of String)
        If _Tag IsNot Nothing Then
            _Tags.Add(_Tag)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Procesa())
        'MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Procesa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Procesa"
        AddHandler oMenuItem.Click, AddressOf Do_Procesa
        If _Tag Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = _Tag.Count = 1
        End If
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Procesa(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.WarnError("pendent de implementar")
    End Sub




End Class

